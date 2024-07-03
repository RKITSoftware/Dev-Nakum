using Bank_Management_System.Enums;
using Bank_Management_System.Extensions;
using Bank_Management_System.Models;
using Bank_Management_System.Models.DTO;
using Bank_Management_System.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bank_Management_System.Business_Logic
{
    /// <summary>
    /// Business Logic class for user-related operations.
    /// </summary>
    public class BLUsers
    {
        #region Private Member
        /// <summary>
        /// Create the object of the Orm Lite connection
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// create the object of the encryption and decryptions
        /// </summary>
        private readonly BLSecurity _objBLSecurity;

        /// <summary>
        /// Create the object of the user model
        /// </summary>
        private Use01 _objUse01;
        #endregion

        #region Public Member
        /// <summary>
        /// Create the object of the response model
        /// </summary>
        public Response objResponse;
        #endregion          

        #region Public Properties
        /// <summary>
        /// Declare the operation type properties
        /// </summary>
        public enmOperationTypes OperationType { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the BLUsers class.
        /// </summary>
        public BLUsers()

        {
            _dbFactory = BLDbConnection.Instance;
            _objBLSecurity = new BLSecurity();
            objResponse = new Response();
        }

        #endregion

        #region Private Method

        /// <summary>
        ///  To check whether email is unique in data base or not
        /// </summary>
        /// <param name="email">user's email</param>
        /// <returns>true if unique email or else false</returns>
        private bool IsUniqueEmail(string email)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    //Use01 user = db.SingleWhere<Use01>("E01F04", email);
                    //return user == null;
                    return !( db.Exists<Use01>(u=>u.E01F04 == email));
                }
            }
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions :: BLUsers :: IsUniqueEmail :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// to check whether email is valid or not
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>true if email is valid or else false</returns>
        private bool IsValidEmail(string email)
        {
            string emailRegex = "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsValidId(int id)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    return db.Exists<Use01>(u => u.E01F01 == id);
                }
            }
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions :: BLUsers :: IsValidId :: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Public Method

        /// <summary>
        /// /Get the user details based on user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>model of user's details</returns>
        public Use01 GetUserObject(int id)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    Use01 user = db.SingleById<Use01>(id);
                    return user;
                }
            }
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions :: BLUsers :: GetUserObject :: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// PreSave method for save the necessary data before adding into database
        /// </summary>
        /// <param name="objDtoUse01">object of the user</param>
        /// <param name="id">user id - required only update the user</param>
        public void PreSave(DtoUse01 objDtoUse01, int id = 0)
        {
            // covert DTO to POCO
            _objUse01 = objDtoUse01.Convert(_objUse01);
            if (OperationType == enmOperationTypes.A)
            {
                // convert from text password to hash password
                //_objUse01.E01F03 = _objBLHashing.HashPassword(_objUse01.E01F03);
                _objUse01.E01F03 = _objBLSecurity.Encrypt(_objUse01.E01F03);
            }
            else if (OperationType == enmOperationTypes.E)
            {
                // assign the user's id
                _objUse01.E01F01 = id;
            }
        }

        /// <summary>
        /// perform the validation while insert or update the user details into database
        /// </summary>
        /// <returns>Response model</returns>
        public Response ValidationOnSave()
        {
            if (OperationType == enmOperationTypes.A)
            {
                bool isValidEmail = IsValidEmail(_objUse01.E01F04);
                if (!isValidEmail)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Enter the valid email.";
                }

                bool isUniqueEmail = IsUniqueEmail(_objUse01.E01F04);
                if (!isUniqueEmail)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Email is already exist!!";
                }


            }
            else if (OperationType == enmOperationTypes.E)
            {
                Use01 user = GetUserObject(_objUse01.E01F01);
                if (user == null)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User is not valid";
                }
                else
                {
                    // for update the entire the objects
                    _objUse01.E01F03 = user.E01F03;
                    _objUse01.E01F04 = user.E01F04;
                    _objUse01.E01F05 = user.E01F05;
                    _objUse01.E01F06 = user.E01F06;
                }
            }
            return objResponse;
        }

        /// <summary>
        /// perform the validation while delete the user using user-id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Response model</returns>
        public Response ValidationOnDelete(int id)
        {
            if (OperationType == enmOperationTypes.D)
            {
                // only id
                bool isValidUser = IsValidId(id);
                if (!isValidUser)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User is not found";
                }
                else
                {
                    _objUse01 = new Use01();
                    _objUse01.E01F01 = id;
                }
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "Operation type is not valid";
            }
            return objResponse;
        }

        /// <summary>
        /// save the user details into database while insert or update the user
        /// </summary>
        /// <returns>Response model</returns>
        public Response Save()
        {
            try
            {
                if (OperationType == enmOperationTypes.A)
                {
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        db.Insert(_objUse01);
                        objResponse.Message = "User is added successfully";
                    }
                }
                else if (OperationType == enmOperationTypes.E)
                {
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        db.Update(_objUse01);
                        objResponse.Message = "User is updated successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions :: BLUsers :: Save :: {ex.Message}");
            }

            return objResponse;
        }

        /// <summary>
        /// Handles user login by verifying credentials and generating a JWT token.
        /// </summary>
        /// <param name="objUse01">User credentials for login.</param>
        /// <returns>Response model</returns>
        public Response LogIn()
        {
            try
            {
                // encrypt the password
                _objUse01.E01F03 = _objBLSecurity.Encrypt( _objUse01.E01F03 );
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // Get the user information based on email and password
                    Use01 user = db.Select<Use01>(u=>u.E01F04 == _objUse01.E01F04 && u.E01F03 == _objUse01.E01F03).FirstOrDefault();

                    // if user is not found
                    if (user == null)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Incorrect username or password";
                    }
                    else
                    {
                        BLAuth objBLAuth = new BLAuth();

                        // generate the JWT token
                        string jwt = objBLAuth.GenerateJWT(user.E01F01, user.E01F02, user.E01F04, user.E01F06.ToString());
                        string role = user.E01F06.ToString();

                        if (jwt == null)
                        {
                            objResponse.IsError = true;
                            objResponse.Message = "something went wrong while generating the token";
                        }
                        else
                        {
                            List<string> list = new List<string>
                            {
                                jwt,
                                role
                            };
                            objResponse.Data = list;
                        }
                    }

                    return objResponse;



                    ////Get the user information based on email
                    //Use01 user = db.SingleWhere<Use01>("E01F04", _objUse01.E01F04);

                    //// if user is not found     
                    //if (user == null)
                    //{
                    //    objResponse.IsError = true;
                    //    objResponse.Message = "Invalid email or password";
                    //}
                    //else
                    //{
                    //    // verify password 
                    //    bool isVerifyPassword = _objBLHashing.Verify(_objUse01.E01F03, user.E01F03);

                    //    //if password is verified generate the jwt token
                    //    if (isVerifyPassword)
                    //    {
                    //        BLAuth objBLAuth = new BLAuth();

                    //        string jwt = objBLAuth.GenerateJWT(user.E01F01, user.E01F02, user.E01F04, user.E01F06.ToString());
                    //        string role = user.E01F06.ToString();

                    //        if (jwt == null)
                    //        {
                    //            objResponse.IsError = true;
                    //            objResponse.Message = "something went wrong while generating the token";
                    //        }
                    //        else
                    //        {
                    //            List<string> list = new List<string>
                    //            {
                    //                jwt,
                    //                role
                    //            };
                    //            objResponse.Data = list;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        objResponse.IsError = true;
                    //        objResponse.Message = "Invalid email or password";
                    //    }
                    //}
                    //return objResponse;
                }
            }
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions :: BLUsers :: LogIn :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// Retrieves details of a specific user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>Response model</returns>
        public Response GetUser(int id)   
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    Use01 user = db.SingleById<Use01>(id);
                   
                    if (user != null)
                    {
                        objResponse.Data = new
                        {
                            E01101 = user.E01F01,
                            E01102 = user.E01F02,
                            E01104 = user.E01F04,
                            E01105 = user.E01F05
                        };
                    }
                    else
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "User is not found";
                    }
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions :: BLUsers :: GetUser :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// Retrieves details of all users from the database.
        /// </summary>
        /// <returns>Response model</returns>
        public Response GetAllUser()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    List<Use01> lstUe01 = db.Select<Use01>();
                    if (lstUe01.Count > 0)
                    {
                        objResponse.Data = lstUe01;
                    }
                    else
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "User is not found";
                    }
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions :: BLUsers :: GetAllUser :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>Response model</returns>
        public Response Delete()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    int noOfDeletedRows = db.DeleteById<Use01>(_objUse01.E01F01);

                    if (noOfDeletedRows > 0)
                    {
                        objResponse.Message = "User is successfully deleted";
                    }
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions :: BLUsers :: Delete :: {ex.Message}");
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        #endregion
    }
}