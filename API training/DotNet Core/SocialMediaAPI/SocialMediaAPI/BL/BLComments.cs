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

        private void PreSave(DtoCom01 objDtoCom01, HttpContext httpContext)
        {
            _objCom01 = _mapper.Map<Com01>(objDtoCom01);
            int userId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            _objCom01.M01F03 = userId;
        }

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
        private Com01 GetCommentById(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<Com01>(id);
            }
        }
        #endregion

        #region Public Method
        public bool Add(DtoCom01 objDtoCom01, HttpContext httpContext)
        {
            PreSave(objDtoCom01, httpContext);
            return AddComments();
        }

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

        public bool Update(int id, DtoCom01 objDtoCom01, HttpContext httpContext)
        {
            bool userCheck = PreSaveUpdate(id, objDtoCom01, httpContext);

            if (!userCheck)
            {
                return false;
            }

            return UpdateComment();
        }

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
