using Bank_Management_System.Models;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;

namespace Bank_Management_System.Business_Logic
{
    /// <summary>
    /// Business Logic class for user-related operations.
    /// </summary>
    public class BLUsers
    {
        #region Private Member
        private readonly IDbConnectionFactory _dbFactory;
        private BLDbConnection _objBLDbConnection;
        private BLHashing _objBLHashing;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the BLUsers class.
        /// </summary>
        public BLUsers()
        {
            _dbFactory = BLDbConnection.Instance;
            _objBLDbConnection = new BLDbConnection();
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Handles user registration by adding a new user to the database.
        /// </summary>
        /// <param name="objUse01">User details for registration.</param>
        /// <returns>The registered user or null in case of failure.</returns>
        public Use01 SignUp(Use01 objUse01)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    objUse01.E01F03 = _objBLHashing.HashPassword(objUse01.E01F03);
                    db.Insert(objUse01);
                    return objUse01;
                }
                catch (MySqlException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Handles user login by verifying credentials and generating a JWT token.
        /// </summary>
        /// <param name="objUse01">User credentials for login.</param>
        /// <returns>The JWT token or null in case of invalid credentials.</returns>
        public string LogIn(Use01 objUse01)
        {
            MySqlConnection objMySqlConnection = new MySqlConnection(_objBLDbConnection.GetConnectionString());
            objMySqlConnection.Open();

            string query = @"select E01F01,E01F02,E01F03,E01F04,E01F06 from use01 where E01F04 = @E01F04";
            MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
            objMySqlCommand.Parameters.AddWithValue("@E01F04", objUse01.E01F04);

            MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

            while (objMySqlDataReader.Read())
            {
                Use01 user = new Use01
                {
                    E01F01 = Convert.ToInt32(objMySqlDataReader["E01F01"]),
                    E01F02 = objMySqlDataReader["E01F02"].ToString(),
                    E01F03 = objMySqlDataReader["E01F03"].ToString(),
                    E01F04 = objMySqlDataReader["E01F04"].ToString(),
                    E01F06 = objMySqlDataReader["E01F06"].ToString(),
                };
                _objBLHashing = new BLHashing();
                if (_objBLHashing.Verify(objUse01.E01F03, user.E01F03))
                {
                    // add JWT and add claims
                    BLAuth objBLAuth = new BLAuth();
                    string jwt = objBLAuth.GenerateJWT(user.E01F01, user.E01F02, user.E01F04, user.E01F06);
                    objMySqlConnection.Close();
                    return jwt;
                }
            }
            objMySqlConnection.Close();
            return null;
        }

        /// <summary>
        /// Retrieves details of a specific user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>The user details or null if not found.</returns>
        public Use01 GetUser(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<Use01>(id);
            }
        }

        /// <summary>
        /// Retrieves details of all users from the database.
        /// </summary>
        /// <returns>A list of user details or null in case of failure.</returns>
        public List<Use01> GetAllUser()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    return db.Select<Use01>();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Updates user details in the database.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <param name="objUse01">Updated user details.</param>
        /// <returns>True if the update is successful, false otherwise.</returns>
        public bool UpdateUser(int id, Use01 objUse01)
        {
            try
            {
                using (MySqlConnection objMySqlConnection = new MySqlConnection(_objBLDbConnection.GetConnectionString()))
                {
                    objMySqlConnection.Open();
                    string query = @"update use01 set E01F02 = @E01F02 where E01F01 = @E01F01";
                    MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                    objMySqlCommand.Parameters.AddWithValue("@E01F01", id);
                    objMySqlCommand.Parameters.AddWithValue("@E01F02", objUse01.E01F02);

                    return objMySqlCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>True if the deletion is successful, false otherwise.</returns>
        public bool DeleteUser(int id)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    return db.DeleteById<Use01>(id) > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
