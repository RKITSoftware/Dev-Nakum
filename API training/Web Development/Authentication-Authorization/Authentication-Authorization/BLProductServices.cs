using Authentication_Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication_Authorization
{
    /// <summary>
    ///  Class which is create the singleton instance 
    /// </summary>
    public class BLProductServices
    {
        #region Private Member
        private static BLProductServices _instance;
        #endregion

        #region Public Properties
        /// <summary>
        /// Create the singleton instance 
        /// if object is created - return that object 
        /// else create the new object 
        /// </summary>
        public static BLProductServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BLProductServices();
                }
                return _instance;
            }
        }

        public List<Products> LstProducts { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// assign the LstProducts to static data
        /// </summary>
        public BLProductServices()
        {
            LstProducts = new List<Products>()
            {
                new Products() { Id = 1,Name = "Mobile Phones", Description="Its nice phone to use", Price=14999},
                new Products() { Id = 2,Name = "Laptop", Description="Its nice Laptop to use", Price=54999},
                new Products() { Id = 3,Name = "AC", Description="Good Products", Price=29999},
                new Products() { Id = 4,Name = "dwe", Description="dwrwr", Price=4999},
            };
        }
        #endregion
    }
}