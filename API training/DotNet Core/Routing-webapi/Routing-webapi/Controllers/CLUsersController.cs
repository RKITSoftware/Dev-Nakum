using Microsoft.AspNetCore.Mvc;
using Routing_webapi.Business_Logic;
using Routing_webapi.Model;

namespace Routing_webapi.Controllers
{
    /// <summary>
    /// handle the user's api call
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLUsersController : ControllerBase
    {
        #region Private Member
        BLUsers _objBLUsers;
        #endregion

        #region Constructor
        public CLUsersController()
        {
            _objBLUsers = new BLUsers();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Add user into list
        /// </summary>
        /// <param name="objUse01">object of the user</param>
        /// <returns>response message</returns>
        [HttpPost]
        public IActionResult AddUser(Use01 objUse01)
        {
            if (!_objBLUsers.AddUser(objUse01))
            {
                return BadRequest("something went wrong");
            }
            else
            {
                return Ok("user added successfully");
            }
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>list of all users</returns>
        [HttpGet]
        public IActionResult GetUsers() { return Ok(_objBLUsers.GetUsers()); }

        /// <summary>
        /// get the user based on user-id
        /// </summary>
        /// <param name="id">userId</param>
        /// <returns>object of the user</returns>
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            Use01 user = _objBLUsers.GetUser(id);
            if(user == null)
            {
                return NotFound("User is not found");
            }
            return Ok(user);
        }
        #endregion
    }
}
