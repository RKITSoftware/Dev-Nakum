using Authentication_Authorization.Basic_Auth;
using Authentication_Authorization.BL;
using System.Net;
using System.Web.Http;

namespace Authentication_Authorization.Controllers
{
    /// <summary>
    /// Manage all the user 
    /// Based on roles - executes the request 
    /// </summary>
    [Authentication]
    public class CLUsersController : ApiController
    {
        #region Private Member
        /// <summary>
        /// Create the object of user' services
        /// </summary>
        private readonly BLUsers _objBLUsers;
        #endregion

        #region Constructor

        /// <summary>
        /// to create instance of user's services 
        /// </summary>
        public CLUsersController()
        {
            _objBLUsers = new BLUsers();
        }
        #endregion


        #region Public Method
        /// <summary>
        /// get all the user list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users")]
        [Authorization(Roles = "admin")]
        public IHttpActionResult GetAllUser()
        {
            return Ok(_objBLUsers.GetAllUsers());
        }
        #endregion
    }
}
