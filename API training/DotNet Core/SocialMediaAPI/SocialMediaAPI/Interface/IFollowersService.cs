﻿using Check_Id_Exist;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.Enums;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Interface
{
    /// <summary>
    /// IFollowersService interface defines methods for managing follower relationships.
    /// </summary>
    public interface IFollowersService
    {
        #region Public Properites
        /// <summary>
        /// operation types A - Add, E - Edit, D - Delete
        /// </summary>
        public enmOperationType OperationType { get; set; }
        #endregion

        #region Public Method

        /// <summary>
        /// Maps the provided DTO object (containing follower data) to a Fol01 entity 
        /// and sets the current user ID (following user) in the Fol01 entity.
        /// </summary>
        /// <param name="objDtoFol01">The DTO object containing follower data.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        public void PreSave(DtoFol01 objDtoFol01, HttpContext httpContext);

        /// <summary>
        /// validation before adding the record into database
        /// </summary>
        /// <param name="objValidation">object of the validation</param>
        /// <returns>response model</returns>
        public Response ValidationOnSave();

        /// <summary>
        /// insert the record into database
        /// </summary>
        /// <returns>response model</returns>
        public Response Save();

        /// <summary>
        /// validation before removing the user from following list
        /// </summary>
        /// <param name="objDtoFol01">dto object of the following</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <param name="objValidation">object of the validation</param>
        /// <returns>response model</returns>
        public Response ValidationOnDelete(DtoFol01 objDtoFol01, HttpContext httpContext);

        /// <summary>
        /// Removes a follower record where the current user is the following user (unfollows).
        /// </summary>
        /// <returns>response model</returns>
        public Response Remove();

        #endregion
    }
}
