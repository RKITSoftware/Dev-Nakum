using Bank_Management_System.Business_Logic;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bank_Management_System.DB
{
    /// <summary>
    /// Manage the transaction related queries
    /// </summary>
    public class DBTransaction
    {
        #region Private Member

        /// <summary>
        /// create the object of the db connection
        /// </summary>
        private readonly BLDbConnection _objBLDbConnection;

        /// <summary>
        /// create the object of the db helper for execute the query
        /// </summary>
        private readonly DBHelper _objDBHelper;
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the object of the DB connection
        /// </summary>
        public DBTransaction()
        {
            _objBLDbConnection = new BLDbConnection();
            _objDBHelper = new DBHelper();
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Retrieves details of all transactions from the database.
        /// </summary>
        /// <returns>all transaction details.</returns>
        public DataTable GetAllTransactions()
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_objBLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();

                string query = string.Format(@"SELECT 
	                                            A01F01 AS A01101,
	                                            E01F02 AS E01102,
                                                E01F04 AS E01104,
	                                            (CASE
                                                    WHEN A01F04 = 'D' THEN 'Deposit'
                                                    ELSE 'Withdraw'
                                                END) AS A01104,
	                                            A01F03 AS A01103
                                            FROM 
                                                tra01 a01 
                                            JOIN 
                                                use01 e01 
                                            ON 
                                                a01.A01F02 = e01.E01F01");

                return _objDBHelper.ExecuteQuery(objMySqlConnection, query);
            }
        }

        /// <summary>
        /// Retrieves details of transactions for a specific user from the database.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>transaction details</returns>
        public DataTable GetTransactionByMe(int userId)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_objBLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();

                string query = string.Format(@"SELECT 
                                                    A01F01 AS A01101,
                                                    E01F02 AS E01102,
                                                    E01F04 AS E01104,
                                                    (CASE
                                                        WHEN A01F04 = 'D' THEN 'Deposit'
                                                        ELSE 'Withdraw'
                                                    END) AS A01104,
                                                    A01F03 AS A01103
                                                FROM
                                                    TRA01 A01
                                                        JOIN
                                                    USE01 E01 ON A01.A01F02 = E01.E01F01
                                                WHERE
                                                    E01.E01F01 = {0}", userId);

                return _objDBHelper.ExecuteQuery(objMySqlConnection, query);
            }
        }

        #endregion
    }
}