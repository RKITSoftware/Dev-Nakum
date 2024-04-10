using AutoMapper;
using MySql.Data.MySqlClient;
using NLog;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model.Dtos;
using SocialMediaAPI.Model.POCO;
using System.Data;


namespace SocialMediaAPI.BL
{
    /// <summary>
    ///  Implements the IUserService interface and provides methods for managing users
    /// </summary>
    public class BLUsers : IUserService
    {
        #region Private Member
        private readonly string _connectionString;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDbConnectionFactory _dbFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private Use01 _objUse01;
        #endregion

        #region Constrctor
        public BLUsers(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _configuration = configuration;
            _mapper = mapper;
        }
        #endregion

        #region Private Method

        /// <summary>
        /// Maps the provided DTO object containing user data (DtoUse01) to a Use01 model object.
        /// </summary>
        /// <param name="objDtoUse01">The DTO object containing user data.</param>
        private void PreSave(DtoUse01 objDtoUse01)
        {
            _objUse01 = _mapper.Map<Use01>(objDtoUse01);
        }

        /// <summary>
        /// Performs asynchronous validation for user signup.
        /// </summary>
        /// <param name="objDtoUse01">The DTO object containing user data.</param>
        /// <returns>True if validation is successful, false otherwise.</returns>
        private async Task<bool> Validation(DtoUse01 objDtoUse01)
        {
            try
            {
                string imageUrl = await UploadImage(objDtoUse01.E01104);
                _objUse01.E01F05 = imageUrl;

                BLHashing objBLHashing = new BLHashing();
                _objUse01.E01F04 = objBLHashing.HashPassword(_objUse01.E01F04);

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: user profile picture upload or password hash :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Inserts the user data from the temporary _objUse01 object into the database table Use01.
        /// </summary>
        /// <returns>True if the user is successfully added to the database, false otherwise.</returns>
        private bool SignUp()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objUse01);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Uploads an image file to the configured uploads folder and returns the URL for the uploaded image.
        /// </summary>
        /// <param name="imageFile">The IFormFile object representing the image to upload.</param>
        /// <returns>The relative or absolute URL of the uploaded image.</returns>
        private async Task<string> UploadImage(IFormFile imageFile)
        {
            
            // (Example using a file system for simplicity)
            string uploadsFolder = Path.Combine(_configuration.GetValue<string>("Uploads:FolderPath"), "");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = _objUse01.E01F02 + Path.GetExtension(imageFile.FileName);
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
        /// Performs user login by validating username and password.
        /// </summary>
        /// <param name="objUse01">A JsonObject containing username and password information.</param>
        /// <returns>An object containing JWT token and user name on successful login, null otherwise.</returns>
        public object Login(JsonObject objUse01)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    BLHashing objBLHashing = new BLHashing();
                    Use01 user = db.Single<Use01>(u => u.E01F02 == objUse01["E01F02"]);

                    if (user != null)
                    {
                        bool isValidated = objBLHashing.Verify(objUse01["E01F04"], user.E01F04);

                        if (isValidated)
                        {
                            // add jwt and claims
                            BLAuth objBLAuth = new BLAuth(_configuration);
                            string jwt = objBLAuth.GenerateJWT(user.E01F01, user.E01F02, user.E01F03, user.E01F07);

                            return new
                            {
                                jwt,
                                user.E01F07
                            };
                        }

                    }
                    _logger.Error("Username or password is incorrect");
                    return null;

                }
            }
            catch (Exception ex)
            {
                _logger.Error("something went wrong while logging", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a list of all users from the database.
        /// </summary>
        /// <returns>A list of dictionaries containing user data.</returns>
        public List<Dictionary<string, object>> GetUsers()
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();
                string query = @"select 
                                    E01F01,
                                    E01F02,
                                    E01F03,
                                    E01F05,
                                    E01F06
                                from 
                                    use01";

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                List<Dictionary<string, object>> lstUsers = new List<Dictionary<string, object>>();
                while (objMySqlDataReader.Read())
                {
                    Dictionary<string, object> user = new Dictionary<string, object>();
                    user.Add("E01101", objMySqlDataReader["E01F01"]);
                    user.Add("E01102", objMySqlDataReader["E01F02"]);
                    user.Add("E01103", objMySqlDataReader["E01F03"]);
                    user.Add("E01105", objMySqlDataReader["E01F05"]);
                    user.Add("E01106", objMySqlDataReader["E01F06"]);
                    lstUsers.Add(user);
                }

                return lstUsers;
            }
        }

        /// <summary>
        /// Adds a new user asynchronously.
        /// </summary>
        /// <param name="objDtoUse01">The DtoUse01 object containing user data.</param>
        /// <returns>True if the user is successfully added, false otherwise.</returns>
        public async Task<bool> Add(DtoUse01 objDtoUse01)
        {
            PreSave(objDtoUse01);
            bool isValidate = await Validation(objDtoUse01);
            if (isValidate)
            {
                return SignUp();
            }
            return false;
        }

        /// <summary>
        /// Retrieves user details based on the user ID from the HTTP context.
        /// </summary>
        /// <param name="httpContext">The HttpContext containing user information.</param>
        /// <returns>A dictionary containing user details.</returns>
        public async  Task<Dictionary<string, object>> GetUserDetails(HttpContext httpContext)
        {
            int id = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            await using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();

                string query = @"SELECT 
                                    E01F02,
                                    E01F03,
                                    E01F05,
                                    E01F06 
                                FROM 
                                    Use01 
                                WHERE 
                                    E01F01 = @E01F01";
                MySqlCommand objMySqlCommand = new MySqlCommand(query,objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@E01F01",id);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                Dictionary<string, object> userDetails = new Dictionary<string, object>();

                while(objMySqlDataReader.Read())
                {
                    userDetails.Add("E01F02", objMySqlDataReader["E01F02"]);
                    userDetails.Add("E01F03", objMySqlDataReader["E01F03"]);
                    userDetails.Add("E01F05", objMySqlDataReader["E01F05"]);
                    userDetails.Add("E01F06", objMySqlDataReader["E01F06"]);
                }
                return userDetails;
            }
        }

        /// <summary>
        /// Retrieves a dictionary containing a list of usernames followed by the current user.
        /// </summary>
        /// <param name="httpContext">The HttpContext containing user information.</param>
        /// <returns>A dictionary with a key "Following" and a HashSet containing usernames of users followed by the current user.</returns>
        public async Task<Dictionary<string, HashSet<string>>> GetFollowing(HttpContext httpContext)
        {
            int id = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            await using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();
                string query = @"SELECT 
                                    E01F02
                                FROM
                                    Fol01 L01
                                        JOIN
                                    Use01 E01 ON L01.L01F03 = E01.E01F01
                                WHERE
                                    L01.L01F02 = @L01F02";

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@L01F02", id);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                Dictionary<string, HashSet<string>> dictUsers = new Dictionary<string, HashSet<string>>();

                    HashSet<string> set = new HashSet<string>();
                while (objMySqlDataReader.Read())
                {
                    set.Add(objMySqlDataReader.GetString("E01F02"));
                    
                }
                dictUsers.Add("Following", set);
                return dictUsers;    
            }
        }


        #endregion
    }
}
