using MySql.Data.MySqlClient;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using System.Configuration;
using System.Data;

namespace SocialMediaAPI.DB
{
   /// <summary>
   /// Database related user's query
   /// </summary>
    public class DBUSE01: DBHelper, IDBUSE01
    {
        #region Publlic Member

        /// <summary>
        /// create the variable for connection string
        /// </summary>
        private readonly string _connectionString; 
        #endregion
        
        #region Constructor
        public DBUSE01(IConfiguration configuration)
        {
            // Retrieves the connection string named "Default" from configuration
            _connectionString = configuration.GetConnectionString("Default");
        } 
        #endregion

        #region Public Method
        /// <summary>
        /// Retrieves a list of all users from the database.
        /// </summary>
        /// <returns>response model</returns>
        public DataTable GetUsers()
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();
                string query = string.Format(@"SELECT 
                                    E01F01 AS E01101,
                                    E01F02 AS E01102,
                                    E01F03 AS E01103,
                                    E01F05 AS E01105,
                                    E01F06 AS E01106
                                FROM 
                                    USE01");
                return ExecuteQuery(objMySqlConnection, query);
            }

        }


        /// <summary>
        /// Retrieves user details based on the user ID from the HTTP context.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>response DataTable containing user details.</returns>
        public DataTable GetUserDetails(int id)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();
                string query = string.Format(@"SELECT 
                                    E01F02 AS E01102,
                                    E01F03 AS E01103,
                                    E01F05 AS E01105,
                                    E01F06 AS E01106 
                                FROM 
                                    Use01 
                                WHERE 
                                    E01F01 = @E01F01");

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@E01F01", id);

                MySqlDataAdapter objMySqlDataAdapter = new MySqlDataAdapter(objMySqlCommand);

                DataTable dtResponse = new DataTable();
                objMySqlDataAdapter.Fill(dtResponse);

                return dtResponse;
            }
        }

        /// <summary>
        /// Retrieves a list of usernames followed by the current user.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>response DataTable containing user details.</returns>
        public DataTable GetFollowing(int id)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();
                string query = string.Format(@"SELECT 
                                    E01F02
                                FROM
                                    Fol01 L01
                                JOIN
                                    Use01 E01 
                                ON 
                                    L01.L01F03 = E01.E01F01
                                WHERE
                                    L01.L01F02 = @L01F02");

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@L01F02", id);

                MySqlDataAdapter objMySqlDataAdapter = new MySqlDataAdapter(objMySqlCommand);

                DataTable dtResponse = new DataTable();
                objMySqlDataAdapter.Fill(dtResponse);

                return dtResponse;
            }
        }
        #endregion
    }
}
