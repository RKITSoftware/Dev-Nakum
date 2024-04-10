using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    /// <summary>
    /// ICommentService interface defines methods for CRUD (Create, Read, Update, Delete) operations on comments.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Adds a new comment to the system.
        /// </summary>
        /// <param name="objDtoCom01">The DTO object representing the comment data.</param>
        /// <param name="httpContext">The HttpContext object representing the current request.</param>
        /// <returns>True if the comment is added successfully, false otherwise.</returns>
        public bool Add(DtoCom01 objDtoCom01, HttpContext httpContext);

        /// <summary>
        /// Deletes a comment from the system.
        /// </summary>
        /// <param name="id">The identifier of the comment to be deleted.</param>
        /// <param name="httpContext">The HttpContext object representing the current request.</param>
        /// <returns>True if the comment is deleted successfully, false otherwise.</returns>
        public bool Delete(int id, HttpContext httpContext);

        /// <summary>
        /// Updates an existing comment in the system.
        /// </summary>
        /// <param name="id">The identifier of the comment to be updated.</param>
        /// <param name="objDtoCom01">The DTO object representing the updated comment data.</param>
        /// <param name="httpContext">The HttpContext object representing the current request.</param>
        /// <returns>True if the comment is updated successfully, false otherwise.</returns>
        public bool Update(int id,DtoCom01 objDtoCom01,HttpContext httpContext);

        /// <summary>
        /// Gets all comments associated with a specific post asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the post.</param>
        /// <returns>A Task that returns a list of dictionaries containing comment details.</returns>
        public Task<List<Dictionary<string, object>>> GetAllCommentsOnPost(int id);
    }
}
