using Check_Id_Exist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Controllers
{
    /// <summary>
    /// for handling comment-related operations in the SocialMediaAPI.
    /// </summary>
    [Route("api/comments")]  
    [ApiController]           
    public class CLCom01Controller : ControllerBase
    {
        #region Private Member

        /// <summary>
        /// The injected ICommentService dependency for interacting with comment logic.
        /// </summary>
        private readonly ICom01Service _commentService;

        #endregion

        #region Public Member

        /// <summary>
        /// create the object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Constructor

        /// Constructor for CLCommentsController that injects the ICommentService dependency.
        /// </summary>
        /// <param name="commentService">The ICommentService instance to use.</param>
        public CLCom01Controller(ICom01Service commentService)
        {
            _commentService = commentService;
        }

        #endregion

        #region Public Method
        /// <summary>
        /// Adds a new comment to the system. Requires authorization.
        /// </summary>
        /// <param name="objDtoCom01">The DTO object representing the comment data.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpPost("add")]
        [Authorize]
        public IActionResult AddComments(DtoCom01 objDtoCom01)
        {
            objResponse = new Response();
            _commentService.OperationType = Enums.enmOperationType.A;
            _commentService.PreSave(objDtoCom01);
            objResponse = _commentService.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _commentService.Save();
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Updates an existing comment. Requires authorization.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="objDtoCom01">The DTO object representing the updated comment data.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult UpdateComments(int id, DtoCom01 objDtoCom01)
        {
            objResponse = new Response();
            _commentService.OperationType = Enums.enmOperationType.E;
            _commentService.PreSave(objDtoCom01,id);
            objResponse = _commentService.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _commentService.Save();
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Deletes a comment. Requires authorization.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteComments(int id)
        {
            objResponse = new Response();
            _commentService.OperationType = Enums.enmOperationType.D;
            objResponse = _commentService.ValidationOnDelete(id);

            if (!objResponse.IsError)
            {
                objResponse = _commentService.Delete();
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Retrieves all comments associated with a specific post asynchronously. Requires authorization.
        /// </summary>
        /// <param name="id">The ID of the post to get comments for.</param>
        /// <returns>IActionResult containing a list of comment data or a bad request message.</returns>
        [HttpGet("post/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllCommentsOnPost(int id)
        {
            objResponse = new Response();
            objResponse = await _commentService.GetAllCommentsOnPost(id);
            return Ok(objResponse);
        }

        #endregion
    }
}