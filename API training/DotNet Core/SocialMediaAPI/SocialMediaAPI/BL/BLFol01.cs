using Check_Id_Exist;
using NLog;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SocialMediaAPI.Enums;
using SocialMediaAPI.Extension;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.DTOS;
using SocialMediaAPI.Model.POCO;
using System.Data;

namespace SocialMediaAPI.BL
{
    /// <summary>
    /// Implements the IFollowersService interface and provides methods for managing followers.
    /// </summary>
    public class BLFOL01 : IFOL01Service
    {
        #region Private Members

        /// <summary>
        /// Connection string used for database connectivity.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Logger instance for logging operations.
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Database connection factory using ORMLite.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Configuration object for retrieving connection string.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Object representing a follower model.
        /// </summary>
        private FOL01 _objFOL01;

        /// <summary>
        /// Validation service instance for validation operations.
        /// </summary>
        private readonly Validation _objValidation;

        #endregion

        #region Private Properties

        /// <summary>
        /// HttpContext instance for accessing the current HTTP context.
        /// </summary>
        private HttpContext HttpContext { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Type of operation: A - Add, E - Edit, D - Delete.
        /// </summary>
        public enmOperationType OperationType { get; set; }

        #endregion

        #region Public Members

        /// <summary>
        /// Response object to return status and messages.
        /// </summary>
        public Response objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BLFol01 class with required dependencies.
        /// </summary>
        public BLFOL01(IConfiguration configuration, Validation objValidation, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _objValidation = objValidation;
            HttpContext = httpContextAccessor.HttpContext;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks if a following relationship between two users exists in the database.
        /// </summary>
        /// <param name="currentUser">ID of the current logged-in user.</param>
        /// <param name="followingUser">ID of the user being followed.</param>
        /// <returns>True if the relationship exists; otherwise, false.</returns>
        private bool IsFollowingUserExist(int currentUser, int followingUser)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<FOL01>(x => x.L01F02 == currentUser && x.L01F03 == followingUser);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Maps the provided DTO object containing follower data to a Fol01 entity and sets the current user ID (following user) in the entity.
        /// </summary>
        /// <param name="objDTOFOL01">DTO object containing follower data.</param>
        public void PreSave(DTOFOL01 objDTOFOL01)
        {
            if (OperationType == enmOperationType.A)
            {
                _objFOL01 = objDTOFOL01.MapDtoToPoco<DTOFOL01, FOL01>(null);
                int userId = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);
                _objFOL01.L01F02 = userId;
            }
        }

        /// <summary>
        /// Validates the input before adding a record to the database.
        /// </summary>
        /// <returns>Response object containing validation results.</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();

            if (OperationType == enmOperationType.A)
            {
                if (_objFOL01.L01F02 == _objFOL01.L01F03)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "It is not possible to add or remove yourself from the following.";
                    return objResponse;
                }
                bool isUserExist = _objValidation.IsExist<USE01>(_objFOL01.L01F02, x => x.E01F01);
                bool isFollowingUserExist = _objValidation.IsExist<USE01>(_objFOL01.L01F03, x => x.E01F01);

                if (!isUserExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User must be authenticated.";
                }
                else if (!isFollowingUserExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Following user does not exist.";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Inserts a follower record into the database.
        /// </summary>
        /// <returns>Response object indicating the result of the operation.</returns>
        public Response Save()
        {
            objResponse = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                bool isAdded = db.Insert(_objFOL01) > 0;

                if (!isAdded)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Something went wrong while following the user.";
                }
                else
                {
                    objResponse.Message = "Successfully followed the user.";
                }
                return objResponse;
            }
        }

        /// <summary>
        /// Validates the input before removing a user from the following list.
        /// </summary>
        /// <param name="objDTOFOL01">DTO object containing follower data.</param>
        /// <returns>Response object containing validation results.</returns>
        public Response ValidationOnDelete(DTOFOL01 objDTOFOL01)
        {
            objResponse = new Response();

            if (OperationType == enmOperationType.D)
            {
                int userId = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);

                if (objDTOFOL01.L01F03 == userId)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "It is not possible to add or remove yourself from the following.";
                    return objResponse;
                }
                bool isUserExist = _objValidation.IsExist<USE01>(userId, x => x.E01F01);
                bool isFollowingUserExist = _objValidation.IsExist<USE01>(objDTOFOL01.L01F03, x => x.E01F01);

                if (!isUserExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User must be authenticated.";
                }
                else if (!isFollowingUserExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Following user does not exist.";
                }
                else if (IsFollowingUserExist(userId, objDTOFOL01.L01F03))
                {
                    _objFOL01 = new FOL01
                    {
                        L01F02 = userId,
                        L01F03 = objDTOFOL01.L01F03
                    };
                }
                else
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User is not in your following list.";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Removes a follower record from the database.
        /// </summary>
        /// <returns>Response object indicating the result of the operation.</returns>
        public Response Remove()
        {
            objResponse = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                bool isRemoved = db.DeleteWhere<FOL01>("L01F02 = {0} && L01F03 = {1}", new object[] { _objFOL01.L01F02, _objFOL01.L01F03 }) > 0;

                if (!isRemoved)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Something went wrong while removing the following user.";
                }
                else
                {
                    objResponse.Message = "Successfully removed the user.";
                }
                return objResponse;
            }
        }

        #endregion
    }
}
