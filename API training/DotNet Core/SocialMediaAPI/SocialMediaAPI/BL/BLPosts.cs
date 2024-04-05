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
        void PreSave(DtoPos01 objDtoPos01, HttpContext httpContext)
        {
            _objPos01 = _mapper.Map<Pos01>(objDtoPos01);
            var userid = httpContext.User.FindFirst("Id");
            _objPos01.S01F02 = Convert.ToInt32(userid.Value);
        }
        private async Task<bool> Validation(DtoPos01 objDtoPos01)
        {
            try
            {
                string imageUrl = await UploadImage(objDtoPos01.S01101);
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
       
        private async Task<string> UploadImage(IFormFile imageFile)
        {
            string uploadFolder = Path.Combine(_configuration.GetValue<string>("Uploads:PostFolderPath"), "");

            if(!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            
            Guid guid = Guid.NewGuid();

            string fileName = _objPos01.S01F02.ToString() +"_"+ guid.ToString("N") + Path.GetExtension(imageFile.FileName);

            string filePath = Path.Combine(uploadFolder, fileName);


            await using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("/Upload/Post", fileName);
        }
       
        private async Task<bool> AddPost()
        {
            try
            {
                using(IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Insert<Pos01>(_objPos01);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: post add :: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Public Method
        public async Task<bool> Add(DtoPos01 objDtoPos01, HttpContext httpContext)
        {
            PreSave(objDtoPos01,httpContext);
            bool isValidated = await Validation(objDtoPos01);
            if(isValidated)
            {
                AddPost();
                return true;
            }
            return false;
        }

        public async Task<List<Dictionary<string, object>>> GetPosts()
        {
            await using (MySqlConnection objMySqlConnection = new MySqlConnection())
            {
                objMySqlConnection.Open();
                string query = @"";
                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                List<Dictionary<string, object>> posts = new List<Dictionary<string, object>>();

                return posts;
            }
        }
        #endregion
    }
}
