using AutoMapper;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NLog;
using NLog.Web.LayoutRenderers;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model.Dtos;
using SocialMediaAPI.Model.POCO;
using System.Data;
using System.Security.Claims;

namespace SocialMediaAPI.BL
{
    public class BLPosts : IPostService
    {
        #region Private Member
        private readonly string _connectionString;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDbConnectionFactory _dbFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private Pos01 _objPos01;
        private Pos01 _objUpdatePos01;
        #endregion

        #region Constructor
        public BLPosts(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("Default");
            _mapper = mapper;
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Maps the provided DTO object (containing post data) to a Pos01 entity
        /// and sets the current user ID (who is posting) in the Pos01 entity.
        /// </summary>
        /// <param name="objDtoPos01">The DTO object containing post data.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        private void PreSave(DtoPos01 objDtoPos01, HttpContext httpContext)
        {
            _objPos01 = _mapper.Map<Pos01>(objDtoPos01);
            var userid = httpContext.User.FindFirst("Id");
            _objPos01.S01F02 = Convert.ToInt32(userid?.Value);
        }

        /// <summary>
        /// Uploads a post image and updates the image URL in the temporary post object (_objPos01) for saving.
        /// Handles potential exceptions and logs errors.
        /// </summary>
        /// <param name="imageFile">The IFormFile containing the image to upload.</param>
        /// <returns>True if the image is uploaded successfully, false otherwise.</returns>
        private async Task<bool> Validation(DtoPos01 objDtoPos01)
        {
            try
            {
                string imageUrl = await UploadImage(objDtoPos01.S01101,_objPos01);
                _logger.Info("Post image upload successfully");
                _objPos01.S01F03 = imageUrl;
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: post image upload :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Uploads an image to the configured folder path and returns the image URL.
        /// Creates the folder if it doesn't exist.
        /// </summary>
        /// <param name="imageFile">The IFormFile containing the image to upload.</param>
        /// <param name="_objPos01">The temporary post object (_objPos01) to update with the image URL.</param>
        /// <returns>The image URL if uploaded successfully, null otherwise.</returns>
        private async Task<string> UploadImage(IFormFile imageFile, Pos01 _objPos01)
        {
            string uploadFolder = Path.Combine(_configuration.GetValue<string>("Uploads:PostFolderPath"), "");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            Guid guid = Guid.NewGuid();

            string fileName = _objPos01.S01F02.ToString() + "_" + guid.ToString("N") + Path.GetExtension(imageFile.FileName);

            string filePath = Path.Combine(uploadFolder, fileName);


            await using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
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
            try
            {
                string filePath = imgUrl.Replace("/Upload/Post\\", "", StringComparison.OrdinalIgnoreCase);

                string fullPath = Path.Combine(_configuration.GetValue<string>("Uploads:PostFolderPath"), filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: post img delete :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Adds a new post to the database.
        /// </summary>
        /// <returns>True if the post is added successfully, false otherwise.</returns>
        private async Task<bool> AddPost()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    return db.Insert<Pos01>(_objPos01) > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: post add :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Performs pre-update validation for a post.
        /// </summary>
        /// <param name="id">The ID of the post to update.</param>
        /// <param name="objDtoPos01">The DTO object containing updated post data.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <returns>True if the post can be updated, false otherwise.</returns>
        private bool PreSaveUpdate(int id, DtoPos01 objDtoPos01, HttpContext httpContext)
        {
            Pos01 post = GetPostById(id);
            if(post == null)
            {
                return false;
            }
            int userid = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            if(post.S01F02 != userid)
            {
                return false;
            }
            _objUpdatePos01 = post;
            _objUpdatePos01.S01F04 = objDtoPos01.S01102 != null ? objDtoPos01.S01102 : post.S01F04;
            _objUpdatePos01.S01F06 = DateTime.Now;
            return true;
        }

        /// <summary>
        /// Handles image deletion and upload during post update.
        /// </summary>
        /// <param name="objDtoPos01">The DTO object containing potentially updated post data (including image).</param>
        /// <returns>True if the image update is successful, false otherwise.</returns>
        private async Task<bool> ValidationUpdate(DtoPos01 objDtoPos01)
        {
            try
            {
                bool delete = DeleteImage(_objUpdatePos01.S01F03);

                if (delete)
                {
                    string imageUrl = await UploadImage(objDtoPos01.S01101,_objUpdatePos01);
                    _logger.Info("Post image upload successfully");
                    _objUpdatePos01.S01F03 = imageUrl;
                    return true;
                }
                else
                {
                    _logger.Error("Error :: delete post img");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: post image upload :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Updates a post in the database.
        /// </summary>
        /// <param name="id">The ID of the post to update.</param>
        /// <returns>True if the post is updated successfully, false otherwise.</returns>
        private bool UpdatePost(int id)
        {
            try
            {
                _objUpdatePos01.S01F01 = id;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    return db.Update<Pos01>(_objUpdatePos01) > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: post update :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets a post by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <returns>The post object if found, null otherwise.</returns>
        private Pos01 GetPostById(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<Pos01>(id);
            }
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Adds a new post to the database.
        /// </summary>
        /// <param name="objDtoPos01">The DTO object containing post data.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <returns>True if the post is added successfully, false otherwise.</returns>
        public async Task<bool> Add(DtoPos01 objDtoPos01, HttpContext httpContext)
        {
            PreSave(objDtoPos01, httpContext);
            bool isValidated = await Validation(objDtoPos01);
            if (isValidated)
            {
                await AddPost();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retrieves a list of all posts from the database.
        /// </summary>
        /// <returns>A list of dictionaries containing post data.</returns>
        public async Task<List<Dictionary<string, object>>> GetPosts()
        {
            await using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();
                string query = @"SELECT 
                                    S01F01, 
                                    E01F02, 
                                    S01F03, 
                                    S01F04, 
                                    S01F06 
                                FROM
                                    Pos01 
                                JOIN 
                                    Use01 
                                ON 
                                    Pos01.S01F02 = Use01.E01F01";
                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                List<Dictionary<string, object>> lstPosts = new List<Dictionary<string, object>>();


                while (objMySqlDataReader.Read())
                {
                    Dictionary<string, object> post = new Dictionary<string, object>();

                    post.Add("S01101", objMySqlDataReader["S01F01"]);
                    post.Add("E01102", objMySqlDataReader["E01F02"]);
                    post.Add("S01103", objMySqlDataReader["S01F03"]);
                    post.Add("S01104", objMySqlDataReader["S01F04"]);
                    post.Add("S01106", objMySqlDataReader["S01F06"]);

                    lstPosts.Add(post);
                }
                return lstPosts;
            }
        }

        /// <summary>
        /// Retrieves a list of posts created by the current user.
        /// </summary>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <returns>A list of dictionaries containing post data.</returns>
        public async Task<List<Dictionary<string, object>>> GetPostByMe(HttpContext httpContext)
        {
            int id = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            await using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();

                string query = @"SELECT 
                                    S01F03, 
                                    S01F04, 
                                    S01F05 
                                FROM 
                                    Pos01 
                                WHERE 
                                    S01F02 = @S01F02";
                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@S01F02", id);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();
                List<Dictionary<string, object>> lstPosts = new List<Dictionary<string, object>>();
                while (objMySqlDataReader.Read())
                {
                    Dictionary<string, object> post = new Dictionary<string, object>();
                    post.Add("S01103", objMySqlDataReader["S01F03"]);
                    post.Add("S01104", objMySqlDataReader["S01F04"]);
                    post.Add("S01105", objMySqlDataReader["S01F05"]);
                    lstPosts.Add(post);
                }
                return lstPosts;
            }
        }

        /// <summary>
        /// Updates a post in the database.
        /// </summary>
        /// <param name="id">The ID of the post to update.</param>
        /// <param name="objDtoPos01">The DTO object containing potentially updated post data (including image).</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID and perform authorization checks.</param>
        /// <returns>True if the post is updated successfully, false otherwise.</returns>
        public async Task<bool> Update(int id, DtoPos01 objDtoPos01, HttpContext httpContext)
        {
            bool userCheck = PreSaveUpdate(id,objDtoPos01, httpContext);

            if (!userCheck)
            {
                return false;
            }

            bool check = objDtoPos01.S01101 != null ? true : false;

            if (check)
            {
                bool isValidated = await ValidationUpdate(objDtoPos01);
                if (isValidated)
                {
                    return UpdatePost(id);
                }
            }
            else
            {
                return UpdatePost(id);
            }
            return false;
        }

        /// <summary>
        /// delete a post from the database.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID and perform authorization checks.</param>
        /// <returns>True if the post is deleted successfully, false otherwise.</returns>
        public bool DeletePost(int id, HttpContext httpContext)
        {
            Pos01 post = GetPostById(id);
            if (post != null)
            {
                int userId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
                if (post.S01F02 != userId)
                {
                    return false;
                }
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    bool isPostDeleted =  db.DeleteById<Pos01>(id) > 0; 
                    if (isPostDeleted)
                    {
                        return DeleteImage(post.S01F03);
                    }
                }
            }
            return false;
        }


        #endregion
    }
}
