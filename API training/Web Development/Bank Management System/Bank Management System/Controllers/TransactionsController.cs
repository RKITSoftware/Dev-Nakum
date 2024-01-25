using Bank_Management_System.Basic_Auth;
using Bank_Management_System.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Web.Http;

namespace Bank_Management_System.Controllers
{
    /// <summary>
    /// Manage all the operation related bank account
    /// </summary>
    [Authentication]
    public class TransactionsController : ApiController
    {
        #region Private Member
        private static int _id = 1;
        #endregion

        #region Private Method
        /// <summary>
        /// get current user - user must be loggedIn
        /// </summary>
        /// <returns>userId</returns>
        internal string GetCurrentUser()
        {
            ClaimsPrincipal currentUser = User as ClaimsPrincipal;
            if (currentUser != null)
            {
                string userId = currentUser.FindFirst("Id")?.Value;

                return userId;
            }
            return null;
        }

        /// <summary>
        /// Add deposit or withdraw data into transaction model
        /// </summary>
        /// <param name="userId">logged user id</param>
        /// <param name="type">deposit or withdraw</param>
        /// <param name="money">money</param>
        private void AddTransaction(int userId, string type, int money)
        {
            Transaction objTransaction = new Transaction();
            objTransaction.Id = _id++;
            objTransaction.UserId = userId;
            objTransaction.Money = money;
            objTransaction.Type = type;

            Transaction.lstTransactions.Add(objTransaction);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// deposit the money into user account 
        /// </summary>
        /// <param name="id">userid</param>
        /// <param name="user">request body - money</param>
        /// <returns></returns>
        [Authorization(Roles ="User")]
        [HttpPatch]
        [Route("api/v1/accounts/deposit")]
        public HttpResponseMessage DepositMoney(UsersV1 user)
        {
            int userId = Convert.ToInt32(GetCurrentUser());

            UsersV1 objUser = UsersV1.lstUsers.FirstOrDefault(x=>x.Id == userId);

            if (objUser == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"user id {userId} is not found !!");
            }

            // to check money is exist or not in user object
            if (!(JObject.FromObject(user).TryGetValue("Money", out JToken money)))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please enter the money which you want to deposit");
            }
            objUser.Money += user.Money;


            // add entry into transaction model
            AddTransaction(userId,"Deposit",user.Money);

            return Request.CreateResponse(HttpStatusCode.OK,"Successfully deposit the money into your account");
        }


        /// <summary>
        /// withdraw the money into user account 
        /// </summary>
        /// <param name="id">userid</param>
        /// <param name="user">request body - money</param>
        /// <returns></returns>
        [Authorization(Roles = "User")]
        [HttpPatch]
        [Route("api/v1/accounts/withdraw")]
        public HttpResponseMessage WithdrawMoney(UsersV1 user)
        {
            int userId = Convert.ToInt32(GetCurrentUser());

            UsersV1 objUser = UsersV1.lstUsers.FirstOrDefault(x => x.Id == userId);

            if (objUser == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"user id {userId} is not found !!");
            }

            // to check money is exist or not in user object
            if (!(JObject.FromObject(user).TryGetValue("Money",out JToken money)))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please enter the money which you want to deposit");
            }
            objUser.Money -= user.Money;


            // add entry into transaction model
            AddTransaction(userId, "Withdraw", user.Money);

            return Request.CreateResponse(HttpStatusCode.OK, "Successfully withdraw the money from your account");
        }

        /// <summary>
        /// admin can see all the transaction which is done by users
        /// </summary>
        /// <returns></returns>
        [Authorization(Roles ="Admin")]
        [HttpGet]
        [Route("api/transactions")]
        public HttpResponseMessage GetAllTransactions()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Transaction.lstTransactions);
        }


        /// <summary>
        /// user can see all the transaction which is done by themselves
        /// </summary>
        /// <returns></returns>
        [Authorization(Roles = "User")]
        [HttpGet]
        [Route("api/user/transaction")]
        public HttpResponseMessage GetTransactions()
        {
            int userId = Convert.ToInt32(GetCurrentUser());
            List<Transaction> userTransactions = MemoryCache.Default.Get($"UserTransactions_{userId}") as List<Transaction>;

            if (userTransactions == null)
            {
                userTransactions = Transaction.lstTransactions.FindAll(x => x.UserId == userId);

                // Add user transactions to cache with a specific expiration time (e.g., 1 hour)
                MemoryCache.Default.Add($"UserTransactions_{userId}", userTransactions, DateTimeOffset.Now.AddHours(1));
            }

            return Request.CreateResponse(HttpStatusCode.OK, userTransactions);
        }
        #endregion
    }
}
