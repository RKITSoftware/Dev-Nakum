using BMS.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            new UsersV2(){Id=1, FullName="Dev Nakum",UserName="deven",Password="Abcd@123", Number="9856895874",Email="dev@gmail.com",Money=0,Role="A"},
            new UsersV2(){Id=2, FullName="Kishan Nakum",UserName="kishan",Password="Abcd@123", Number="7458962351",Email="kishan@gmail.com",Money=0,Role="U"},
        };

        /// <summary>
        /// user id 
        /// </summary>
        private static int _id = 3;


        /// <summary>
        /// Object of the users
        /// </summary>
        private UsersV2 _objUsersV2;

       
        #endregion

        #region Public Member
        /// <summary>
        /// Object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Public Properties
        /// <summary>
        /// operation types - (I - Insert),(U - Update),(D - Delete)
        /// </summary>
        public EnmOperationTypes OperationTypes { get; set; }
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
            if (OperationTypes == EnmOperationTypes.A)
            {
                _objUsersV2.Id = _id++;
                _objUsersV2.UserName = objUsersV2.UserName;
                _objUsersV2.Password = objUsersV2.Password;
            }

            if (OperationTypes == EnmOperationTypes.E)
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
            if (OperationTypes == EnmOperationTypes.A)
            {
                bool isUsers = _lstUsersV2.Any(u => u.UserName == _objUsersV2.UserName);
                if (isUsers)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Username already exists.";
                }
            }

            if (OperationTypes == EnmOperationTypes.E)
            {
                bool isUsers = _lstUsersV2.Any(u => u.Id == _objUsersV2.Id);
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
            objResponse = new Response();
            if (OperationTypes == EnmOperationTypes.D)
            {
                UsersV2 user = _lstUsersV2.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "user is not exists.";
                }
                else
                {
                    _objUsersV2 = new UsersV2();
                    _objUsersV2 = user;
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
            objResponse = new Response();
            if (OperationTypes == EnmOperationTypes.A)
            {
                _lstUsersV2.Add(_objUsersV2);

                objResponse.Message = "User added successfully";
            }

            if (OperationTypes == EnmOperationTypes.E)
            {
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
            objResponse = new Response();
            UsersV2 userV1 = _lstUsersV2.FirstOrDefault(u => u.UserName == objUsersV2.UserName && u.Password == objUsersV2.Password);

            if (userV1 != null)
            {
                BLAuth objBLAuth = new BLAuth();
                string token = objBLAuth.GenerateJWT(userV1.Id, userV1.UserName, userV1.Email, userV1.Role);
                objResponse.Message = "User logged in successfully";

                DataTable dt = new DataTable();
                dt.Columns.Add("token", typeof(string));
                dt.Columns.Add("role", typeof(string));

                DataRow row = dt.NewRow();
                row["token"] = token;
                row["role"] = userV1.Role;

                dt.Rows.Add(row);
                objResponse.Data = dt;
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
            objResponse = new Response();
            UsersV2 user = _lstUsersV2.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "User is not found";

                return objResponse;
            }
            List<UsersV2> _lstUser = new List<UsersV2>()
            {
                user
            };

            objResponse.Data = _lstUser.ToDataTable();
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
            objResponse.Data = _lstUsersV2.ToDataTable();
            return objResponse;
        }

        /// <summary>
        /// delete the user based on user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Response model</returns>
        public Response DeleteUser()
        {
            objResponse = new Response();

            _lstUsersV2.Remove(_objUsersV2);
            objResponse.Message = "User is deleted successfully";
            return objResponse;
        }
        #endregion
    }
}