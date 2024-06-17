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
    ///  Implements the ICommentService interface and provides methods for managing comments on posts.
    /// </summary>
    public class BLCom01 : ICom01Service
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
        private Com01 _objCom01;

        private readonly Validation _objValidation;
        private readonly IDBCom01 _objIDBCom01;

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
        public BLCom01(IConfiguration configuration, Validation objValidation, IDBCom01 objIDBCom01, IHttpContextAccessor httpContextAccessor)
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
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    var comment = db.SingleById<Com01>(commentId);
                    return comment != null ? comment.M01F03 : -1;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: GetUserId :: {ex.Message}");
                return -1;
            }
        }
        #endregion

        #region Public Method

        /// <summary>
        /// map the dto object to the poco object 
        /// </summary>
        /// <param name="objDtoCom01">object of the comment</param>
        /// <param name="httpContext">current context for getting the user id</param>
        /// <param name="commentId">comment id</param>
        public void PreSave(DtoCom01 objDtoCom01,  int commentId = 0)
        {
            _objCom01 = objDtoCom01.MapDtoToPoco<DtoCom01, Com01>(null);
            int userId = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);
            _objCom01.M01F03 = userId;

            //if (OperationType == enmOperationType.A)
            //{
            //    _objCom01.M01F03 = userId;
            //}
            if (OperationType == enmOperationType.E)
            {
                _objCom01.M01F01 = commentId;

                //_objCom01 = objDtoCom01.MapDtoToPoco<DtoCom01, Com01>(_objCom01);
            }
        }

        /// <summary>
        /// validation before add or update comment into database
        /// </summary>
        /// <param name="objValidation">service of validation</param>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();

            if (OperationType == enmOperationType.A)
            {
                // to check whether post is available or not.
                // use FromServices - resolve the service dependency directly in action method
                bool isPostExist = _objValidation.IsExist<Pos01>(_objCom01.M01F02, x => x.S01F01);
                if (!isPostExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Post is not exist";
                }
            }
            if (OperationType == enmOperationType.E)
            {
                // to check whether comment is available or not.
                bool isCommentExist = _objValidation.IsExist<Com01>(_objCom01.M01F01, x => x.M01F01);
                if (!isCommentExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Comment is not exist";
                }
                else
                {
                    int userIdFromComment = GetUserId(_objCom01.M01F01);
                    if (userIdFromComment != _objCom01.M01F03)
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
            try
            {
                if (OperationType == enmOperationType.A)
                {
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        bool isCommentAdded = db.Insert(_objCom01) > 0;
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
                }
                if (OperationType == enmOperationType.E)
                {
                    // for update the only comment's content
                    bool isCommentUpdated = _objIDBCom01.UpdateComment(_objCom01.M01F01, _objCom01.M01F04);
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
            catch (Exception ex)
            {
                _logger.Error($"exception :: add comments :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }


        /// <summary>
        /// validation before delete the comment
        /// </summary>
        /// <param name="commentId">comment id</param>
        /// <param name="httpContext">current context for getting the user id</param>
        /// <param name="objValidation">object of the validation</param>
        /// <returns>response model</returns>
        public Response ValidationOnDelete(int commentId)
        {
            objResponse = new Response();
            if (OperationType == enmOperationType.D)
            {
                bool isCommentExist = _objValidation.IsExist<Com01>(commentId, x => x.M01F01);
                if (!isCommentExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "comment is not exist.";
                }
                else
                {
                    int userId = Convert.ToInt32(HttpContext.User.FindFirst("id")?.Value);
                    int userIdFromComment = GetUserId(commentId);

                    if (userIdFromComment != userId)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "user is not exist.";
                    }
                    else
                    {
                        _objCom01 = new Com01();
                        _objCom01.M01F01 = commentId;
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
            try
            {
                if (OperationType == enmOperationType.D)
                {
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        bool isCommetDeleted = db.DeleteById<Com01>(_objCom01.M01F01) > 0;
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
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: add comments :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }

        }

        /// <summary>
        /// Gets all comments for a specific post.
        /// </summary>
        /// <param name="id">The ID of the post to get comments for.</param>
        /// <returns>response model</returns>
        public async Task<Response> GetAllCommentsOnPost(int id)
        {
            objResponse = new Response();
            DataTable dtAllCommentsOnPost = await _objIDBCom01.GetAllCommentsOnPost(id);
            objResponse.Data = dtAllCommentsOnPost;
            return objResponse;
        }

        #endregion
    }
}
