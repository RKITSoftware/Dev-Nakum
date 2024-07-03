using Bank_Management_System.Enums;
using Bank_Management_System.Models;
using Bank_Management_System.Models.POCO;

namespace Bank_Management_System.Extensions
{
    /// <summary>
    /// extension methods for reuse the custom logic
    /// </summary>
    public static class Use01Extension
    {
        #region Public Method
        /// <summary>
        /// update the balance 
        /// </summary>
        /// <param name="_objUse01">object of the current logged in user</param>
        /// <param name="amount">total amount to update the balance</param>
        /// <param name="transactionTypes">transaction type - Deposit or withdraw</param>
        /// <returns></returns>
        public static Response UpdateBalance(this Use01 _objUse01, int amount, enmTransactionTypes transactionTypes)
        {
            Response objResponse = new Response();
            if (transactionTypes == enmTransactionTypes.D)
            {
                _objUse01.E01F05 += amount;
            }
            else if (transactionTypes == enmTransactionTypes.W)
            {
                if (_objUse01.E01F05 >= amount)
                {
                    _objUse01.E01F05 -= amount;
                }
                else
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Insufficient balance";
                }
            }
            return objResponse;
        }
        #endregion
    }
}