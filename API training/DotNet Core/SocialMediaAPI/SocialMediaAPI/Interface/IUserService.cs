using ServiceStack.Text;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    /// <summary>
    /// IUserService interface defines methods for user management operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Adds a new user to the system asynchronously.
        /// </summary>
        /// <param name="objDtoUse01">The DTO object representing the user data.</param>
        /// <returns>A Task that returns true if the user is added successfully, false otherwise.</returns>
        public Task<bool> Add(DtoUse01 objDtoUse01);

        /// <summary>
        /// Attempts to log a user in to the system. 
        /// </summary>
        /// <param name="objUse01">A JsonObject containing login credentials (username/password likely).</param>
        /// <returns>An object representing the login result (its containing a token and role).</returns>
        public object Login(JsonObject objUse01);

        /// <summary>
        /// Gets a list of all users.
        /// </summary>
        /// <returns>A list of dictionaries containing user details.</returns>
        public List<Dictionary<string, object>> GetUsers();

        /// <summary>
        /// Gets details for the currently logged-in user asynchronously.
        /// </summary>
        /// <param name="httpContext">The HttpContext object representing the current request.</param>
        /// <returns>A Task that returns a dictionary containing details of the logged-in user.</returns>
        public Task<Dictionary<string, object>> GetUserDetails(HttpContext httpContext);

        /// <summary>
        /// Gets a dictionary mapping usernames to a HashSet of usernames that the current user follows asynchronously.
        /// </summary>
        /// <param name="httpContext">The HttpContext object representing the current request.</param>
        /// <returns>A Task that returns a dictionary where keys are usernames the current user follows and values are HashSets containing usernames of those users' followers.</returns>
        public Task<Dictionary<string, HashSet<string>>> GetFollowing(HttpContext httpContext);
    }
}
