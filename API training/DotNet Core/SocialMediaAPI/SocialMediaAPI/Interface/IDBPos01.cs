using System.Data;

namespace SocialMediaAPI.Interface
{
    /// <summary>
    /// define method of DBPOS01
    /// </summary>
    public interface IDBPOS01
    {
        /// <summary>
        /// get the user id and image of post
        /// </summary>
        /// <param name="postId">id of post</param>
        /// <returns>userId and url of post</returns>
        public (int, string) GetUserIdAndImgOfPost(int postId);

        /// <summary>
        /// Retrieves a list of all posts from the database.
        /// </summary>
        /// <returns>response DataTable</returns>
        public DataTable GetPosts();


        /// <summary>
        /// Retrieves a list of posts created by the current user.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>response datatable</returns>
        public DataTable GetPostByMe(int id);
    }
}
