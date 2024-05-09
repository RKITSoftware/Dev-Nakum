using BMS.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace BMS.BL
{
    public class BLUsersV2
    {
        #region Private member
        /// <summary>
        /// Static user list
        /// </summary>
        private static List<UsersV2> _lstUsersV2 = new List<UsersV2>()
        {
            new UsersV2(){
                Id=1, FullName="Dev Nakum",UserName="deven",Password="Abcd@123", Number="9856895874",Email="dev@gmail.com",Money=0,Role="A"},
            };


        /// <summary>
        /// user id 
        /// </summary>
        private static int _id = 2;


        /// <summary>
        /// Object of the users
        /// </summary>
        private UsersV2 _objUsersV2;
        #endregion

        #region Constructor
        /// <summary>
        /// initialize the object of the response model
        /// </summary>
        public BLUsersV2()
        {
            objResponse = new Response();
        }
        #endregion


        #region Public Member
        /// <summary>
        /// Object of the response model
        /// </summary>
        public Response objResponse;
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
            //Minimum length(8 characters)
            //At least one uppercase letter
            //At least one lowercase letter
            //At least one number
            //At least one special character
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
        private bool IsUserExist<T>(T value, Func<UsersV2, T> selector)
        {
            return _lstUsersV2.Any(u => selector(u).Equals(value));
        }

        #endregion

        #region Public Method

        /// <summary>
        /// PreSave method for save the necessary data before adding into list
        /// </summary>
        /// <param name="objUsersV2">object of the user</param>
        /// <param name="id">user id only needed when update the user</param>
        public void PreSave(UsersV2 objUsersV2, int id = 0)
        {
            _objUsersV2 = new UsersV2();
            if (OperationTypes == enmOperationTypes.A)
            {
                _objUsersV2.Id = _id++;
                _objUsersV2.UserName = objUsersV2.UserName;
                _objUsersV2.Password = objUsersV2.Password;
            }

            if (OperationTypes == enmOperationTypes.E)
            {
                _objUsersV2.Id = id;
            }
            _objUsersV2.Email = objUsersV2.Email;
            _objUsersV2.Number = objUsersV2.Number;
            _objUsersV2.FullName = objUsersV2.FullName;
        }

        /// <summary>
        /// perform the validation while insert or update the user details into list
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();
            if (OperationTypes == enmOperationTypes.A)
            {
                // to validate username is exists or not
                bool isUsers = IsUserExist(_objUsersV2.UserName, u => u.UserName);

                // to validate the phone number 
                bool isValidNumber = _objUsersV2.Number.Length == 10;

                // to validate the password
                bool iSValidPassword = IsValidPassword(_objUsersV2.Password);

                // response set based on validations
                if (isUsers)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Username already exists.";
                }

                if (!isValidNumber)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Enter the valid phone number.";
                }

                if (!iSValidPassword)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Enter the valid password.";
                }
            }

            if (OperationTypes == enmOperationTypes.E)
            {
                bool isUsers = IsUserExist(_objUsersV2.Id, u => u.Id);
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
                    _objUsersV2 = new UsersV2();
                    _objUsersV2.Id = id;
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
                    _lstUsersV2.Add(_objUsersV2);
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
                UsersV2 user = _lstUsersV2.FirstOrDefault(u => u.Id == _objUsersV2.Id);

                user.Email = _objUsersV2.Email;
                user.FullName = _objUsersV2.FullName;
                user.Number = _objUsersV2.Number;
                objResponse.Message = "User updated successfully";
            }
            return objResponse;
        }

        /// <summary>
        /// login the user if user is validated
        /// </summary>
        /// <param name="objUsersV2">object of users - username and password</param>
        /// <returns>response model</returns>
        public Response Login(UsersV2 objUsersV2)
        {
            UsersV2 userV2 = _lstUsersV2.FirstOrDefault(u => u.UserName == objUsersV2.UserName && u.Password == objUsersV2.Password);

            if (userV2 != null)
            {
                BLAuth objBLAuth = new BLAuth();
                // get the token
                string token = objBLAuth.GenerateJWT(userV2.Id, userV2.UserName, userV2.Email, userV2.Role);

                object loginData = new
                {
                    token,
                    userV2.Role
                };

                // Add the user details in cache
                BLCache.Add("user", userV2);

                objResponse.Message = "User logged in successfully";
                objResponse.Data = loginData;
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
                UsersV2 user = _lstUsersV2.FirstOrDefault(u => u.Id == id);

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
        public UsersV2 GetUserObject(int id)
        {
            UsersV2 user = _lstUsersV2.FirstOrDefault(u => u.Id == id);
            return user;
        }

        /// <summary>
        /// List of all the user
        /// </summary>
        /// <returns>Response model</returns>
        public Response GetAllUser()
        {
            objResponse = new Response();
            objResponse.Data = _lstUsersV2;
            return objResponse;
        }

        /// <summary>
        /// delete the user based on user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Response model</returns>
        public Response DeleteUser()
        {
            _lstUsersV2.RemoveAll(u => u.Id == _objUsersV2.Id);
            objResponse.Message = "User is deleted successfully";
            return objResponse;
        }
        #endregion
    }
}