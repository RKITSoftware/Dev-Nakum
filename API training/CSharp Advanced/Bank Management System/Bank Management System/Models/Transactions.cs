using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank_Management_System.Models
{
    /// <summary>
    /// Schema of the Transactions
    /// </summary>
    public class Tra01
    {
        #region Public Properties
        /// <summary>
        /// Transaction's Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int A01F01 { get; set; }

        /// <summary>
        /// Employee's Id
        /// </summary>
        public int A01F02 { get; set; }

        /// <summary>
        /// Transactions Money
        /// </summary>
        public int A01F03 { get; set; }

        /// <summary>
        /// Deposit or withdraw
        /// </summary>
        public string A01F04 { get; set; }
        #endregion
    }
}