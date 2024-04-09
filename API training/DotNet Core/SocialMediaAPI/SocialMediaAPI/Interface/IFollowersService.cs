using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    public interface IFollowersService
    {
        public bool Add(DtoFol01 objDtoFol01, HttpContext httpContext);
        public bool Remove(DtoFol01 objDtoFol01, HttpContext httpContext);
    }
}
