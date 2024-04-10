using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    /// <summary>
    /// IPostService interface defines methods for CRUD (Create, Read, Update, Delete) operations on posts.
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Adds a new post to the system asynchronously.
        /// </summary>
        /// <param name="objDtoPos01">The DTO object representing the post data.</param>
        /// <param name="httpContext">The HttpContext object representing the current request.</param>
        /// <returns>A Task that returns true if the post is added successfully, false otherwise.</returns>
        public Task<bool> Add(DtoPos01 objDtoPos01, HttpContext httpContext);

        /// <summary>
        /// Gets a list of all posts asynchronously.
        /// </summary>
        /// <returns>A Task that returns a list of dictionaries containing post details.</returns>
        public Task<List<Dictionary<string, object>>> GetPosts();

        /// <summary>
        /// Gets a list of posts created by the currently logged-in user asynchronously.
        /// </summary>
        /// <param name="httpContext">The HttpContext object representing the current request (likely for user identification).</param>
        /// <returns>A Task that returns a list of dictionaries containing post details for the logged-in user.</returns>
        public Task<List<Dictionary<string, object>>> GetPostByMe(HttpContext httpContext);

        /// <summary>
        /// Updates an existing post asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the post to be updated.</param>
        /// <param name="objDtoPos01">The DTO object representing the updated post data.</param>
        /// <param name="httpContext">The HttpContext object representing the current request.</param>
        /// <returns>A Task that returns true if the post is updated successfully, false otherwise.</returns>
        public Task<bool> Update(int id, DtoPos01 objDtoPos01, HttpContext httpContext);

        /// <summary>
        /// Deletes a post from the system.
        /// </summary>
        /// <param name="id">The identifier of the post to be deleted.</param>
        /// <param name="httpContext">The HttpContext object representing the current request.</param>
        /// <returns>True if the post is deleted successfully, false otherwise.</returns>
        public bool DeletePost(int id, HttpContext httpContext);
    }
}