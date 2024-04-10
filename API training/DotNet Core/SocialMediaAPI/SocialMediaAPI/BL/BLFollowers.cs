using AutoMapper;
using NLog;
using Org.BouncyCastle.Asn1.Cmp;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model.Dtos;
using SocialMediaAPI.Model.POCO;
using System.Data;

namespace SocialMediaAPI.BL
{
    /// <summary>
    ///  Implements the IFollowersService interface and provides methods for managing followers
    /// </summary>
    public class BLFollowers : IFollowersService
    {
        #region Private Member
        private readonly string _connectionString;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDbConnectionFactory _dbFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private Fol01 _objFol01;
        #endregion

        #region Constructor
        public BLFollowers(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Default");
            _mapper = mapper;
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }
        #endregion

        #region Private Method

        /// <summary>
        /// Maps the provided DTO object (containing follower data) to a Fol01 entity 
        /// and sets the current user ID (following user) in the Fol01 entity.
        /// </summary>
        /// <param name="objDtoFol01">The DTO object containing follower data.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>

        private void PreSave(DtoFol01 objDtoFol01, HttpContext httpContext)
        {
            _objFol01 = _mapper.Map<Fol01>(objDtoFol01);
            int userId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            _objFol01.L01F02 = userId;
        }

        /// <summary>
        /// Insert a new following record.
        /// </summary>
        /// <returns>True if the following user is added successfully , false otherwise.</returns>
        private bool Following()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    return db.Insert<Fol01>(_objFol01) > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: following :: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Adds a new follower record for the current user following another user.
        /// </summary>
        /// <param name="objDtoFol01">The DTO object containing follower data (potentially containing the following user ID).</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID (for following user).</param>
        /// <returns>True if the follower is added successfully, false otherwise.</returns>

        public bool Add(DtoFol01 objDtoFol01, HttpContext httpContext)
        {
            PreSave(objDtoFol01, httpContext);
            return Following();
        }

        /// <summary>
        /// Removes a follower record where the current user is the following user (unfollows).
        /// </summary>
        /// <returns>True if the follower is deleted successfully, false otherwise.</returns>
        public bool Remove(DtoFol01 objDtoFol01, HttpContext httpContext)
        {
            int followersId = objDtoFol01.L01101;
            int followingId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                int ans = db.DeleteWhere<Fol01>("L01F02 = {0} && L01F03 = {1}", new object[] {followingId,followersId });

                return ans > 0;
            }
        }
        #endregion
    }
}
