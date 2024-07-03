using Filter.Business_Logic;
using Filter.Filter;
using Filter.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Filter.Controllers
{
    /// <summary>
    /// Manage the users api
    /// </summary>

    [Route("api/users")]
    [ApiController]
    [ResultFilter("Controller users")]
    [ActionAsyncFilter("Controller users")]
    public class CLUsersController : ControllerBase
    {
        #region Private Member
        /// <summary>
        /// create the object of thr user services
        /// </summary>
        private readonly BLUsers _objBLUsers;
        #endregion

        #region Contructor

        /// <summary>
        /// initialize the object of the user services
        /// </summary>
        public CLUsersController()
        {
            _objBLUsers = new BLUsers();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Gets the current user's ID from the claims.
        /// </summary>
        /// <returns>The current user's ID or 0 if not found.</returns>
        private int GetCurrentUser()
        {
            ClaimsPrincipal currentUser = User as ClaimsPrincipal;
            if (currentUser != null)
            {
                string userId = currentUser.FindFirst("Id")?.Value;

                return Convert.ToInt32(userId);
            }
            return 0;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// register the user
        /// </summary>
        /// <param name="objUse01">object of the user</param>
        /// <returns>response message</returns>
        [HttpPost("signup")]
        public IActionResult SignUp(Use01 objUse01)
        {
            _objBLUsers.SignUp(objUse01);
            return Ok("User added successfully");
        }

        /// <summary>
        /// login the user
        /// </summary>
        /// <param name="objUse01">object of the user</param>
        /// <returns>response message</returns>
        [HttpPost("login")]
        public IActionResult Login(Use01 objUse01)
        {
            object user = _objBLUsers.Login(objUse01);

            if(user==null)
            {
                return BadRequest("Incorrect username and password");
            }
            return Ok(user);
        }

        /// <summary>
        /// get user details based on user login
        /// </summary>
        /// <returns>user details</returns>
        [Authorize]
        //[AuthorizationFilter]
        [HttpGet("details")]
        public IActionResult UserDetails()
        {
            int id = GetCurrentUser();
            return Ok(_objBLUsers.UserDetails(id));
        }

        /// <summary>
        /// get all users details
        /// </summary>
        /// <returns>list of all the users</returns>
        [Authorize]
        //[AuthorizationFilter]
        [HttpGet]
        public IActionResult AllUser()
        {
            return Ok(_objBLUsers.AllUser());
        }
        #endregion
    }
}
