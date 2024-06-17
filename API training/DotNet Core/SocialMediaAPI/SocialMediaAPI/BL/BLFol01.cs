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
    ///  Implements the IFollowersService interface and provides methods for managing followers
    /// </summary>
    public class BLFol01 : IFol01Service
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
        /// create the object of the following model
        /// </summary>
        private Fol01 _objFol01;


        private readonly Validation _objValidation;
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
        public BLFol01(IConfiguration configuration, Validation objValidation, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _objValidation = objValidation;
            HttpContext = httpContextAccessor.HttpContext;
        }
        #endregion

        #region Private Method
        /// <summary>
        /// to check whether record is exist or not
        /// </summary>
        /// <param name="currentUser">current logged in user</param>
        /// <param name="followingUser">following user</param>
        /// <returns>true if record found or else false</returns>
        private bool IsFollowingUserExist(int currentUser, int followingUser)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<Fol01>(x => x.L01F02 == currentUser && x.L01F03 == followingUser);
            }
        } 
        #endregion

        #region Public Method

        /// <summary>
        /// Maps the provided DTO object (containing follower data) to a Fol01 entity 
        /// and sets the current user ID (following user) in the Fol01 entity.
        /// </summary>
        /// <param name="objDtoFol01">The DTO object containing follower data.</param>

        public void PreSave(DtoFol01 objDtoFol01)
        {
            if (OperationType == enmOperationType.A)
            {
                _objFol01 = objDtoFol01.MapDtoToPoco<DtoFol01, Fol01>(null);
                int userId = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);
                _objFol01.L01F02 = userId; 
            }
        }

        /// <summary>
        /// validation before adding the record into database
        /// </summary>
        /// <param name="objValidation">object of the validation</param>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();

            if (OperationType == enmOperationType.A)
            {
                if(_objFol01.L01F02 == _objFol01.L01F03)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "It is not possible to add or remove your self from the following";
                    return objResponse;
                }
                bool isUserExist = _objValidation.IsExist<Use01>(_objFol01.L01F02, x => x.E01F01);  
                bool isFollowingUserExist = _objValidation.IsExist<Use01>(_objFol01.L01F03, x => x.E01F01);
                
                if (!isUserExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User must be authenticated";
                }
                else if (!isFollowingUserExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Following user is not exist";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// insert the record into database
        /// </summary>
        /// <returns>response model</returns>
        public Response Save()
        {
            objResponse = new Response();
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    bool isAdded = db.Insert(_objFol01) > 0;

                    if(!isAdded)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "something went wrong while following the user";
                    }
                    else
                    {
                        objResponse.Message = "successfully following the user";
                    }
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: following :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// validation before removing the user from following list
        /// </summary>
        /// <param name="objDtoFol01">dto object of the following</param>
        /// <returns>response model</returns>
        public Response ValidationOnDelete(DtoFol01 objDtoFol01)
        {
            objResponse = new Response();

            if (OperationType == enmOperationType.D)
            {
                int userId = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);

                if(objDtoFol01.L01F03 == userId)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "It is not possible to add or remove your self from the following";
                    return objResponse;
                }
                bool isUserExist = _objValidation.IsExist<Use01>(userId, x => x.E01F01);
                bool isFollowingUserExist = _objValidation.IsExist<Use01>(objDtoFol01.L01F03, x => x.E01F01);

                if (!isUserExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User must be authenticated";
                }
                else if (!isFollowingUserExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Following user is not exist";
                }
                else if( IsFollowingUserExist(userId,objDtoFol01.L01F03) )
                {
                    _objFol01 = new Fol01
                    {
                        L01F02 = userId,
                        L01F03 = objDtoFol01.L01F03
                    };
                }
                else
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User is not exist in your following list";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Removes a follower record where the current user is the following user (unfollows).
        /// </summary>
        /// <returns>response model</returns>
        public Response Remove()
        {
            objResponse = new Response();
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    bool isRemoved = db.DeleteWhere<Fol01>("L01F02 = {0} && L01F03 = {1}", new object[] { _objFol01.L01F02, _objFol01.L01F03 }) > 0;

                    if (!isRemoved)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "something went wrong while remove the following user";
                    }
                    else
                    {
                        objResponse.Message = "successfully removed the user";
                    }
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: following :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }
        #endregion
    }
}