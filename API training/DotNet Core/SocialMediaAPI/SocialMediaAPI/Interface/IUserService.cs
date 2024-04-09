using ServiceStack.Text;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    public interface IUserService
    {
        public Task<bool> Add(DtoUse01 objDtoUse01);

        public object Login(JsonObject objUse01);

        public List<Dictionary<string, object>> GetUsers();

        public Task<Dictionary<string, object>> GetUserDetails(HttpContext httpContext);

        public Task<Dictionary<string, HashSet<string>>> GetFollowing(HttpContext httpContext);
    }
}
