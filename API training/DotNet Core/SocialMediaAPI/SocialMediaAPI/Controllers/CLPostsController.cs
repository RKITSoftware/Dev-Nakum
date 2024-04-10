using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Filter;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model.Dtos;


namespace SocialMediaAPI.Controllers
{
    /// <summary>
    /// for handling post-related operations in the SocialMediaAPI.
    /// </summary>
    [Route("api/posts")]
    [ApiController]
    public class CLPostsController : ControllerBase
    {
        #region Private Member

        /// <summary>
        /// The injected IPostService dependency for interacting with post logic.
        /// </summary>
        private readonly IPostService _postService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for CLPostsController that injects the IPostService dependency.
        /// </summary>
        /// <param name="postService">The IPostService instance to use.</param>
        public CLPostsController(IPostService postService)
        {
            _postService = postService;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="objDtoPos01">The DTO object representing the post data.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpPost("add")]
        [Authorize]
        [PostValidationFilter]
        public async Task<IActionResult> AddPost([FromForm] DtoPos01 objDtoPos01)
        {
            bool postAdded = await _postService.Add(objDtoPos01, HttpContext);
            if (postAdded)
            {
                return Ok("Post added successfully");
            }
            return BadRequest("Something went wrong");
        }

        /// <summary>
        /// Retrieves all posts.
        /// </summary>
        /// <returns>IActionResult containing a list of post data or a bad request message.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPost()
        {
            List<Dictionary<string, object>> posts = await _postService.GetPosts();
            if (posts != null)
            {
                return Ok(posts);
            }
            return BadRequest("Posts are not found");
        }

        /// <summary>
        /// Retrieves posts created by the authorized user.
        /// </summary>
        /// <returns>IActionResult containing a list of the user's posts or a bad request message.</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetPostByMe()
        {
            List<Dictionary<string, object>> posts = await _postService.GetPostByMe(HttpContext);
            if (posts != null)
            {
                return Ok(posts);
            }
            return BadRequest("Posts are not found");
        }

        /// <summary>
        /// Updates an existing post.
        /// </summary>
        /// <param name="id">The ID of the post to update.</param>
        /// <param name="objDtoPos01">The DTO object representing the updated post data.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(int id, [FromForm] DtoPos01 objDtoPos01)
        {
            bool postUpdated = await _postService.Update(id, objDtoPos01, HttpContext);
            if (postUpdated)
            {
                return Ok("Post updated successfully");
            }
            return BadRequest("Something went wrong while updating the post");
        }

        /// <summary>
        /// Deletes a post.
        /// </summary>  
        /// <param name="id">The ID of the post to delete.</param>
        /// <returns>IActionResult indicating success or failure with a message.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeletePost(int id)
        {
            bool isDeleted = _postService.DeletePost(id, HttpContext);

            if(isDeleted)
            {
                return Ok("Post is successfully deleted");
            }
            return BadRequest("Something went wrong while deleting the post");
        }
        #endregion
    }
}
