using BMS.Basic_Auth;
using BMS.BL;
using BMS.Models;
using System;
using System.Security.Claims;
using System.Web.Http;

namespace BMS.Controllers
{
    /// <summary>
    /// manage the transaction related operations
    /// </summary>

    [Authentication]
    [RoutePrefix("api/transactions")]
    public class CLTransactionsController : ApiController
    {
        #region Private Member
        /// <summary>
        /// Initialize the object of the transaction's services
        /// </summary>
        private readonly BLTransactions _objBLTransactions;
        #endregion

        #region Public Member
        /// <summary>
        /// Object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Constructor
        /// <summary>
        /// create the object
        /// </summary>
        public CLTransactionsController()
        {
            _objBLTransactions = new BLTransactions();
            objResponse = new Response();
        }
        #endregion


        #region Private Method

        /// <summary>
        /// get the user id of logged user
        /// </summary>
        /// <returns>user id</returns>
        private int GetCurrentUser()
        {
            ClaimsPrincipal currentUser = User as ClaimsPrincipal;
            if (currentUser != null)
            {
                string userId = currentUser.FindFirst("Id")?.Value;

                return Convert.ToInt32(userId);
            }
            return 0;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Deposit the money into logged in user's amount
        /// </summary>
        /// <param name="objTransactions">object of the transaction contains money</param>
        /// <returns>response model</returns>
        [HttpPost]
        [Route("deposit")]
        public Response DepositMoney(Transactions objTransactions)
        {
            int userId = GetCurrentUser();

            _objBLTransactions.TransactionType = EnmTransactionTypes.D;
            _objBLTransactions.PreSave(userId, objTransactions);

            objResponse = _objBLTransactions.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _objBLTransactions.Save();
            }
            
            return objResponse;
        }

        /// <summary>
        /// withdraw the money into logged in user's amount
        /// </summary>
        /// <param name="objTransactions">object of the transaction contains money</param>
        /// <returns>response model</returns>
        [HttpPost]
        [Route("withdraw")]
        public Response WithdrawMoney(Transactions objTransactions)
        {
            int userId = GetCurrentUser();

            _objBLTransactions.TransactionType = EnmTransactionTypes.W;
            _objBLTransactions.PreSave(userId, objTransactions);

            objResponse = _objBLTransactions.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _objBLTransactions.Save();
            }

            return objResponse;
        }

        /// <summary>
        /// Get transaction by logged in user's
        /// </summary>
        /// <returns>response model</returns>
        [HttpGet]
        [Route("me")]
        public Response GetTransactionByMe()
        {
            int userId = GetCurrentUser();
            objResponse =  _objBLTransactions.GetTransactions(userId);
            return objResponse;
        }

        /// <summary>
        /// Get all transactions which is done by all the users
        /// </summary>
        /// <returns>response modal</returns>
        [Authorization(Roles ="A")]
        [HttpGet]
        [Route("all")]
        public Response GetAllTransactionBy()
        {
            objResponse = _objBLTransactions.GetAllTransactions();
            return objResponse;
        }


        #endregion
    }
}
