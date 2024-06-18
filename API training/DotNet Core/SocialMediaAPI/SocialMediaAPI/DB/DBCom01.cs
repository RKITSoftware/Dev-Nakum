using MySql.Data.MySqlClient;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Model;
using System.Data;

namespace SocialMediaAPI.DB
{
    public class DBCOM01 : IDBCOM01
    {
        #region Publlic Member

        /// <summary>
        /// create the variable for connection string
        /// </summary>
        private readonly string _connectionString;
        #endregion

        #region Constructor
        public DBCOM01(IConfiguration configuration)
        {
            // Retrieves the connection string named "Default" from configuration
            _connectionString = configuration.GetConnectionString("Default");
        }
        #endregion

        /// <summary>
        /// update comment content
        /// </summary>
        /// <param name="commentId">id of comments</param>
        /// <param name="commentContent">content of comment</param>
        /// <returns>true if comment updated or else false</returns>
        public bool UpdateComment(int commentId, string commentContent)
        {
            // for update the only comment's content
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();

                string query = @"UPDATE 
                                    Com01 
                                SET 
                                    M01F04 = @M01F04 
                                WHERE 
                                    M01F01 = @M01F01";

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@M01F04", commentContent);
                objMySqlCommand.Parameters.AddWithValue("@M01F01", commentId);

                return objMySqlCommand.ExecuteNonQuery() > 0;

            }
        }

        /// <summary>
        /// Gets all comments for a specific post.
        /// </summary>
        /// <param name="id">The ID of the post to get comments for.</param>
        /// <returns>response DataTable</returns>
        public DataTable GetAllCommentsOnPost(int id)
        {
           using (MySqlConnection objMySqlConnection = new MySqlConnection(_connectionString))
            {
                objMySqlConnection.Open();
                string query = @"SELECT 
                                    M01F01, 
                                    E01F02,
                                    M01F04, 
                                    M01F05 
                                FROM 
                                    Com01 
                                JOIN 
                                    Use01 
                                ON 
                                    Com01.M01F03 = Use01.E01F01 
                                WHERE 
                                    M01F02 = @M01F02";

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@M01F02", id);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                DataTable dtAllCommentsOnPost = new DataTable();
                dtAllCommentsOnPost.Load(objMySqlDataReader);

                return dtAllCommentsOnPost;
            }
        }
    }
}
