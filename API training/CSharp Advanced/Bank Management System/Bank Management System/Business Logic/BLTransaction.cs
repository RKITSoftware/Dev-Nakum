using Bank_Management_System.Models;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace Bank_Management_System.Business_Logic
{
    /// <summary>
    /// Business Logic class for transaction-related operations.
    /// </summary>
    public class BLTransaction
    {
        #region Private Member
        private readonly IDbConnectionFactory _dbFactory;
        private BLDbConnection _objBLDbConnection;
        private string _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Statements");
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the BLTransaction class.
        /// </summary>
        public BLTransaction()
        {
            _dbFactory = BLDbConnection.Instance;
            _objBLDbConnection = new BLDbConnection();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Changes the key name for better readability in file writing.
        /// </summary>
        /// <param name="keyName">Original key name.</param>
        /// <returns>Modified key name.</returns>
        private string changeKeyName(string keyName)
        {
            switch (keyName)
            {
                case "A01F01":
                    return "Id    ";
                case "E01F02":
                    return "Name  ";
                case "E01F04":
                    return "Email ";
                case "A01F04":
                    return "Type  ";
                case "A01F03":
                    return "Money ";
                default:
                    break;
            }
            return null;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Adds a new transaction, updates user's money, and records the transaction details in the database.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="money">Transaction amount.</param>
        /// <param name="type">Transaction type (Deposit or Withdraw).</param>
        /// <returns>True if the transaction is successful, false otherwise.</returns>
        public bool AddTransaction(int userId, int money, string type)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    BLUsers objBLUsers = new BLUsers();
                    Use01 objUse01 = objBLUsers.GetUser(userId);

                    if (type == "Deposit")
                    {
                        objUse01.E01F05 += money;
                    }
                    else if (type == "Withdraw")
                    {
                        objUse01.E01F05 -= money;
                    }
                    int updateRow = db.Update(objUse01);

                    if (updateRow > 0)
                    {
                        Tra01 objTRA01 = new Tra01();
                        objTRA01.A01F02 = userId;
                        objTRA01.A01F03 = money;
                        objTRA01.A01F04 = type;

                        db.Insert(objTRA01);
                    }
                    else
                    {
                        return false;
                    }

                    return true;
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Retrieves details of all transactions from the database.
        /// </summary>
        /// <returns>A list of dictionaries containing transaction details.</returns>
        public List<Dictionary<string, string>> GetAllTransactions()
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_objBLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();

                string query = @"SELECT 
	                                A01F01,
	                                E01F02,
                                    E01F04,
	                                A01F04,
	                                A01F03
                                FROM 
                                    tra01 t 
                                JOIN 
                                    use01 u 
                                ON 
                                    t.A01F02 = u.E01F01";

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                List<Dictionary<string, string>> lstTra01 = new List<Dictionary<string, string>>();
                while (objMySqlDataReader.Read())
                {
                    lstTra01.Add(new Dictionary<string, string>
                    {
                        {"A01F01" , objMySqlDataReader["A01F01"].ToString() },
                        {"E01F02" , objMySqlDataReader["E01F02"].ToString() },
                        {"E01F04" , objMySqlDataReader["E01F04"].ToString() },
                        {"A01F04" , objMySqlDataReader["A01F04"].ToString() },
                        {"A01F03" , objMySqlDataReader["A01F03"].ToString() }
                    });
                }

                return lstTra01;
            }
        }

        /// <summary>
        /// Retrieves details of transactions for a specific user from the database.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>A list of dictionaries containing transaction details for the specified user.</returns>
        public List<Dictionary<string, string>> GetTransactionByMe(int userId)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(_objBLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();

                string query = @"SELECT 
	                                A01F01,
	                                E01F02,
                                    E01F04,
	                                A01F04,
	                                A01F03
                                FROM 
                                    tra01 t 
                                JOIN 
                                    use01 u 
                                ON 
                                    t.A01F02 = u.E01F01
                                WHERE
                                    u.E01F01 = @E01F01";

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@E01F01", userId);
                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                List<Dictionary<string, string>> lstTra01 = new List<Dictionary<string, string>>();
                while (objMySqlDataReader.Read())
                {
                    lstTra01.Add(new Dictionary<string, string>
                    {
                        {"A01F01" , objMySqlDataReader["A01F01"].ToString() },
                        {"E01F02" , objMySqlDataReader["E01F02"].ToString() },
                        {"E01F04" , objMySqlDataReader["E01F04"].ToString() },
                        {"A01F04" , objMySqlDataReader["A01F04"].ToString() },
                        {"A01F03" , objMySqlDataReader["A01F03"].ToString() }
                    });
                }

                return lstTra01;
            }
        }

        /// <summary>
        /// Writes transaction details to a file for a specific user.
        /// </summary>
        /// <param name="username">Username associated with the transactions.</param>
        /// <param name="lstDictTransactioins">List of dictionaries containing transaction details.</param>
        /// <returns>True if file writing is successful, false otherwise.</returns>
        public bool FileWrite(string username, List<Dictionary<string, string>> lstDictTransactioins)
        {
            string fileName = username + ".txt";
            string filePath = Path.Combine(_folderPath, fileName);
            try
            {
                using (StreamWriter ojStreamWriter = new StreamWriter(filePath))
                {
                    int id = 1;
                    foreach (var dictionary in lstDictTransactioins)
                    {
                        // Write each dictionary as a line in the file
                        ojStreamWriter.WriteLine($"Id    : {id++}");
                        foreach (var kvp in dictionary)
                        {
                            if (kvp.Key == "A01F01")
                            {
                                continue;
                            }
                            ojStreamWriter.WriteLine($"{changeKeyName(kvp.Key)}: {kvp.Value}");
                        }

                        // Add a separator line between dictionaries for better readability
                        ojStreamWriter.WriteLine(new string('-', 20));
                    }
                    return true;
                }
            }
            catch (IOException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Downloads the file containing transaction details for a specific user.
        /// </summary>
        /// <param name="username">Username associated with the transactions.</param>
        /// <returns>HttpResponseMessage containing the file to be downloaded.</returns>
        public HttpResponseMessage DownloadFile(string username)
        {
            try
            {
                string fileName = username + ".txt";
                string filePath = Path.Combine(_folderPath, fileName);

                // check file exists
                if (!File.Exists(filePath))
                {
                    throw new Exception("File Not Found");
                }

                // Read the file content
                byte[] fileBytes = File.ReadAllBytes(filePath);

                // Create a response with the file content
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(fileBytes);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Generates a statement file for a specific user and initiates the file download.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <param name="Request">HttpRequestMessage used for creating error responses.</param>
        /// <returns>HttpResponseMessage containing the file to be downloaded or an error response.</returns>
        public HttpResponseMessage Statements(int id, HttpRequestMessage Request)
        {
            List<Dictionary<string, string>> lstDictTransactions = GetTransactionByMe(id);

            // Check if the user has any transactions
            if (lstDictTransactions.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "You have not made any transactions.");
            }

            BLUsers objBLUsers = new BLUsers();
            Use01 objUse01 = objBLUsers.GetUser(id);

            try
            {
                if (objUse01 != null)
                {
                    string username = objUse01.E01F02;

                    if (FileWrite(username, lstDictTransactions))
                    {
                        return DownloadFile(username);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to write the file.");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found.");
                }
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong.");
            }
        }
        #endregion
    }
}