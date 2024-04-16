using BMS.Models;
using MoreLinq;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

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
            new UsersV1(){Id=1, FirstName="Dev",LastName="Nakum",UserName="deven",Password="Abcd@123", Number="9856895874",Email="dev@gmail.com",Money=0,Role="A"},
            new UsersV1(){Id=2, FirstName="Kishan",LastName="Nakum",UserName="kishan",Password="Abcd@123", Number="7458962351",Email="kishan@gmail.com",Money=0,Role="U"},
        };

        /// <summary>
        /// user id 
        /// </summary>
        private static int _id = 3;

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
        /// <param name="objUsersV1">object of the user</param>
        /// <param name="id">user id only needed when update the user</param>
        public void PreSave(UsersV1 objUsersV1, int id = 0)
        {
            _objUsersV1 = new UsersV1();
            if (OperationTypes == EnmOperationTypes.A)
            {
                _objUsersV1.Id = _id++;
                _objUsersV1.UserName = objUsersV1.UserName;
                _objUsersV1.Password = objUsersV1.Password;
            }

            if (OperationTypes == EnmOperationTypes.E)
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
            objResponse = new Response();
            if (OperationTypes == EnmOperationTypes.A)
            {
                bool isUsers = _lstUsersV1.Any(u => u.UserName == _objUsersV1.UserName);
                if (isUsers)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Username already exists.";
                }
            }

            if (OperationTypes == EnmOperationTypes.E)
            {
                bool isUsers = _lstUsersV1.Any(u => u.Id == _objUsersV1.Id);
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
                UsersV1 user = _lstUsersV1.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "user is not exists.";
                }
                else
                {
                    _objUsersV1 = new UsersV1();
                    _objUsersV1 = user;
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
                _lstUsersV1.Add(_objUsersV1);

                objResponse.Message = "User added successfully";
            }

            if (OperationTypes == EnmOperationTypes.E)
            {
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
            objResponse = new Response();
            UsersV1 userV1 = _lstUsersV1.FirstOrDefault(u => u.UserName == objUsersV1.UserName && u.Password == objUsersV1.Password);

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
            UsersV1 user = _lstUsersV1.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "User is not found";

                return objResponse;
            }
            List<UsersV1> _lstUser = new List<UsersV1>()
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
            objResponse = new Response();
            objResponse.Data = _lstUsersV1.ToDataTable();
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

            _lstUsersV1.Remove(_objUsersV1);
            objResponse.Message = "User is deleted successfully";
            return objResponse;
        }
        #endregion
    }
}