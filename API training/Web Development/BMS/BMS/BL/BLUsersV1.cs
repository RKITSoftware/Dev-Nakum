using BMS.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace BMS.BL
{
    /// <summary>
    /// Manage the user's related services
    /// </summary>
    public class BLUsersV1
    {
        #region Private member
        /// <summary>
        /// Static user list
        /// </summary>
        private static List<UsersV1> _lstUsersV1 = new List<UsersV1>()
        {
            //only one admin is there and default role is User
            new UsersV1(){
                Id=1, FirstName="Dev",LastName="Nakum",UserName="deven",Password="Abcd@123", Number="9856895874",Email="dev@gmail.com",Money=0,Role="A"},
            };

        /// <summary>
        /// user id 
        /// </summary>
        private static int _id = 2;

        /// <summary>
        /// Object of the users
        /// </summary>
        private UsersV1 _objUsersV1;
        #endregion

        #region Public Member
        /// <summary>
        /// Object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Constructor
        /// <summary>
        /// initialize the object of the response model
        /// </summary>
        public BLUsersV1()
        {
            objResponse = new Response();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// operation types - (A - Add),(E - Edit),(D - Delete)
        /// </summary>
        public enmOperationTypes OperationTypes { get; set; }
        #endregion


        #region Private Method

        /// <summary>
        /// to check password is valid or noy based on regex
        /// </summary>
        /// <param name="password">user's password</param>
        /// <returns>true if password is valid or else false</returns>
        private bool IsValidPassword(string password)
        {
            // Minimum length(8 characters)
            // At least one uppercase letter
            // At least one lowercase letter
            // At least one number
            // At least one special character
            string passwordRegex = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]{8,}$";
            return Regex.IsMatch(password, passwordRegex);
        }

        /// <summary>
        /// to check user is exist or not 
        /// </summary>
        /// <typeparam name="T">data type </typeparam>
        /// <param name="value"></param>
        /// <param name="selector"></param>
        /// <returns>true if user exist or else false</returns>
        private bool IsUserExist<T>(T value, Func<UsersV1, T> selector)
        {
            return _lstUsersV1.Any(u => selector(u).Equals(value));
        }

        #endregion

        #region Public Method

        /// <summary>
        /// PreSave method for save the necessary data before adding into list
        /// </summary>
        /// <param name="objUsersV1">object of the user</param>
        /// <param name="id">user id only needed when update the user</param>
        public void PreSave(UsersV1 objUsersV1, int id = 0)
        {
            _objUsersV1 = new UsersV1();
            if (OperationTypes == enmOperationTypes.A)
            {
                _objUsersV1.Id = _id++;
                _objUsersV1.UserName = objUsersV1.UserName;
                _objUsersV1.Password = objUsersV1.Password;
            }

            if (OperationTypes == enmOperationTypes.E)
            {
                _objUsersV1.Id = id;
            }
            _objUsersV1.Email = objUsersV1.Email;
            _objUsersV1.Number = objUsersV1.Number;
            _objUsersV1.FirstName = objUsersV1.FirstName;
            _objUsersV1.LastName = objUsersV1.LastName;
        }

        /// <summary>
        /// perform the validation while insert or update the user details into list
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            if (OperationTypes == enmOperationTypes.A)
            {
                // to validate username is exists or not
                bool isUsers = IsUserExist(_objUsersV1.UserName, u => u.UserName);
                
                // to validate the password
                bool iSValidPassword = IsValidPassword(_objUsersV1.Password);

                // response set based on validations
                if (isUsers)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Username already exists.";
                }

                if (!iSValidPassword)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Enter the valid password.";
                }
            }

            if (OperationTypes == enmOperationTypes.E)
            {
                bool isUsers = IsUserExist(_objUsersV1.Id, u => u.Id);
                if (!isUsers)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User is not exists.";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// perform the validation while delete the user using user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Response model</returns>
        public Response ValidationOnDelete(int id)
        {
            if (OperationTypes == enmOperationTypes.D)
            {
                bool isUser = IsUserExist(id, u => u.Id);
                if (!isUser)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "user is not exists.";
                }
                else
                {
                    _objUsersV1 = new UsersV1();
                    _objUsersV1.Id = id;
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Save the user details on list while insert or update the user details 
        /// </summary>
        /// <returns>Response model</returns>
        public Response Save()
        {
            if (OperationTypes == enmOperationTypes.A)
            {
                try
                {
                    _lstUsersV1.Add(_objUsersV1);
                }
                catch (Exception ex)
                {
                    objResponse.IsError = true;
                    objResponse.Message = ex.Message;
                    throw ex;
                }

                objResponse.Message = "User added successfully";
            }

            if (OperationTypes == enmOperationTypes.E)
            {
                // get the user object from the list and update it with _objUsersV1 object
                UsersV1 user = _lstUsersV1.FirstOrDefault(u => u.Id == _objUsersV1.Id);

                user.Email = _objUsersV1.Email;
                user.FirstName = _objUsersV1.FirstName;
                user.LastName = _objUsersV1.LastName;
                user.Number = _objUsersV1.Number;
                objResponse.Message = "User updated successfully";
            }
            return objResponse;
        }

        /// <summary>
        /// login the user if user is validated
        /// </summary>
        /// <param name="objUsersV1">object of users - username and password</param>
        /// <returns>response model</returns>
        public Response Login(UsersV1 objUsersV1)
        {
            UsersV1 userV1 = _lstUsersV1.FirstOrDefault(u => u.UserName == objUsersV1.UserName && u.Password == objUsersV1.Password);

            if (userV1 != null)
            {
                BLAuth objBLAuth = new BLAuth();

                // get the token
                string token = objBLAuth.GenerateJWT(userV1.Id, userV1.UserName, userV1.Email, userV1.Role);

                if (token == null)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "something went wrong while generating token";
                }
                else
                {
                    object loginData = new
                    {
                        token,
                        userV1.Role
                    };

                    // Add the user details in cache
                    BLCache.Add("user", userV1);

                    objResponse.Message = "User logged in successfully";
                    objResponse.Data = loginData; 
                }
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "Incorrect username or password";
            }
            return objResponse;
        }

        /// <summary>
        /// Get user details based on user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>response model</returns>
        public Response GetUser(int id)
        {
            // try to get the user from cache 
            object userFromCache = BLCache.Get("user");
            if (userFromCache != null)
            {
                objResponse.Data = userFromCache;
            }
            else
            {
                UsersV1 user = _lstUsersV1.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User is not found";

                    return objResponse;
                }

                objResponse.Data = user;

            }
            return objResponse;
        }

        /// <summary>
        /// get user details based on user id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>object of the user if user is exist or else return null</returns>
        public UsersV1 GetUserObject(int id)
        {
            UsersV1 user = _lstUsersV1.FirstOrDefault(u => u.Id == id);
            return user;
        }

        /// <summary>
        /// List of all the user
        /// </summary>
        /// <returns>Response model</returns>
        public Response GetAllUser()
        {
            objResponse.Data = _lstUsersV1;
            return objResponse;
        }

        /// <summary>
        /// delete the user based on user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Response model</returns>
        public Response DeleteUser()
        {
            // remove the user object based on user id
            _lstUsersV1.RemoveAll(u => u.Id == _objUsersV1.Id);
            objResponse.Message = "User is deleted successfully";
            return objResponse;
        }
        #endregion
    }
}