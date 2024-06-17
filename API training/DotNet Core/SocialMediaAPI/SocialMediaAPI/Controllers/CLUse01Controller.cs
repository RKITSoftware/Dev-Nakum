using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using SocialMediaAPI.Enums;
using SocialMediaAPI.Filter;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Controllers
{
    /// <summary>
    /// for handling user-related operations in the SocialMediaAPI.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class CLUse01Controller : ControllerBase
    {
        #region Private Member

        /// <summary>
        /// The injected IUserService dependency for interacting with user logic.
        /// </summary>
        private readonly IUse01Service _userService;

        /// <summary>
        /// The logger instance for logging messages.
        /// </summary>
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Public Member

        /// <summary>
        /// Create the object of response model
        /// </summary>
        public Response objResponse;
        #endregion


        #region Constructor

        /// <summary>
        /// Constructor for CLUsersController that injects the IUserService dependency.
        /// Logs a message upon construction.
        /// </summary>
        /// <param name="userService">The IUserService instance to use.</param>
        public CLUse01Controller(IUse01Service userService)
        {
            _userService = userService;
            objResponse = new Response();
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
            _userService.OperationType = enmOperationType.A;

            await _userService.PreSave(objDtoUse01);
            objResponse = _userService.ValidationOnSave();
            if (!objResponse.IsError)
            {
                objResponse = _userService.Save();
            }

            return Ok(objResponse);
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="objUse01">The JsonObject representing the login credentials.</param>
        /// <returns>IActionResult containing a token object or a bad request message.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] DtoUse01 objDtoUse01)
        {
            _logger.Info("Login method called");
            objResponse = _userService.Login(objDtoUse01);

            return Ok(objResponse);
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>IActionResult containing a list of user data or a Not Found message.</returns>
        [HttpGet]
        [Authorize(Roles = "A")]
        public async Task<IActionResult> GetUsers()
        {
            objResponse = await _userService.GetUsers();
            return Ok(objResponse);
        }

        /// <summary>
        /// Retrieves details of the currently authenticated user. Requires authorization.
        /// </summary>
        /// <returns>IActionResult containing a dictionary of user details or a Not Found message.</returns>
        [HttpGet("details")]
        [Authorize]
        public IActionResult GetUserDetails()
        {
            objResponse =  _userService.GetUserDetails();
            return Ok(objResponse);
        }

        /// <summary>
        /// Retrieves a list of users the currently authenticated user is following. 
        /// </summary>
        /// <returns>IActionResult containing a list of following users or a bad request message (implementation pending).</returns>
        [HttpGet("following")]
        [Authorize]
        public  IActionResult GetUsersFollowing()
        {
            objResponse = _userService.GetFollowing();
            return Ok(objResponse);
        }

        #endregion
    }
}
