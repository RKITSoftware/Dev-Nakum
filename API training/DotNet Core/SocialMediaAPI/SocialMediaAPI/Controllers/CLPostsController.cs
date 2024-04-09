using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model.Dtos;


namespace SocialMediaAPI.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class CLPostsController : ControllerBase
    {
        #region Private Member
        private readonly IPostService _postService;

        #endregion

        #region Constructor
        public CLPostsController(IPostService postService)
        {
            _postService = postService;
        }

        #endregion

        #region Public Method

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddPost([FromForm] DtoPos01 objDtoPos01)
        {
            bool post = await _postService.Add(objDtoPos01,HttpContext);
            if(post)
            {
                return Ok("Post added successfully");
            }
            return BadRequest("something went wrong");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPost()
        {
            List<Dictionary<string, object>> posts = await  _postService.GetPosts();

            if (posts != null)
            {
                return Ok(posts);
            }
            return BadRequest("Post is not found");
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetPostByMe()
        {
            List<Dictionary<string, object>> posts = await _postService.GetPostByMe(HttpContext);
            if (posts != null)
            {
                return Ok(posts);
            }
            return BadRequest("Post is not found");
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(int id,[FromForm] DtoPos01 objDtoPos01)
        {
            bool updatePost = await _postService.Update(id,objDtoPos01,HttpContext);
            if (updatePost)
            {
                return Ok("Post updated successfully");
            }
            return BadRequest("something went wrong while updating th post");
        }

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
