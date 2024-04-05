using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    public interface IPostService
    {
        public Task<bool> Add(DtoPos01 objDtoPos01,HttpContext httpContext);
        public Task<List<Dictionary<string,object>>> GetPosts();
    }
}
