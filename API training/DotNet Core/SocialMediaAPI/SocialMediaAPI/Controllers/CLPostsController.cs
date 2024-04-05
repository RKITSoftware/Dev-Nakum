using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Text;
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
        #endregion
    }
}
