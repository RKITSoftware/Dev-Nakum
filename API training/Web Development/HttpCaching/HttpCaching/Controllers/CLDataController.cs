using HttpCaching.BL;
using System.Web.Http;

namespace HttpCaching.Controllers
{
    /// <summary>
    /// Operation related on cache
    /// </summary>
    public class CLDataController : ApiController
    {
        #region Private Member
        /// <summary>
        /// create the user service object
        /// </summary>
        private readonly BLUsers _objBLUsers;
        #endregion


        #region Constructor
        /// <summary>
        /// initialize the object
        /// </summary>
        public CLDataController()
        {
            _objBLUsers = new BLUsers();
        }
        #endregion
        /// <summary>
        ///  Get the user data and store into cache
        /// </summary>
        /// <returns>users data</returns>
        [HttpGet]
        [Route("api/data")]
        public IHttpActionResult GetResult()
        {
            string[][] getUserData = _objBLUsers.DisplayAndAddCache();
            return Ok(getUserData);
        }

        /// <summary>
        /// Get the result by id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user's data based on user id</returns>
        [HttpGet]
        [Route("api/data/{id}")]
        public IHttpActionResult GetResultById(string id)
        {
            object data = _objBLUsers.GetUserById(id);
            return Ok(data);
        }

        /// <summary>
        /// Get the cache data
        /// </summary>
        /// <returns>available cache data</returns>
        [HttpGet]
        [Route("api/data/all")]
        public IHttpActionResult GetAllCacheData()
        {
            return Ok(BLCache.GetAllCache());
        }
    }
}
