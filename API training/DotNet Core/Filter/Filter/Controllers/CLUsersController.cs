using Filter.Business_Logic;
using Filter.Filter;
using Filter.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Filter.Controllers
{
    [Route("api/users")]
    [ApiController]
    [ResultFilter("Controller")]
    [ActionFilter("Controller")]

    public class CLUsersController : ControllerBase
    {
        #region Private Member
        private BLUsers _objBLUsers;
        #endregion

        #region Contructor
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

        [HttpPost("signup")]

        public IActionResult SignUp(Use01 objUse01)
        {
            _objBLUsers.SignUp(objUse01);
            return Ok("User added successfully");
        }

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


        [AuthorizationFilter]
        [HttpGet("me")]
        public IActionResult UserDetails()
        {
            int id = GetCurrentUser();
            return Ok(_objBLUsers.UserDetails(id));
        }

        [AuthorizationFilter]
        [HttpGet]
        public IActionResult AllUser()
        {
            return Ok(_objBLUsers.AllUser());
        }
        #endregion
    }
}
