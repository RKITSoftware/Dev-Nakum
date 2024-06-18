using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Enums;
using SocialMediaAPI.Filter;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Controllers
{
    /// <summary>
    /// for handling post-related operations in the SocialMediaAPI.
    /// </summary>
    [Route("api/posts")]
    [ApiController]
    [Authorize]
    public class CLPOS01Controller : ControllerBase
    {
        #region Private Member

        /// <summary>
        /// The injected IPostService dependency for interacting with post logic.
        /// </summary>
        private readonly IPOS01Service _postService;


        #endregion


        #region Public Member

        /// <summary>
        /// Create the object of response model
        /// </summary>
        public Response objResponse;
        #endregion


        #region Constructor

        /// <summary>
        /// Constructor for CLPostsController that injects the IPostService dependency.
        /// </summary>
        /// <param name="postService">The IPostService instance to use.</param>
        public CLPOS01Controller(IPOS01Service postService)
        {
            _postService = postService;
            objResponse = new Response();
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="objDTOPOS01">The DTO object representing the post data.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpPost("add")]
        [PostValidationFilter]
        public IActionResult AddPost([FromForm] DTOPOS01 objDTOPOS01)
        {
            _postService.OperationType = enmOperationType.A;
            _postService.PreSave(objDTOPOS01);
            objResponse = _postService.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _postService.Save();
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Retrieves all posts.
        /// </summary>
        /// <returns>IActionResult containing a list of post data or a bad request message.</returns>
        [HttpGet]
        public IActionResult GetAllPost()
        {
            objResponse = _postService.GetPosts();
            return Ok(objResponse);
        }

        /// <summary>
        /// Retrieves posts created by the authorized user.
        /// </summary>
        /// <returns>IActionResult containing a list of the user's posts or a bad request message.</returns>
        [HttpGet("details")]
        public IActionResult GetPostByMe()
        {
            objResponse =  _postService.GetPostByMe();
            return Ok(objResponse);
        }

        /// <summary>
        /// Updates an existing post.
        /// </summary>
        /// <param name="id">The ID of the post to update.</param>
        /// <param name="objDTOPOS01">The DTO object representing the updated post data.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpPatch("{id}")]
        public IActionResult UpdatePost(int id, [FromForm] DTOPOS01 objDTOPOS01)
        {
            _postService.OperationType = enmOperationType.E;
             _postService.PreSave(objDTOPOS01, id);
            objResponse = _postService.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _postService.Save();
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Deletes a post.
        /// </summary>  
        /// <param name="id">The ID of the post to delete.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            _postService.OperationType = enmOperationType.D;
            objResponse = _postService.ValidationOnDelete(id);

            if (!objResponse.IsError)
            {
                objResponse = _postService.Delete();
            }
            return Ok(objResponse);
        }
        #endregion
    }
}
