using PartialClassAPI.Models;
using System.Collections.Generic;

namespace PartialClassAPI.BL
{
    /// <summary>
    /// partial class that manage the customer services
    /// </summary>
    public partial class BLCustomer
    {
        #region Private Member

        /// <summary>
        /// Customer's Id
        /// </summary>
        private static int _id = 1;

        /// <summary>
        /// list of the customer
        /// </summary>
        private static List<Customer> _lstCustomer = new List<Customer>();
        #endregion
    }
}