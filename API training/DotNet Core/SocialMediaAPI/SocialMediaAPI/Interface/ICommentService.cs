using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    public interface ICommentService
    {
        public bool Add(DtoCom01 objDtoCom01, HttpContext httpContext);

        public bool Delete(int id, HttpContext httpContext);

        public bool Update(int id,DtoCom01 objDtoCom01,HttpContext httpContext);

        public Task<List<Dictionary<string, object>>> GetAllCommentsOnPost(int id);
    }
}
