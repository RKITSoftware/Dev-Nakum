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
    ///  Implements the IUserService interface and provides methods for managing users
    /// </summary>
    public class BLUse01 : IUse01Service
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

        private readonly IDBUse01 _objIDBUse01;

        /// <summary>
        /// create the object of the user model
        /// </summary>
        private Use01 _objUse01;
        #endregion

        #region Private Property
        /// <summary>
        /// set the current Http Context to get the user id
        /// </summary>
        private HttpContext HttpContext { get; set; }
        #endregion

        #region Public Properties

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
        public BLUse01(IConfiguration configuration, IDBUse01 dbUse01, IHttpContextAccessor httpContextAccessor)
        {
            // Injects IConfiguration for accessing application settings
            _configuration = configuration;

            // Retrieves the connection string named "Default" from configuration
            _connectionString = _configuration.GetConnectionString("Default");

            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _objIDBUse01 = dbUse01;
            HttpContext = httpContextAccessor.HttpContext;
        }

        ///// <summary>
        ///// for initialize the operation from CL 
        ///// </summary>
        //public BLUsers() { }
        #endregion


        #region Private Method
        /// <summary>
        /// Uploads an image file to the configured uploads folder and returns the URL for the uploaded image.
        /// </summary>
        /// <param name="imageFile">The IFormFile object representing the image to upload.</param>
        /// <returns>The relative or absolute URL of the uploaded image.</returns>
        private async Task<string> UploadImage(IFormFile imageFile, string username = "")
        {
            string uploadsFolder = Path.Combine(_configuration.GetValue<string>("Uploads:FolderPath"), "");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string name = "";
            if (username == null)
            {
                name = _objUse01.E01F02;
            }
            else
            {
                name = username;
            }
            string uniqueFileName = name + Path.GetExtension(imageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            // Return the relative or absolute URL of the uploaded image
            return Path.Combine("/Upload/ProfilePicture", uniqueFileName);  // Adjust path based on your setup
        }
        #endregion


        #region Public Method
        /// <summary>
        /// Maps the provided DTO object containing user data (DtoUse01) to a Use01 model object.
        /// </summary>
        /// <param name="objDtoUse01">The DTO object containing user data.</param>
        public async Task PreSave(DtoUse01 objDtoUse01)
        {
            if (OperationType == enmOperationType.A)
            {
                try
                {
                    _objUse01 = objDtoUse01.MapDtoToPoco<DtoUse01, Use01>(null);
                    string imageUrl = await UploadImage(objDtoUse01.E01F05, objDtoUse01.E01F02);
                    _objUse01.E01F05 = imageUrl;
                }
                catch (Exception ex)
                {
                    _logger.Error($"exception :: user profile picture :: {ex.Message}");
                }
            }
        }

        /// <summary>
        ///  validation for user signup.
        /// </summary>
        /// <param name="objDtoUse01">The DTO object containing user data.</param>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();

            try
            {
                //string imageUrl = await UploadImage(objDtoUse01.E01F05);
                //_objUse01.E01F05 = imageUrl;

                BLHashing objBLHashing = new BLHashing();
                _objUse01.E01F04 = objBLHashing.HashPassword(_objUse01.E01F04);

            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: password hash :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
            }
            return objResponse;
        }
        /// <summary>
        /// Inserts the user data from the temporary _objUse01 object into the database table Use01.
        /// </summary>
        /// <returns>response model</returns>
        public Response Save()
        {
            objResponse = new Response();

            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objUse01);
                    objResponse.Message = "User is added successfully";
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
            }
            return objResponse;
        }

        /// <summary>
        /// Performs user login by validating username and password.
        /// </summary>
        /// <param name="objDtoUse01">user object - contains the username and password</param>
        /// <returns>Response model</returns>
        public Response Login(DtoUse01 objDtoUse01)
        {
            objResponse = new Response();
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    BLHashing objBLHashing = new BLHashing();
                    Use01 user = db.Single<Use01>(u => u.E01F02 == objDtoUse01.E01F02);

                    if (user != null)
                    {
                        bool isValidated = objBLHashing.Verify(objDtoUse01.E01F04, user.E01F04);

                        if (isValidated)
                        {
                            // add jwt and claims
                            BLAuth objBLAuth = new BLAuth(_configuration);
                            string jwt = objBLAuth.GenerateJWT(user.E01F01, user.E01F02, user.E01F03, user.E01F07);

                            objResponse.Data = new
                            {
                                jwt,
                                user.E01F07
                            };
                        }
                        else
                        {
                            _logger.Error("Username or password is incorrect");

                            objResponse.IsError = true;
                            objResponse.Message = "Username or password is incorrect";
                        }
                    }
                    else
                    {
                        _logger.Error("Username or password is incorrect");

                        objResponse.IsError = true;
                        objResponse.Message = "Username or password is incorrect";
                    }
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("something went wrong while logging", ex.Message);
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }

        }

        /// <summary>
        /// Retrieves a list of all users from the database.
        /// </summary>
        /// <returns>response model</returns>
        public async Task<Response> GetUsers()
        {
            objResponse = new Response();
            try
            {
                DataTable dtGetUsers = await _objIDBUse01.GetUsers();
                objResponse.Data = dtGetUsers;
                return objResponse;
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// Retrieves user details based on the user ID from the HTTP context.
        /// </summary>
        /// <returns>response model containing user details.</returns>
        public Response GetUserDetails()
        {
            objResponse = new Response();

            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);
            DataTable dtGetUserDetail =  _objIDBUse01.GetUserDetails(id);
            objResponse.Data = dtGetUserDetail;
            return objResponse;
        }

        /// <summary>
        /// Retrieves a list of usernames followed by the current user.
        /// </summary>
        /// <returns>response model</returns>
        public  Response GetFollowing()
        {
            objResponse = new Response();
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id")?.Value);
            DataTable dtFollowingDetails =  _objIDBUse01.GetFollowing(id);
            objResponse.Data = dtFollowingDetails;
            return objResponse;
        }
        #endregion
    }
}