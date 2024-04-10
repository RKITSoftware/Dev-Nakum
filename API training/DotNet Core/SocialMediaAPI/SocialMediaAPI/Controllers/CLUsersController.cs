using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ServiceStack.Text;
using SocialMediaAPI.Filter;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;
using SocialMediaAPI.Model.POCO;
using System.Data;
using System.Security.Claims;

namespace SocialMediaAPI.Controllers
{
    /// <summary>
    /// for handling user-related operations in the SocialMediaAPI.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class CLUsersController : ControllerBase
    {
        #region Private Member

        /// <summary>
        /// The injected IUserService dependency for interacting with user logic.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// The logger instance for logging messages.
        /// </summary>
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for CLUsersController that injects the IUserService dependency.
        /// Logs a message upon construction.
        /// </summary>
        /// <param name="userService">The IUserService instance to use.</param>
        public CLUsersController(IUserService userService)
        {
            _userService = userService;
            _logger.Info("User controller constructor called");
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="objDtoUse01">The DTO object representing the user data.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpPost("signup")]
        [UserValidationFilter]
        public async Task<IActionResult> SignUp([FromForm] DtoUse01 objDtoUse01)
        {
            _logger.Info("Signup method called");
            bool userAdded = await _userService.Add(objDtoUse01);
            if (userAdded)
            {
                return Ok("User added successfully");
            }
            return BadRequest("Something went wrong");
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="objUse01">The JsonObject representing the login credentials.</param>
        /// <returns>IActionResult containing a token object or a bad request message.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] JsonObject objUse01)
        {
            _logger.Info("Login method called");
            object tokenObject = _userService.Login(objUse01);
            if (tokenObject == null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(tokenObject);
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>IActionResult containing a list of user data or a Not Found message.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers()
        {
            dynamic userList = _userService.GetUsers();
            if (userList == null)
            {
                return NotFound();
            }
            return Ok(userList);
        }

        /// <summary>
        /// Retrieves details of the currently authenticated user. Requires authorization.
        /// </summary>
        /// <returns>IActionResult containing a dictionary of user details or a Not Found message.</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUserDetails()
        {
            Dictionary<string, object> userDetails = await _userService.GetUserDetails(HttpContext);
            if (userDetails == null)
            {
                return NotFound();
            }
            return Ok(userDetails);
        }

        /// <summary>
        /// Retrieves a list of users the currently authenticated user is following. 
        /// </summary>
        /// <returns>IActionResult containing a list of following users or a bad request message (implementation pending).</returns>
        [HttpGet("following")]
        [Authorize]
        public async Task<IActionResult> GetUsersFollowing()
        {
            return Ok(await _userService.GetFollowing(HttpContext));
        }

        #endregion
    }
}
