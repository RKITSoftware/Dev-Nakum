using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Controllers
{
    /// <summary>
    /// for handling comment-related operations in the SocialMediaAPI.
    /// </summary>
    [Route("api/comments")]  
    [ApiController]           
    public class CLCommentsController : ControllerBase
    {
        #region Private Member

        /// <summary>
        /// The injected ICommentService dependency for interacting with comment logic.
        /// </summary>
        private readonly ICommentService _commentService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for CLCommentsController that injects the ICommentService dependency.
        /// </summary>
        /// <param name="commentService">The ICommentService instance to use.</param>
        public CLCommentsController(ICommentService commentService)
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
            bool commentAdded = _commentService.Add(objDtoCom01, HttpContext);
            if (commentAdded)
            {
                return Ok("Comment added successfully");
            }
            return BadRequest("Something went wrong while adding the comment");
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
            bool commentUpdated = _commentService.Update(id, objDtoCom01, HttpContext);
            if (commentUpdated)
            {
                return Ok("Comment updated successfully");
            }
            return BadRequest("Something went wrong while updating the comment");
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
            bool commentDeleted = _commentService.Delete(id, HttpContext);
            if (commentDeleted)
            {
                return Ok("Comment deleted successfully");
            }
            return BadRequest("Something went wrong while deleting the comment");
        }

        /// <summary>
        /// Retrieves all comments associated with a specific post asynchronously. Requires authorization.
        /// </summary>
        /// <param name="id">The ID of the post to get comments for.</param>
        /// <returns>IActionResult containing a list of comment data or a bad request message.</returns>
        [HttpGet("post/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllCommentsOnPostGetAllCommentsOnPost(int id)
        {
            List<Dictionary<string, object>> comments = await _commentService.GetAllCommentsOnPost(id);
            if (comments != null)
            {
                return Ok(comments);
            }
            return BadRequest("Something went wrong");
        }

        #endregion
    }
}

