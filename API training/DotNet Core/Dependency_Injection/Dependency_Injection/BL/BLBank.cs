using Dependency_Injection.Interface;
using Dependency_Injection.Model;

namespace Dependency_Injection.BL
{
    /// <summary>
    /// class of bank related transaction
    /// </summary>
    public class BLBank : IBank
    {
        #region Private member

        /// <summary>
        /// create the list for the manage the bank schema
        /// </summary>
        private List<Ban01> _lstBank = new List<Ban01>();
        #endregion


        #region Public Method
        /// <summary>
        /// Add transaction into list
        /// </summary>
        /// <param name="objBan01">object of bank</param>
        /// <returns>true or false for successfully add into list</returns>
        public bool AddTransaction(Ban01 objBan01)
        {
            _lstBank.Add(objBan01);
            return true;
        }
        #endregion
    }
}
