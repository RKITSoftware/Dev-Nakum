using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CLCommentsController : ControllerBase
    {
        #region Private Member
        private readonly ICommentService _commentService;
        #endregion
        
        #region Constructor
        public CLCommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        #endregion

        #region Public Method
        [HttpPost("add")]
        [Authorize]
        public IActionResult AddComments(DtoCom01 objDtoCom01)
        {
            bool comment = _commentService.Add(objDtoCom01,HttpContext);
            if (comment)
            {
                return Ok("Comment added successfully");
            }
            return BadRequest("Something went wrong while adding the  comment");
        }

        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult UpdateComments(int id, DtoCom01 objDtoCom01)
        {
            bool comment = _commentService.Update(id,objDtoCom01, HttpContext);
            if (comment)
            {
                return Ok("Comment updated successfully");
            }
            return BadRequest("Something went wrong while updating the comment");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteComments(int id)
        {
            bool comment = _commentService.Delete(id, HttpContext);
            if (comment)
            {
                return Ok("Comment deleted successfully");
            }
            return BadRequest("Something went wrong while deleting the comment");
        }

        [HttpGet("post/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllCommentsOnPostGetAllCommentsOnPost(int id)
        {
            List<Dictionary<string, object>> comments = await _commentService.GetAllCommentsOnPost(id);
            if (comments!=null)
            {
                return Ok(comments);
            }
            return BadRequest("Something went wrong");
        }
        #endregion
    }
}
