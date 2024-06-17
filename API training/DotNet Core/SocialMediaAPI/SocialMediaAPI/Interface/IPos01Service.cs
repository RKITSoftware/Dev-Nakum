using SocialMediaAPI.Enums;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    /// <summary>
    /// IPostService interface defines methods for CRUD (Create, Read, Update, Delete) operations on posts.
    /// </summary>
    public interface IPos01Service
    {
        #region Properties
        /// <summary>
        /// Operation Types - A - Add, E - Edit, D - Delete
        /// </summary>
        public enmOperationType OperationType { get; set; }
        #endregion


        #region Public Method


        /// <summary>
        /// PreSave the DTOs object to the POCOs and pre validations
        /// </summary>
        /// <param name="objDtoPos01">object of the post</param>
        /// <param name="postId">post id</param>
        public Task PreSave(DtoPos01 objDtoPos01,  int postId = 0);

        /// <summary>
        /// Post object Validation before inserting or updating into database
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave();

        /// <summary>
        /// Validation before deleting post into database
        /// </summary>
        /// <param name="postId">post id</param>
        /// <returns>response model</returns>
        public Response ValidationOnDelete(int postId);

        /// <summary>
        /// Add or Update the post into database
        /// </summary>
        /// <returns>response model</returns>
        public Response Save();

        /// <summary>
        /// Retrieves a list of all posts from the database.
        /// </summary>
        /// <returns>response model</returns>
        public Task<Response> GetPosts();

        /// <summary>
        /// Retrieves a list of posts created by the current user.
        /// </summary>
        /// <returns>Response model</returns>
        public Task<Response> GetPostByMe();

        /// <summary>
        /// delete a post from the database.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <returns>response model</returns>
        public Response Delete();

        #endregion
    }
}