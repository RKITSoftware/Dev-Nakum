using Check_Id_Exist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Enums;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Controllers
{
    /// <summary>
    /// for handling follower-related operations in the SocialMediaAPI.
    /// </summary>
    [Route("api/followers")]  
    [ApiController]           
    public class CLFol01Controller : ControllerBase
    {
        #region Private Member
        /// <summary>
        /// The injected IFollowersService dependency for interacting with follower logic.
        /// </summary>
        private readonly IFol01Service _followersService;
        #endregion

        #region Public Member
        /// <summary>
        /// create the object of the response model
        /// </summary>
        public Response objResponse;
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor for CLFollowersController that injects the IFollowersService dependency.
        /// </summary>
        /// <param name="followersService">The IFollowersService instance to use.</param>
        public CLFol01Controller(IFol01Service followersService)
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
            objResponse = new Response();
            _followersService.OperationType = enmOperationType.A;
            _followersService.PreSave(objDtoFol01);
            objResponse = _followersService.ValidationOnSave();
            if (!objResponse.IsError)
            {
                objResponse = _followersService.Save();
            }

            return Ok(objResponse);
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
            objResponse = new Response();
            _followersService.OperationType = enmOperationType.D;
            objResponse = _followersService.ValidationOnDelete(objDtoFol01);
            if (!objResponse.IsError)
            {
                objResponse = _followersService.Remove();
            }

            return Ok(objResponse);
        }

        #endregion
    }
}
