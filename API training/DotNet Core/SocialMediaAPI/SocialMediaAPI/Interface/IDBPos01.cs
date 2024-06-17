using System.Data;

namespace SocialMediaAPI.Interface
{
    /// <summary>
    /// define method of DBPos01
    /// </summary>
    public interface IDBPos01
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
        public Task<DataTable> GetPosts();


        /// <summary>
        /// Retrieves a list of posts created by the current user.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>response datatable</returns>
        public Task<DataTable> GetPostByMe(int id);
    }
}
