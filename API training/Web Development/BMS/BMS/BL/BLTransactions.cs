using BMS.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

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
            objResponse = new Response();
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Transaction type - (D - Deposit, W - Withdraw)
        /// </summary>
        public enmTransactionTypes TransactionType { get; set; }
        #endregion


        #region Public Method

        /// <summary>
        /// PreSave method for save the necessary data before adding into list
        /// </summary>
        /// <param name="userId">user id - currently logged in</param>
        /// <param name="objTransactions">object of the transaction - amount</param>
        public void PreSave(int userId, Transactions objTransactions)
        {
            _objTransactions = new Transactions
            {
                Id = _id++,
                UserId = userId,
                Money = objTransactions.Money,
                Type = TransactionType == enmTransactionTypes.D ? "Deposit" : "Withdraw"
            };

        }

        /// <summary>
        /// perform the validation on current logged in user
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            BLUsersV1 objBLUsersV1 = new BLUsersV1();
            UsersV1 objUsersV1 = objBLUsersV1.GetUserObject(_objTransactions.UserId);
            if (objUsersV1 == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "User is not found";
            }
            else
            {
                // required for update the money based on transaction types
                _objUsersV1 = objUsersV1;

                // to validate the condition of the insufficient balance
                if(TransactionType == enmTransactionTypes.W && _objUsersV1.Money < _objTransactions.Money)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "insufficient balance";
                }
            }

            return objResponse;
        }

        /// <summary>
        /// Save the details into list based on transaction types
        /// </summary>
        /// <returns>response model</returns>
        public Response Save()
        {
            if (TransactionType == enmTransactionTypes.D)
            {
                _objUsersV1.Money += _objTransactions.Money;
            }
            else if (TransactionType == enmTransactionTypes.W)
            {
                _objUsersV1.Money -= _objTransactions.Money;
            }

            try
            {
                _lstTransaction.Add(_objTransactions);

                BLCache.Add("user", _objUsersV1);
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                throw ex;
            }
            objResponse.Message = "Transaction is done";
            return objResponse;
        }

        /// <summary>
        /// get all the list of transactions
        /// </summary>
        /// <returns>response model</returns>
        public Response GetAllTransactions()
        {
            objResponse.Data = _lstTransaction;
            return objResponse;
        }

        /// <summary>
        /// Get all the transaction which is done by logged in user 
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Response model</returns>
        public Response GetTransactions(int id)
        {
            List<Transactions> userTransaction = _lstTransaction.Select(t => t)
                                                   .Where(t => t.UserId == id)
                                                   .ToList();
            //List<Transactions> userTransaction = _lstTransaction.FindAll(t => t.UserId == id);
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