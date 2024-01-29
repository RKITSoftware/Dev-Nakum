using PartialClassAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace PartialClassAPI.Controllers
{
    /// <summary>
    /// partial class which can handle the members and constructor
    /// </summary>
    public partial class CLCustomer1Controller : ApiController
    {
        #region Private Member
        private static int _id = 1;
        private static List<Customer> lstCustomer;
        #endregion

        #region Constructor
        /// <summary>
        /// initialize the list 
        /// </summary>
        static CLCustomer1Controller()
        {
            lstCustomer = new List<Customer>();
        }
        #endregion

    }
}
