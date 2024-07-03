using Dependency_Injection.Interface;
using Dependency_Injection.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dependency_Injection.Controllers
{
    /// <summary>
    /// controller can manage the users related api
    /// </summary>
    /// 
    [Route("api/users")]
    [ApiController]
    public class CLUsersController : ControllerBase
    {
        #region Private member

        /// <summary>
        /// create the object of the user interface
        /// </summary>
        private readonly IUsers _users;
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the object of the user interface
        /// </summary>
        /// <param name="users"></param>
        public CLUsersController(IUsers users)
        {
            _users = users;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Add user's details
        /// </summary>
        /// <param name="objUse01">object of the user</param>
        /// <returns>response message</returns>
        [HttpPost]
        public IActionResult AddUsers(Use01 objUse01)
        {
            bool user = _users.AddUsers(objUse01);
            if (user)
            {
                return Ok("User added successfully");
            }
            return BadRequest("Something went wrong");
        }

        /// <summary>
        /// Get All the user's details
        /// </summary>
        /// <returns>list of all the users</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_users.GetAllUsers());
        }
        #endregion
    }
}
