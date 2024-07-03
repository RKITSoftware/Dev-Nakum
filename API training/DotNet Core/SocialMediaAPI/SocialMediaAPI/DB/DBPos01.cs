using MySql.Data.MySqlClient;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using System.Data;

namespace SocialMediaAPI.DB
{
    /// <summary>
    /// Database related post's query
    /// </summary>
    public class DBPOS01 : DBHelper, IDBPOS01
    {
        #region Publlic Member

        /// <summary>
        /// create the variable for connection string
        /// </summary>
        private readonly string _connectionString;
        #endregion

        #region Constructor
        public DBPOS01(IConfiguration configuration)
        {
            // Retrieves the connection string named "Default" from configuration
            _connectionString = configuration.GetConnectionString("Default");
        }
        #endregion

        #region Public Method
        /// <summary>
        /// get the user id and image of post
        /// </summary>
        /// <param name="postId">id of post</param>
        /// <returns>userId and url of post</returns>
        public (int, string) GetUserIdAndImgOfPost(int postId)
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
                return (0, "");////
            }
        }

        /// <summary>
        /// Retrieves a list of all posts from the database.
        /// </summary>
        /// <returns>response DataTable</returns>
        public DataTable GetPosts()
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
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
                return ExecuteQuery(objMySqlConnection, query);

            }
        }

        /// <summary>
        /// Retrieves a list of posts created by the current user.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>response datatable</returns>
        public DataTable GetPostByMe(int id)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();

                string query = @"SELECT 
                                    S01F01, 
                                    S01F03, 
                                    S01F04, 
                                    S01F05 
                                FROM 
                                    Pos01 
                                WHERE 
                                    S01F02 = @S01F02";
                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@S01F02", id);

                MySqlDataAdapter objMySqlDataAdapter = new MySqlDataAdapter(objMySqlCommand);

                DataTable dtResponse = new DataTable();
                objMySqlDataAdapter.Fill(dtResponse);

                return dtResponse;
            }

        } 
        #endregion
    }
}
