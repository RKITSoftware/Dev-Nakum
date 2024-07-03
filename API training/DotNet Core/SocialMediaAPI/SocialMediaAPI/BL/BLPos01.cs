using Check_Id_Exist;
using NLog;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SocialMediaAPI.Enums;
using SocialMediaAPI.Extension;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;
using SocialMediaAPI.Model.POCO;
using System.Data;

namespace SocialMediaAPI.BL
{
    public class BLPOS01 : IPOS01Service
    {
        #region Private Member

        /// <summary>
        /// create the variable for connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// create the object of the logger
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// create the object of the connection factory for ORMLite
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;
        /// <summary>
        /// create the object of the configuration for gt the connection string
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Validation service instance for validation operations.
        /// </summary>
        private readonly Validation _objValidation;

        /// <summary>
        /// Interface for database operations related to posts.
        /// </summary>
        private readonly IDBPOS01 _objIDBPos01;

        /// <summary>
        /// create the object of the post model
        /// </summary>
        private POS01 _objPOS01;

        #endregion

        #region Private Property

        /// <summary>
        /// set the current Http Context to get the user id
        /// </summary>
        private HttpContext HttpContext { get; set; }

        #endregion

        #region Public Properites

        /// <summary>
        /// operation types A - Add, E - Edit, D - Delete
        /// </summary>
        public enmOperationType OperationType { get; set; }

        #endregion

        #region Public Member
        /// <summary>
        /// create the object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BLPOS01 class with required dependencies.
        /// </summary>
        public BLPOS01(IConfiguration configuration, Validation objValidation, IDBPOS01 objIDBPos01, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _objValidation = objValidation;
            _objIDBPos01 = objIDBPos01;
            HttpContext = httpContextAccessor.HttpContext;
        }
        #endregion

        #region Private Method

        /// <summary>
        /// Uploads an image to the configured folder path and returns the image URL.
        /// </summary>
        /// <param name="imageFile">The IFormFile containing the image to upload.</param>
        /// <param name="_objPOS01">The temporary post object (_objPOS01) to update with the image URL.</param>
        /// <returns>The image URL if uploaded successfully, null otherwise.</returns>
        private string UploadImage(IFormFile imageFile, POS01 _objPOS01)
        {
            string uploadFolder, fileName, filePath;

            uploadFolder = Path.Combine(_configuration.GetValue<string>("Uploads:PostFolderPath"), "");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            Guid guid = Guid.NewGuid();
            fileName = _objPOS01.S01F02.ToString() + "_" + guid.ToString("N") + Path.GetExtension(imageFile.FileName);
            filePath = Path.Combine(uploadFolder, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("/Upload/Post", fileName);
        }

        /// <summary>
        /// Deletes an image file associated with a post.
        /// </summary>
        /// <param name="imgUrl">The URL of the image to delete.</param>
        /// <returns>True if the image is deleted successfully, false otherwise.</returns>
        private bool DeleteImage(string imgUrl)
        {
            string filePath, fullPath;
            filePath = imgUrl.Replace("/Upload/Post\\", "", StringComparison.OrdinalIgnoreCase);
            fullPath = Path.Combine(_configuration.GetValue<string>("Uploads:PostFolderPath"), filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets a post by its ID from the database.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve.</param>
        /// <returns>The post object if found, null otherwise.</returns>
        private POS01 GetPostById(int postId, int userId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Single<POS01>(x => x.S01F01 == postId && x.S01F02 == userId);
            }
        }

        #endregion

        #region Public Method

        /// <summary>
        /// PreSave the DTOs object to the POCOs and pre validations
        /// </summary>
        /// <param name="objDTOPOS01">object of the post</param>
        /// <param name="postId">post id</param>
        public void PreSave(DTOPOS01 objDTOPOS01, int postId = 0)
        {
            _objPOS01 = new POS01();
            int userId = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);
            string imageUrl;
            if (OperationType == enmOperationType.E)
            {
                //need to update the entire the object of the post
                POS01 post = GetPostById(postId, userId);

                // if post is available 
                if (post != null)
                {
                    _objPOS01 = post;
                    _objPOS01.S01F06 = DateTime.Now;
                }
            }

            if (OperationType == enmOperationType.A)
            {
                _objPOS01.S01F02 = userId;
            }

            // to check post content is available or not
            if (objDTOPOS01.S01F04 != null)
            {
                _objPOS01 = objDTOPOS01.MapDtoToPoco<DTOPOS01, POS01>(_objPOS01);
            }

            if (objDTOPOS01.S01F03 != null)
            {
                imageUrl = UploadImage(objDTOPOS01.S01F03, _objPOS01);
                _objPOS01.S01F03 = imageUrl;
                _logger.Info("Post image upload successfully");
            }
        }

        /// <summary>
        /// Post object Validation before inserting or updating into database
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();
            bool isPostExist, isUserExist;

            if (OperationType == enmOperationType.E)
            {
                //validation for post id
                isPostExist = _objValidation.IsExist<POS01>(_objPOS01.S01F01, x => x.S01F01);
                if (!isPostExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "post is not exist";
                    return objResponse;
                }
            }

            //validation for user id
            isUserExist = _objValidation.IsExist<USE01>(_objPOS01.S01F02, x => x.E01F01);
            if (!isUserExist)
            {
                objResponse.IsError = true;
                objResponse.Message = "user is not exist";
            }

            return objResponse;

        }

        /// <summary>
        /// Validation before deleting post into database
        /// </summary>
        /// <param name="postId">post id</param>
        /// <returns>response model</returns>
        public Response ValidationOnDelete(int postId)
        {
            objResponse = new Response();
            int userId;
            bool isUserExist, isPostExist;

            if (OperationType == enmOperationType.D)
            {
                userId = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);

                isUserExist = _objValidation.IsExist<USE01>(userId, x => x.E01F01);
                if (!isUserExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "user is not exist";

                    return objResponse;
                }
                isPostExist = _objValidation.IsExist<POS01>(postId, x => x.S01F01);
                if (!isPostExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "post is not exist";
                }
                else
                {
                    // validate the userId form post -- need to same 
                    (int userIdFromPost, string imgUrl) = _objIDBPos01.GetUserIdAndImgOfPost(postId);

                    if (userIdFromPost != userId)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "You can not delete the post, which is not created by you.";
                    }
                    else
                    {
                        _objPOS01 = new POS01
                        {
                            S01F01 = postId,
                            S01F03 = imgUrl
                        };
                    }
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Add or Update the post into database
        /// </summary>
        /// <returns>response model</returns>
        public Response Save()
        {
            objResponse = new Response();
            bool isPostAdded, isPostUpdated;

            if (OperationType == enmOperationType.A)
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    isPostAdded = db.Insert(_objPOS01) > 0;
                }
                if (!isPostAdded)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "something went wrong for adding post";
                }
                else
                {
                    objResponse.Message = "Post is added successfully";
                }
            }

            if (OperationType == enmOperationType.E)
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    isPostUpdated = db.Update(_objPOS01) > 0;
                }
                if (!isPostUpdated)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "something went wrong for updating post";
                }
                else
                {
                    objResponse.Message = "Post is updated successfully";
                }
            }
            return objResponse;

        }

        /// <summary>
        /// Retrieves a list of all posts from the database.
        /// </summary>
        /// <returns>response model</returns>
        public Response GetPosts()
        {
            objResponse = new Response();

            DataTable dtAllPost = _objIDBPos01.GetPosts();
            objResponse.Data = dtAllPost;
            return objResponse;

        }

        /// <summary>
        /// Retrieves a list of posts created by the current user.
        /// </summary>
        /// <returns>Response model</returns>
        public Response GetPostByMe()
        {
            objResponse = new Response();

            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);
            DataTable dtGetPostByUser = _objIDBPos01.GetPostByMe(id);
            objResponse.Data = dtGetPostByUser;
            return objResponse;
        }

        /// <summary>
        /// delete a post from the database.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <returns>response model</returns>
        public Response Delete()
        {
            objResponse = new Response();
            bool isPostDeleted, isImgDeleted;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isPostDeleted = db.DeleteById<POS01>(_objPOS01.S01F01) > 0;
                if (isPostDeleted)
                {
                    isImgDeleted = DeleteImage(_objPOS01.S01F03);
                    if (!isImgDeleted)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "something went wrong while delete the post img";
                    }
                    else
                    {
                        objResponse.Message = "Successfully deleted the post";
                    }
                }
            }
            return objResponse;
        }

        #endregion
    }
}