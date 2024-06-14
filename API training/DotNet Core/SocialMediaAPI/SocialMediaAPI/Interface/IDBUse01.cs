using SocialMediaAPI.Model;
using System.Data;

namespace SocialMediaAPI.Interface
{
    public interface IDBUse01
    {
        #region Public Method
        /// <summary>
        /// Retrieves a list of all users from the database.
        /// </summary>
        /// <returns>response model</returns>
        public Task<DataTable> GetUsers();

        /// <summary>
        /// Retrieves user details based on the user ID from the HTTP context.
        /// </summary>
        /// <param name="httpContext">The HttpContext containing user information.</param>
        /// <returns>response model containing user details.</returns>
        public Task<DataTable> GetUserDetails(int id);


        /// <summary>
        /// Retrieves a list of usernames followed by the current user.
        /// </summary>
        /// <param name="httpContext">The HttpContext containing user information.</param>
        /// <returns>response model</returns>
        public Task<DataTable> GetFollowing(int id);
        #endregion
    }
}
