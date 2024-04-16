using BMS.Models;
using MoreLinq;
using System.Collections.Generic;

namespace BMS.BL
{
    /// <summary>
    ///  Manage the transaction's related services
    /// </summary>
    public class BLTransactions
    {
        #region Private Member
        /// <summary>
        /// Transaction's Id
        /// </summary>
        private static int _id = 1;

        /// <summary>
        /// static list to manage the all transactions
        /// </summary>
        private static List<Transactions> _lstTransaction = new List<Transactions>();

        /// <summary>
        /// Object of the transaction model
        /// </summary>
        private Transactions _objTransactions;

       
        /// <summary>
        /// Object of the user model
        /// </summary>
        private UsersV1 _objUsersV1;
        #endregion

        #region Public Member

        /// <summary>
        /// object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the user's objects
        /// </summary>
        public BLTransactions()
        {
            _objUsersV1 = new UsersV1();
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Transaction type - (D - Deposit, W - Withdraw)
        /// </summary>
        public EnmTransactionTypes TransactionType { get; set; }
        #endregion

        #region Public Method

        /// <summary>
        /// PreSave method for save the necessary data before adding into list
        /// </summary>
        /// <param name="userId">user id - currently logged in</param>
        /// <param name="objTransactions">object of the transaction - amount</param>
        public void PreSave(int userId, Transactions objTransactions)
        {
            objResponse = new Response();

            _objTransactions = new Transactions();
            _objTransactions.Id = _id++;
            _objTransactions.UserId = userId;
            _objTransactions.Money = objTransactions.Money;
            _objTransactions.Type = TransactionType == EnmTransactionTypes.D ? "D" : "W";

        }

        /// <summary>
        /// perform the validation on current logged in user
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();

            BLUsersV1 objBLUsersV1 = new BLUsersV1();
            UsersV1 objUsersV1 = objBLUsersV1.GetUserObject(_objTransactions.UserId);
            if (objUsersV1 == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "User is not found";
            }
            _objUsersV1 = objUsersV1;

            return objResponse;
        }

        /// <summary>
        /// Save the details into list based on transaction types
        /// </summary>
        /// <returns>response model</returns>
        public Response Save()
        {
            objResponse = new Response();

            if (TransactionType == EnmTransactionTypes.D)
            {
                _objUsersV1.Money += _objTransactions.Money;
            }
            else if (TransactionType == EnmTransactionTypes.W)
            {
                if (_objUsersV1.Money > _objTransactions.Money)
                {
                    _objUsersV1.Money -= _objTransactions.Money;
                }
                else
                {
                    objResponse.IsError = true;
                    objResponse.Message = "insufficient balance";
                    return  objResponse;
                }
            }

            objResponse.Message = "Transaction is done";
            _lstTransaction.Add(_objTransactions);

            return objResponse;
        }

        /// <summary>
        /// get all the list of transactions
        /// </summary>
        /// <returns>response model</returns>
        public Response GetAllTransactions()
        {
            objResponse = new Response();
            objResponse.Data = _lstTransaction.ToDataTable();
            return objResponse;
        }

        /// <summary>
        /// Get all the transaction which is done by logged in user 
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Response model</returns>
        public Response GetTransactions(int id)
        {
            objResponse = new Response();
            List<Transactions> userTransaction = _lstTransaction.FindAll(t => t.UserId == id);
            if (userTransaction.Count == 0)
            {
                objResponse.Message = "Transaction is not found by your self";    
            }
            else
            {
                objResponse.Data = userTransaction.ToDataTable();    
            }
            return objResponse;
        }
        #endregion
    }
}