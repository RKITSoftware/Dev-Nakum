using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    /// <summary>
    /// IFollowersService interface defines methods for managing follower relationships.
    /// </summary>
    public interface IFollowersService
    {
        /// <summary>
        /// Adds a follower relationship to the system.
        /// </summary>
        /// <param name="objDtoFol01">The DTO object representing the follower relationship data.</param>
        /// <param name="httpContext">The HttpContext object representing the current request.</param>
        /// <returns>True if the follower relationship is added successfully, false otherwise.</returns>
        public bool Add(DtoFol01 objDtoFol01, HttpContext httpContext);

        /// <summary>
        /// Removes a follower relationship from the system.
        /// </summary>
        /// <param name="objDtoFol01">The DTO object representing the follower relationship data.</param>
        /// <param name="httpContext">The HttpContext object representing the current request.</param>
        /// <returns>True if the follower relationship is removed successfully, false otherwise.</returns>
        public bool Remove(DtoFol01 objDtoFol01, HttpContext httpContext);
    }
}
