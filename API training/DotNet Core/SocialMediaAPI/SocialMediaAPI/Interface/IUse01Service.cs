using SocialMediaAPI.Enums;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    /// <summary>
    /// IUserService interface defines methods for user management operations.
    /// </summary>
    public interface IUSE01Service
    {
        /// <summary>
        /// Operation Types - A - Add, E - Edit, D - Delete
        /// </summary>
        public enmOperationType OperationType { get; set; }

        /// <summary>
        /// Maps the provided DTO object containing user data (DtoUse01) to a Use01 model object.
        /// </summary>
        /// <param name="objDTOUSE01">The DTO object containing user data.</param>
        public void PreSave(DTOUSE01 objDTOUSE01);

        /// <summary>
        ///  validation for user signup.
        /// </summary>
        /// <param name="objDTOUSE01">The DTO object containing user data.</param>
        /// <returns>response model</returns>
        public Response ValidationOnSave();


        /// <summary>
        /// Inserts the user data from the temporary _objUSE01 object into the database table Use01.
        /// </summary>
        /// <returns>response model</returns>
        public Response Save();

        /// <summary>
        /// Performs user login by validating username and password.
        /// </summary>
        /// <param name="objDTOUSE01">user object - contains the username and password</param>
        /// <returns>Response model</returns>
        public Response Login(DTOUSE02 objDTOUSE02);

        /// <summary>
        /// Retrieves a list of all users from the database.
        /// </summary>
        /// <returns>response model</returns>
        public Response GetUsers();

        /// <summary>
        /// Retrieves user details based on the user ID from the HTTP context.
        /// </summary>
        /// <returns>response model containing user details.</returns>
        public Response GetUserDetails();

        /// <summary>
        /// Retrieves a list of usernames followed by the current user.
        /// </summary>
        /// <returns>response model</returns>
        public Response GetFollowing();
    }
}