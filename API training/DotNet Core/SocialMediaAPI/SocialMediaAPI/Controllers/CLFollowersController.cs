using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Controllers
{
    /// <summary>
    /// for handling follower-related operations in the SocialMediaAPI.
    /// </summary>
    [Route("api/followers")]  
    [ApiController]           
    public class CLFollowersController : ControllerBase
    {
        #region Private Member

        /// <summary>
        /// The injected IFollowersService dependency for interacting with follower logic.
        /// </summary>
        private readonly IFollowersService _followersService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for CLFollowersController that injects the IFollowersService dependency.
        /// </summary>
        /// <param name="followersService">The IFollowersService instance to use.</param>
        public CLFollowersController(IFollowersService followersService)
        {
            _followersService = followersService;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Allows a user to follow another user. Requires authorization.
        /// </summary>
        /// <param name="objDtoFol01">The DTO object representing the following user ID.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpPost]
        [Authorize]
        public IActionResult Add(DtoFol01 objDtoFol01)
        {
            bool followSuccessful = _followersService.Add(objDtoFol01, HttpContext);
            if (followSuccessful)
            {
                return Ok("You have successfully followed the user");
            }
            return BadRequest("Something went wrong");
        }

        /// <summary>
        /// Allows a user to unfollow another user. Requires authorization.
        /// </summary>
        /// <param name="objDtoFol01">The DTO object representing the following user ID.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpDelete]
        [Authorize]
        public IActionResult Remove(DtoFol01 objDtoFol01)
        {
            bool unfollowed = _followersService.Remove(objDtoFol01, HttpContext);
            if (unfollowed)
            {
                return Ok("Removed successfully");
            }
            return BadRequest("Something went wrong");
        }

        #endregion
    }
}
