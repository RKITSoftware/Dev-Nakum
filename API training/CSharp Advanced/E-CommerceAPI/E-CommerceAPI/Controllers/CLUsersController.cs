using E_CommerceAPI.Attributes;
using E_CommerceAPI.BL;
using E_CommerceAPI.Models;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Routing;

namespace E_CommerceAPI.Controllers
{
    public class CLUsersController : ApiController
    {
        #region Private Member
        private BLUsers _objBLUsers;
        #endregion

        #region Constructor
        public CLUsersController()
        {
            _objBLUsers = new BLUsers();
        }
        #endregion

        #region Private Method
        private string GetCurrentUser()
        {
            ClaimsPrincipal currentUser = User as ClaimsPrincipal;
            if (currentUser != null)
            {
                string userId = currentUser.FindFirst("Id")?.Value;

                return userId;
            }
            return null;
        }
        #endregion

        #region Public Method

        [HttpPost]
        [Route("api/users/signup")]
        public IHttpActionResult SignUp([FromBody] Use01 objUse01)
        {
            Use01 user = _objBLUsers.SignUp(objUse01);
            if(user == null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(user);
        }


        [HttpPost]
        [Route("api/users/login")]
        public IHttpActionResult Login([FromBody] Use01 objUse01)
        {
            object user = _objBLUsers.Login(objUse01);
            if (user == null)
            {
                return BadRequest("Incorrect email or password");
            }
            return Ok(user);
        }

        [JwtAuthorization]
        [HttpPut]
        [Route("api/users/me")]
        public IHttpActionResult UpdateUserName([FromBody] Use01 objUse01)
        {
            string id = GetCurrentUser();
            object user = _objBLUsers.UpdateUserName(id, objUse01);
            if (user == null)
            {
                return BadRequest("Incorrect email or password");
            }
            return Ok("Successfully changed username");
        }


        [JwtAuthorization]
        [HttpPut]
        [Route("api/users/password")]
        public IHttpActionResult ChangePassword([FromBody] JObject objPassword)
        {
            string id = GetCurrentUser();
            string user = _objBLUsers.ChangePassword(id, objPassword);
           
            return Ok(user);
        }


        [JwtAuthorization]
        [Authorize(Roles ="Admin")]
        [HttpDelete]
        [Route("api/users/")]
        public IHttpActionResult DeleteUser([FromBody] Use01 objUse01)
        {
            bool user = _objBLUsers.DeleteUser(objUse01.E01F01);
            if (user)
            {
                return Ok("User deleted successfully");
            }
            else
            {
                return BadRequest("User not found");
            }
        }

        [JwtAuthorization]
        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("api/users")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(_objBLUsers.GetAllUsers());
        }

        [JwtAuthorization]
        [HttpGet]
        [Route("api/users/me")]
        public IHttpActionResult GetUsers()
        {
            string id = GetCurrentUser();
            return Ok(_objBLUsers.GetUsers(id));
        }
        #endregion
    }
}
