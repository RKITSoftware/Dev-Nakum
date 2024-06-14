using Check_Id_Exist;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Enums;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    /// <summary>
    /// ICommentService interface defines methods for CRUD (Create, Read, Update, Delete) operations on comments.
    /// </summary>
    public interface ICommentService
    {

        public enmOperationType OperationType { get; set; }

        /// <summary>
        /// map the dto object to the poco object 
        /// </summary>
        /// <param name="objDtoCom01">object of the comment</param>
        /// <param name="httpContext">current context for getting the user id</param>
        /// <param name="commentId">comment id</param>
        public void PreSave(DtoCom01 objDtoCom01, HttpContext httpContext, int commentId = 0);

        /// <summary>
        /// validation before add or update comment into database
        /// </summary>
        /// <param name="objValidation">service of validation</param>
        /// <returns>response model</returns>
        public Response ValidationOnSave();

        /// <summary>
        /// Add or Update the comment into data base
        /// </summary>
        /// <returns>response model</returns>
        public Response Save();

        /// <summary>
        /// validation before delete the comment
        /// </summary>
        /// <param name="commentId">comment id</param>
        /// <param name="httpContext">current context for getting the user id</param>
        /// <param name="objValidation">object of the validation</param>
        /// <returns>response model</returns>
        public Response ValidationOnDelete(int commentId, HttpContext httpContext);

        /// <summary>
        /// Deletes a comment from the database.
        /// </summary>
        /// <returns>response model</returns>
        public Response Delete();

        /// <summary>
        /// Gets all comments for a specific post.
        /// </summary>
        /// <param name="id">The ID of the post to get comments for.</param>
        /// <returns>response model</returns>
        public Task<Response> GetAllCommentsOnPost(int id);
    }
}
