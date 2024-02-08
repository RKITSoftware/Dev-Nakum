using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank_Management_System.Models
{
    /// <summary>
    /// Schema of the user
    /// </summary>
    public class Use01
    {
        #region Public Properties
        /// <summary>
        /// Employee's id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int E01F01 { get; set; }

        /// <summary>
        /// Employee's Name
        /// </summary>
        public string E01F02 { get; set; }

        /// <summary>
        /// Employee's Password
        /// </summary>
        public string E01F03 { get; set; }

        /// <summary>
        /// Employee's Email
        /// </summary>
        public string E01F04 { get; set; }

        /// <summary>
        /// Employee's Money
        /// </summary>
        public int E01F05 { get; set; } = 0;

        /// <summary>
        /// Employee's Role
        /// </summary>
        public string E01F06 { get; set; } = "User";
        #endregion
    }
}