using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Controllers
{
    [Route("api/followers")]
    [ApiController]
    public class CLFollowersController : ControllerBase
    {
        #region Private Member
        private readonly IFollowersService _followersService;

        #endregion

        #region Constructor
        public CLFollowersController(IFollowersService followersService)
        {
            _followersService = followersService;
        }

        #endregion

        #region Public Method
        [HttpPost]
        [Authorize]
        public IActionResult Add(DtoFol01 objDtoFol01)
        {
            bool isSuccess = _followersService.Add(objDtoFol01, HttpContext);
            if(isSuccess)
            {
                return Ok("you have successfully followed the user");
            }
            return BadRequest("something went wrong");
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Remove(DtoFol01 objDtoFol01)
        {
            bool isRemoved = _followersService.Remove(objDtoFol01, HttpContext);
            if (isRemoved)
            {
                return Ok("Removed successfully");
            }
            return BadRequest("something went wrong");
        }
        #endregion
    }
}
