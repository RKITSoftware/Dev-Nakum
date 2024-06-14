﻿using Check_Id_Exist;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using NLog;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SocialMediaAPI.Enums;
using SocialMediaAPI.Extension;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using SocialMediaAPI.Model.Dtos;
using SocialMediaAPI.Model.POCO;
using System.Data;

namespace SocialMediaAPI.BL
{
    public class BLPos01 : IPostService
    {
        #region Private Member

        /// <summary>
        /// create the variable for connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// create the object of the logger
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// create the object of the connection factory for ORMLite
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;
        /// <summary>
        /// create the object of the configuration for gt the connection string
        /// </summary>
        private readonly IConfiguration _configuration;

        private readonly Validation _objValidation;

        /// <summary>
        /// create the object of the post model
        /// </summary>
        private Pos01 _objPos01;

        #endregion

        #region Public Properites
        /// <summary>
        /// operation types A - Add, E - Edit, D - Delete
        /// </summary>
        public enmOperationType OperationType { get; set; }
        #endregion

        #region Public Member
        /// <summary>
        /// create the object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Constructor
        public BLPos01(IConfiguration configuration, Validation objValidation)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _objValidation = objValidation;
        }
        #endregion

        #region Private Method
        private (int,string) GetUserIdAndImgOfPost(int postId)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();

                string query = @"SELECT 
                                    S01F02,
                                    S01F03
                                FROM 
                                    Pos01 
                                WHERE 
                                    S01F01 = @S01F01";
                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@S01F01", postId);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                if (objMySqlDataReader.HasRows)
                {
                    objMySqlDataReader.Read();
                    int userId = objMySqlDataReader.GetInt32("S01F02");
                    string imgUrl = objMySqlDataReader.GetString("S01F03");
                    
                    return (userId, imgUrl);
                }
                return (0,"");
            }
        }
        /// <summary>
        /// Uploads an image to the configured folder path and returns the image URL.
        /// Creates the folder if it doesn't exist.
        /// </summary>
        /// <param name="imageFile">The IFormFile containing the image to upload.</param>
        /// <param name="_objPos01">The temporary post object (_objPos01) to update with the image URL.</param>
        /// <returns>The image URL if uploaded successfully, null otherwise.</returns>
        private async Task<string> UploadImage(IFormFile imageFile, Pos01 _objPos01)
        {
            string uploadFolder = Path.Combine(_configuration.GetValue<string>("Uploads:PostFolderPath"), "");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            Guid guid = Guid.NewGuid();

            string fileName = _objPos01.S01F02.ToString() + "_" + guid.ToString("N") + Path.GetExtension(imageFile.FileName);

            string filePath = Path.Combine(uploadFolder, fileName);


            await using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("/Upload/Post", fileName);
        }

        /// <summary>
        /// Deletes an image file associated with a post.
        /// </summary>
        /// <param name="imgUrl">The URL of the image to delete.</param>
        /// <returns>True if the image is deleted successfully, false otherwise.</returns>
        private bool DeleteImage(string imgUrl)
        {
            try
            {
                string filePath = imgUrl.Replace("/Upload/Post\\", "", StringComparison.OrdinalIgnoreCase);

                string fullPath = Path.Combine(_configuration.GetValue<string>("Uploads:PostFolderPath"), filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: post img delete :: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Gets a post by its ID from the database.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve.</param>
        /// <returns>The post object if found, null otherwise.</returns>
        private Pos01 GetPostById(int postId,int userId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Single<Pos01>(x=>x.S01F01 == postId && x.S01F02 == userId);
            }
        }

        #endregion

        #region Public Method
       
        /// <summary>
        /// PreSave the DTOs object to the POCOs and pre validations
        /// </summary>
        /// <param name="objDtoPos01">object of the post</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <param name="postId">post id</param>
        public async Task PreSave(DtoPos01 objDtoPos01, HttpContext httpContext, int postId = 0)
        {
            _objPos01 = new Pos01();
            int userId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
            
            if (OperationType == enmOperationType.E)
            {
                //_objPos01.S01F01 = postId;

                //need to update the entire the object of the post
                Pos01 post = GetPostById(postId,userId);

                // if post is available 
                if(post != null)
                {
                    _objPos01 = post;
                    _objPos01.S01F06 = DateTime.Now;

                }

            }

            if (OperationType == enmOperationType.A)
            {
                _objPos01.S01F02 = userId;
            }

            // to check post content is available or not
            if (objDtoPos01.S01F04 != null)
            {
                _objPos01 = objDtoPos01.MapDtoToPoco<DtoPos01, Pos01>(_objPos01);
            }

            if (objDtoPos01.S01F03 != null)
            {
                string imageUrl = await UploadImage(objDtoPos01.S01F03, _objPos01);
                _objPos01.S01F03 = imageUrl;
                _logger.Info("Post image upload successfully");
            }
        }
        /// <summary>
        /// Post object Validation before inserting or updating into database
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();
            
            try
            {
                if (OperationType == enmOperationType.E)
                {
                    //validation for post id
                    //bool isPostExist = _objValidation.IsExist2<Pos01>(_objPos01.S01F01);
                    bool isPostExist = _objValidation.IsExist<Pos01>(_objPos01.S01F01, x => x.S01F01);
                    if (!isPostExist)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "post is not exist";
                        return objResponse;
                    }
                }

                //validation for user id
                bool isUserExist = _objValidation.IsExist<Use01>(_objPos01.S01F02,x => x.E01F01);
                if (!isUserExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "user is not exist";
                }

                return objResponse;
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: IsExist :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// Validation before deleting post into database
        /// </summary>
        /// <param name="postId">post id</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <returns>response model</returns>
        public Response ValidationOnDelete(int postId, HttpContext httpContext)
        {
            objResponse = new Response();

            try
            {
                if (OperationType == enmOperationType.D)
                {
                    int userId = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);

                    bool isUserExist = _objValidation.IsExist<Use01>(userId, x => x.E01F01);
                    if (!isUserExist)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "user is not exist";

                        return objResponse;
                    }
                    bool isPostExist = _objValidation.IsExist<Pos01>(postId,x=>x.S01F01);
                    if (!isPostExist)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "post is not exist";
                    }
                    else
                    {
                        // validate the userId form post -- need to same 
                        (int userIdFromPost, string imgUrl) = GetUserIdAndImgOfPost(postId);


                        if (userIdFromPost != userId)
                        {
                            objResponse.IsError = true;
                            objResponse.Message = "You can not delete the post, which is not created by you.";
                        }
                        else
                        {
                            _objPos01 = new Pos01();
                            _objPos01.S01F01 = postId;
                            _objPos01.S01F03 = imgUrl;
                        }
                    }
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: IsExist :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }
        
        /// <summary>
        /// Add or Update the post into database
        /// </summary>
        /// <returns>response model</returns>
        public Response Save()
        {
            objResponse = new Response();

            try
            {
                if (OperationType == enmOperationType.A)
                {
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        bool isPostAdded = db.Insert(_objPos01) > 0;
                        if (!isPostAdded)
                        {
                            objResponse.IsError = true;
                            objResponse.Message = "something went wrong for adding post";
                        }
                        else
                        {
                            objResponse.Message = "Post is added successfully";
                        }
                    }
                }

                if (OperationType == enmOperationType.E)
                {
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        bool isPostUpdated = db.Update(_objPos01) > 0;
                        if (!isPostUpdated)
                        {
                            objResponse.IsError = true;
                            objResponse.Message = "something went wrong for updating post";
                        }
                        else
                        {
                            objResponse.Message = "Post is updated successfully";
                        }
                    }
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: Save :: Post :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// Retrieves a list of all posts from the database.
        /// </summary>
        /// <returns>response model</returns>
        public async Task<Response> GetPosts()
        {
            objResponse = new Response();

            try
            {
                await using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
                {
                    objMySqlConnection.Open();
                    string query = @"SELECT 
                                    S01F01 AS S01101, 
                                    E01F02 AS E01102, 
                                    S01F03 AS S01103, 
                                    S01F04 AS S01104, 
                                    S01F06 AS S01106 
                                FROM
                                    Pos01 
                                JOIN 
                                    Use01 
                                ON 
                                    Pos01.S01F02 = Use01.E01F01";
                    MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);

                    MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                    DataTable dtAllPost = new DataTable();
                    dtAllPost.Load(objMySqlDataReader);

                    objResponse.Data = dtAllPost;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: GetPosts :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// Retrieves a list of posts created by the current user.
        /// </summary>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <returns>Response model</returns>
        public async Task<Response> GetPostByMe(HttpContext httpContext)
        {
            objResponse = new Response();

            try
            {
                int id = Convert.ToInt32(httpContext.User.FindFirst("Id")?.Value);
                await using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
                {
                    objMySqlConnection.Open();

                    string query = @"SELECT 
                                    S01F03, 
                                    S01F04, 
                                    S01F05 
                                FROM 
                                    Pos01 
                                WHERE 
                                    S01F02 = @S01F02";
                    MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                    objMySqlCommand.Parameters.AddWithValue("@S01F02", id);

                    MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();
                    DataTable dtGetPostByUser = new DataTable();

                    dtGetPostByUser.Load(objMySqlDataReader);
                    objResponse.Data = dtGetPostByUser;

                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: GetPostByMe :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// delete a post from the database.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <param name="httpContext">The HTTP context used to get the current user ID.</param>
        /// <returns>response model</returns>
        public Response Delete()
        {
            objResponse = new Response();

            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    bool isPostDeleted = db.DeleteById<Pos01>(_objPos01.S01F01) > 0;
                    if (isPostDeleted)
                    {
                        bool isImgDeleted = DeleteImage(_objPos01.S01F03);
                        if (!isImgDeleted)
                        {
                            objResponse.IsError = true;
                            objResponse.Message = "something went wrong while delete the post img";
                        }
                        else
                        {
                            objResponse.Message = "Successfully deleted the post";
                        }
                    }
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                _logger.Error($"exception :: Delete :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
            
        }

        #endregion
    }
}
