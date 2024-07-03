using Bank_Management_System.DB;
using Bank_Management_System.Enums;
using Bank_Management_System.Extensions;
using Bank_Management_System.Models;
using Bank_Management_System.Models.DTO;
using Bank_Management_System.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Bank_Management_System.Business_Logic
{
    /// <summary>
    /// Business Logic class for transaction-related operations.
    /// </summary>
    public class BLTransaction
    {
        #region Private Member
        /// <summary>
        /// create the object of the db connection for ORM
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// create the object of the db transaction
        /// </summary>
        private readonly DBTransaction _objDBTransactions;

        /// <summary>
        /// folder path for storing the statements
        /// </summary>
        private string _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Statements");

        /// <summary>
        /// object of the transaction
        /// </summary>
        private Tra01 _objTra01;

        /// <summary>
        /// object of the user
        /// </summary>
        private Use01 _objUse01;
        #endregion

        #region Public Member
        /// <summary>
        ///  object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Public properties
        /// <summary>
        /// transaction Types
        /// </summary>
        public enmTransactionTypes TransactionType { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the BLTransaction class.
        /// </summary>
        public BLTransaction()
        {
            _dbFactory = BLDbConnection.Instance;
            _objDBTransactions = new DBTransaction();
            objResponse = new Response();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Changes the key name for better readability in file writing.
        /// </summary>
        /// <param name="keyName">Original key name.</param>
        /// <returns>Modified key name.</returns>
        private string ChangeKeyName(string keyName)
        {
            switch (keyName)
            {
                case "A01101":
                    return "Id    ";
                case "E01102":
                    return "Name  ";
                case "E01104":
                    return "Email ";
                case "A01104":
                    return "Type  ";
                case "A01103":
                    return "Money ";
                default:
                    break;
            }
            return null;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// pre save the object before inserting or updating into database
        /// </summary>
        /// <param name="id">transaction Id</param>
        /// <param name="objDtoUse01">object of the user</param>
        public void PreSave(int id, DtoTra01 objDtoUse01)
        {
            _objTra01 = new Tra01();
            _objTra01 = objDtoUse01.Convert(_objTra01);
            _objTra01.A01F02 = id;
            _objTra01.A01F04 = TransactionType == enmTransactionTypes.D ? 'D' : 'W';
        }

        /// <summary>
        /// to check the validation before inserting or updating into database
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            BLUsers objBLUsers = new BLUsers();
            Use01 objUse01 = objBLUsers.GetUserObject(_objTra01.A01F02);

            if (objUse01 == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "User is not valid";
            }
            else
            {
                _objUse01 = objUse01;

                // update the balance using extension method
                objResponse = _objUse01.UpdateBalance(_objTra01.A01F03, TransactionType);
            }
            return objResponse;
        }

        /// <summary>
        /// insert or update the object into database
        /// </summary>
        /// <returns>response model</returns>
        public Response Save()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    using (IDbTransaction tx = db.BeginTransaction())
                    {
                        try
                        {
                            // update the user object 
                            db.Update(_objUse01);

                            // insert the record into database
                            db.Insert(_objTra01);


                            tx.Commit();
                            objResponse.Message = TransactionType == enmTransactionTypes.D ? "Successfully deposit the money into your account" : "successfully withdraw the money from your account";
                        }
                        catch (Exception ex)
                        {
                            BLErrorHandling.WriteFile($"CLTransactions ::  BLTransaction :: Save :: {ex.Message}");
                            tx.Rollback();
                            throw;
                        }
                    }

                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions ::  BLTransaction :: Save :: {ex.Message}");
                return objResponse;
            }
        }

        /// <summary>
        /// Retrieves details of all transactions from the database.
        /// </summary>
        /// <returns>all transaction details.</returns>
        public Response GetAllTransactions()
        {
            DataTable dtAllTransactions = _objDBTransactions.GetAllTransactions();
            if (dtAllTransactions.Rows.Count == 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Not found any transaction";
            }
            else
            {
                objResponse.Data = dtAllTransactions;
            }
            return objResponse;
        }

        /// <summary>
        /// Retrieves details of transactions for a specific user from the database.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>transaction details</returns>
        public Response GetTransactionByMe(int userId)
        {

            DataTable dtTransaction = _objDBTransactions.GetTransactionByMe(userId);
            if (dtTransaction.Rows.Count == 0)
            {
                objResponse.IsError = true;
                objResponse.Message = "Not found any transaction by your self";
            }
            else
            {
                objResponse.Data = dtTransaction;
            }

            return objResponse;
        }

        /// <summary>
        /// Writes transaction details to a file for a specific user.
        /// </summary>
        /// <param name="username">Username associated with the transactions.</param>
        /// <param name="lstDictTransactioins">List of dictionaries containing transaction details.</param>
        /// <returns>True if file writing is successful, false otherwise.</returns>
        public bool FileWrite(string username, DataTable lstDictTransactioins)
        {
            string fileName = username + ".txt";
            string filePath = Path.Combine(_folderPath, fileName);
            try
            {
                using (StreamWriter ojStreamWriter = new StreamWriter(filePath))
                {
                    int id = 1;
                    // Loop through each row in the DataTable
                    foreach (DataRow row in lstDictTransactioins.Rows)
                    {
                        ojStreamWriter.WriteLine($"Id: {id++}");

                        // Loop through each column in the current row
                        foreach (DataColumn column in lstDictTransactioins.Columns)
                        {
                            if (column.ColumnName == "A01101")
                            {
                                continue;
                            }

                            ojStreamWriter.WriteLine($"{ChangeKeyName(column.ColumnName)}: {row[column]}");
                        }

                        // Add a separator line between transactions for better readability
                        ojStreamWriter.WriteLine(new string('-', 20));
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions ::  BLTransaction :: FileWrite :: {ex.Message}");
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
                BLErrorHandling.WriteFile($"CLTransactions ::  BLTransaction :: DownloadFile :: {ex.Message}");
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
            DataTable dtTransaction = GetTransactionByMe(id).Data;

            // Check if the user has any transactions
            if (dtTransaction.Rows.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "You have not made any transactions.");
            }

            BLUsers objBLUsers = new BLUsers();
            Use01 objUse01 = objBLUsers.GetUserObject(id);

            try
            {
                if (objUse01 != null)
                {
                    string username = objUse01.E01F02;

                    if (FileWrite(username, dtTransaction))
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
            catch (Exception ex)
            {
                BLErrorHandling.WriteFile($"CLTransactions ::  BLTransaction :: Statements :: {ex.Message}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong.");
            }
        }
        #endregion
    }
}