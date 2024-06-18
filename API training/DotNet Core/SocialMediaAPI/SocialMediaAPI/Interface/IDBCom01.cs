using System.Data;

namespace SocialMediaAPI.Interface
{
    public interface IDBCOM01
    {
        /// <summary>
        /// update comment content
        /// </summary>
        /// <param name="commentId">id of comments</param>
        /// <param name="commentContent">content of comment</param>
        /// <returns>true if comment updated or else false</returns>
        public bool UpdateComment(int commentId, string commentContent);

        /// <summary>
        /// Gets all comments for a specific post.
        /// </summary>
        /// <param name="id">The ID of the post to get comments for.</param>
        /// <returns>response DataTable</returns>
        public DataTable GetAllCommentsOnPost(int id);
    }
}
