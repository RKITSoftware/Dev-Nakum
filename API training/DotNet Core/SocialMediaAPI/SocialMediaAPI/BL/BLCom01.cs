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
    /// <summary>
    /// Implements the ICommentService interface and provides methods for managing comments on posts.
    /// </summary>
    public class BLCOM01 : ICOM01Service
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
        /// create the object of the comment model
        /// </summary>
        private COM01 _objCOM01;

        /// <summary>
        /// Service responsible for validation operations.
        /// </summary>
        private readonly Validation _objValidation;

        /// <summary>
        /// Interface for database operations related to comments.
        /// </summary>
        private readonly IDBCOM01 _objIDBCom01;

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
        /// Initializes a new instance of the BLCOM01 class.
        /// </summary>
        public BLCOM01(IConfiguration configuration, Validation objValidation, IDBCOM01 objIDBCom01, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _objValidation = objValidation;
            _objIDBCom01 = objIDBCom01;
            HttpContext = httpContextAccessor.HttpContext;
        }
        #endregion


        #region Private Method

        /// <summary>
        /// get the user id based on comment id
        /// </summary>
        /// <param name="commentId">comment id</param>
        /// <returns>user id if exist or else -1</returns>
        private int GetUserId(int commentId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var comment = db.SingleById<COM01>(commentId);
                return comment != null ? comment.M01F03 : -1;
            }
        }
        #endregion

        #region Public Method

        /// <summary>
        /// map the dto object to the poco object 
        /// </summary>
        /// <param name="objDTOCOM01">object of the comment</param>
        /// <param name="commentId">comment id</param>
        public void PreSave(DTOCOM01 objDTOCOM01, int commentId = 0)
        {
            _objCOM01 = objDTOCOM01.MapDtoToPoco<DTOCOM01, COM01>(null);
            int userId = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);
            _objCOM01.M01F03 = userId;
            if (OperationType == enmOperationType.E)
            {
                _objCOM01.M01F01 = commentId;
            }
        }

        /// <summary>
        /// validation before add or update comment into database
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();

            if (OperationType == enmOperationType.A)
            {
                // to check whether post is available or not.
                bool isPostExist = _objValidation.IsExist<POS01>(_objCOM01.M01F02, S01 => S01.S01F01);
                if (!isPostExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Post is not exist";
                }
            }
            if (OperationType == enmOperationType.E)
            {
                // to check whether comment is available or not.
                bool isCommentExist = _objValidation.IsExist<COM01>(_objCOM01.M01F01, M01 => M01.M01F01);
                if (!isCommentExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Comment is not exist";
                }
                else
                {
                    int userIdFromComment = GetUserId(_objCOM01.M01F01);
                    if (userIdFromComment != _objCOM01.M01F03)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "you can not update this comment";
                    }
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Add or Update the comment into data base
        /// </summary>
        /// <returns>response model</returns>
        public Response Save()
        {
            objResponse = new Response();

            if (OperationType == enmOperationType.A)
            {
                bool isCommentAdded = false;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    isCommentAdded = db.Insert(_objCOM01) > 0;
                }
                if (!isCommentAdded)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "something went wrong while adding comment";
                }
                else
                {
                    objResponse.Message = "comment is added successfully";
                }
            }
            if (OperationType == enmOperationType.E)
            {
                // for update the only comment's content
                bool isCommentUpdated = _objIDBCom01.UpdateComment(_objCOM01.M01F01, _objCOM01.M01F04);
                if (!isCommentUpdated)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "something went wrong while updating the comment";
                }
                else
                {
                    objResponse.Message = "comment is updated successfully";
                }
            }
            return objResponse;

        }

        /// <summary>
        /// validation before delete the comment
        /// </summary>
        /// <param name="commentId">comment id</param>
        /// <returns>response model</returns>
        public Response ValidationOnDelete(int commentId)
        {
            objResponse = new Response();
            bool isCommentExist = false;
            int userId = 0, userIdFromComment = 0;

            if (OperationType == enmOperationType.D)
            {
                isCommentExist = _objValidation.IsExist<COM01>(commentId, x => x.M01F01);
                if (!isCommentExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "comment is not exist.";
                }
                else
                {
                    userId = Convert.ToInt32(HttpContext.User.FindFirst("id")?.Value);
                    userIdFromComment = GetUserId(commentId);

                    if (userIdFromComment != userId)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "user is not exist.";
                    }
                    else
                    {
                        _objCOM01 = new COM01();
                        _objCOM01.M01F01 = commentId;
                    }
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Deletes a comment from the database.
        /// </summary>
        /// <returns>response model</returns>
        public Response Delete()
        {
            objResponse = new Response();
            bool isCommetDeleted = false;
            if (OperationType == enmOperationType.D)
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    isCommetDeleted = db.DeleteById<COM01>(_objCOM01.M01F01) > 0;
                }
                if (!isCommetDeleted)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "something went wrong while deleting the comment";
                }
                else
                {
                    objResponse.Message = "comment is successfully deleted";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Gets all comments for a specific post.
        /// </summary>
        /// <param name="id">The ID of the post to get comments for.</param>
        /// <returns>response model</returns>
        public Response GetAllCommentsOnPost(int id)
        {
            objResponse = new Response();
            DataTable dtAllCommentsOnPost = _objIDBCom01.GetAllCommentsOnPost(id);
            objResponse.Data = dtAllCommentsOnPost;
            return objResponse;
        }

        #endregion
    }
}
