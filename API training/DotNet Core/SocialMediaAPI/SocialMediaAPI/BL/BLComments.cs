using AutoMapper;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NLog;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model.Dtos;
using SocialMediaAPI.Model.POCO;
using System.Data;

namespace SocialMediaAPI.BL
{
    /// <summary>
    ///  Implements the ICommentService interface and provides methods for managing comments on posts.
    /// </summary>
    public class BLComments : ICommentService
    {
        #region Private Member
        private readonly string _connectionString;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDbConnectionFactory _dbFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private Com01 _objCom01;
        private Com01 _objUpdateCom01;
        #endregion

        #region Constructor
        public BLComments(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("Default");
            _mapper = mapper;
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }
        #endregion

        #region Private Method

        /// <summary>
        /// Prepares a DtoCom01 object for saving by mapping it to a Com01 entity and setting the current user ID as the comment author.
        /// </summary>
        /// <param name="objDtoCom01">The comment data transfer object.</param>
        /// <param name="httpContext">The HTTP context containing user information.</param>
        private void PreSave(DtoCom01 objDtoCom01, HttpContext httpContext)
        {
            _objCom01 = _mapper.Map<Com01>(objDtoCom01);
            int userId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            _objCom01.M01F03 = userId;
        }

        /// <summary>
        /// Attempts to add a new comment to the database.
        /// </summary>
        /// <returns>True if the comment is added successfully, false otherwise.</returns>
        private bool AddComments()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    return db.Insert<Com01>(_objCom01) > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: add comments :: {ex.Message}");
                return false;
            }
        }


        /// <summary>
        /// Prepares a comment for update by verifying the user ID matches the comment author 
        /// and populating the update data.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="objDtoCom01">The comment data transfer object containing update data.</param>
        /// <param name="httpContext">The HTTP context containing user information.</param>
        /// <returns>True if the comment can be updated, false otherwise.</returns>
        private bool PreSaveUpdate(int id,DtoCom01 objDtoCom01, HttpContext httpContext)
        {
            Com01 objCom01 = GetCommentById(id);
            if (objCom01 == null)
            {
                return false;
            }
            int userid = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);

            if (objCom01.M01F03 != userid)
            {
                return false;
            }

            _objUpdateCom01 = objCom01;
            _objUpdateCom01.M01F04 = objDtoCom01.M01102;
            return true;
        }

        /// <summary>
        /// Attempts to update a comment in the database.
        /// </summary>
        /// <returns>True if the comment is updated successfully, false otherwise.</returns>
        private bool UpdateComment()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    return db.Update<Com01>(_objUpdateCom01) > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: comments update :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets a comment by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve.</param>
        /// <returns>The Com01 entity representing the comment, or null if not found.</returns>
        private Com01 GetCommentById(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<Com01>(id);
            }
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Adds a new comment to the database.
        /// </summary>
        /// <param name="objDtoCom01">The comment data transfer object containing comment data.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <returns>True if the comment is added successfully, false otherwise.</returns>

        public bool Add(DtoCom01 objDtoCom01, HttpContext httpContext)
        {
            PreSave(objDtoCom01, httpContext);
            return AddComments();
        }

        /// <summary>
        /// Deletes a comment from the database.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <returns>True if the comment is deleted successfully, false otherwise.</returns>
        public bool Delete(int id, HttpContext httpContext)
        {
            Com01 objCom01 = GetCommentById(id);
            if (objCom01 != null)
            {
                int userId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);

                if (objCom01.M01F03 != userId)
                {
                    return false;
                }

                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    return db.DeleteById<Com01>(id) > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// Updates a comment in the database.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="objDtoCom01">The comment data transfer object containing update data.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID and perform authorization checks.</param>
        /// <returns>True if the comment is updated successfully, false otherwise.</returns>
        public bool Update(int id, DtoCom01 objDtoCom01, HttpContext httpContext)
        {
            bool userCheck = PreSaveUpdate(id, objDtoCom01, httpContext);

            if (!userCheck)
            {
                return false;
            }

            return UpdateComment();
        }


        /// <summary>
        /// Gets all comments for a specific post.
        /// </summary>
        /// <param name="id">The ID of the post to get comments for.</param>
        /// <returns>A list of dictionaries containing comment information (user name, comment text, etc.).</returns>
        public async Task<List<Dictionary<string, object>>> GetAllCommentsOnPost(int id)
        {
            await using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();
                string query = @"SELECT E01F02,M01F04,M01F05 FROM Com01 JOIN Use01 ON Com01.M01F03 = Use01.E01F01 WHERE M01F02 = @M01F02";

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@M01F02",id);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                List<Dictionary<string, object>> lstCom01 = new List<Dictionary<string, object>>();

                while (objMySqlDataReader.Read())
                {
                    Dictionary<string, object> comment = new Dictionary<string, object>();

                    comment.Add("E01101", objMySqlDataReader["E01F02"]);
                    comment.Add("M01101", objMySqlDataReader["M01F04"]);
                    comment.Add("M01102", objMySqlDataReader["M01F05"]);
                   

                    lstCom01.Add(comment);
                }
                return lstCom01;
            }
        }


        #endregion
    }
}
