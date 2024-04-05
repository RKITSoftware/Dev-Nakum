using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ServiceStack.Text;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;
using SocialMediaAPI.Model.POCO;
using System.Data;
using System.Security.Claims;

namespace SocialMediaAPI.Controllers
{
    [Route("api/users")]
    [ApiController]

    public class CLUsersController : ControllerBase
    {
        #region Private Member
        private readonly IUserService _userService;
        private Logger _logger = LogManager.GetCurrentClassLogger();    
        #endregion

        #region Constructor
        public CLUsersController(IUserService userService)
        {
            _userService = userService;
            _logger.Info("user constructor called");
        }
        #endregion

        #region Public Method

        [HttpPost("signup")]
        public async  Task<IActionResult> SignUp([FromForm] DtoUse01 objDtoUse01)
        {
            _logger.Info("signup method called");
            bool user = await _userService.Add(objDtoUse01);

            if (user)
            {
                return Ok("User added successfully");   
            }
            return BadRequest("something went wrong");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] JsonObject objUse01)
        {
            _logger.Info("login method called");
            object objToken = _userService.Login(objUse01);

            if (objToken==null)
            {
                return BadRequest("something went wrong");
            }
            return Ok(objToken);
        }



        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult GetUsers()
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role);
            _logger.Info($"get users method called : role is {role.Value}");
            dynamic lstUse01 = _userService.GetUsers();

            if ( lstUse01 == null)
            {
                return BadRequest("No User found");
            }
            return Ok(lstUse01);
        }
        #endregion
    }
}
