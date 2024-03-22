using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware___webapi.Business_Logic;
using Middleware___webapi.Model;

namespace Middleware___webapi.Controllers
{
    /// <summary>
    /// user's controller that manage the user's api
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class CLUsersController : ControllerBase
    {
        #region Private Member
        BLUsers _objBLUsers;
        #endregion

        #region Controller
        public CLUsersController()
        {
            _objBLUsers = new BLUsers();
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Add users into list
        /// </summary>
        /// <param name="objUse01">user's object</param>
        /// <returns>response message based on user added or not</returns>
        [HttpPost]
        public IActionResult AddUser([FromBody] Use01 objUse01)
        {
            bool user = _objBLUsers.AddUsers(objUse01);
            
            return Ok("user added successfully");
        }

        /// <summary>
        /// Get the list of all the user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_objBLUsers.GetAllUsers());
        }

        /// <summary>
        /// To validate the username and password from header
        /// </summary>
        /// <returns>successfully message</returns>
        [HttpGet]
        [Route("validate")]
        public IActionResult UserDetails()
        {
            return Ok("Successfully entered username and password");
        }
        #endregion
    }
}
