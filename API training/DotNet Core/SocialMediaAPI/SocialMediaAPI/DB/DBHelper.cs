using MySql.Data.MySqlClient;
using System.Data;

namespace SocialMediaAPI.DB
{
    /// <summary>
    /// Execute the query 
    /// </summary>
    public class DBHelper
    {
        #region Public Method
        /// <summary>
        /// Execute the query based on provided connection and query
        /// </summary>
        /// <param name="objMySqlConnection">MySQL connection</param>
        /// <param name="query">MySQL query</param>
        /// <returns>response in the data table</returns>
        public DataTable ExecuteQuery(MySqlConnection objMySqlConnection, string query)
        {
            MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
            MySqlDataAdapter objMySqlDataAdapter = new MySqlDataAdapter(objMySqlCommand);

            DataTable dtResponse = new DataTable();
            objMySqlDataAdapter.Fill(dtResponse);

            return dtResponse;
        }
        #endregion
    }
}
