using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbstractClassAPI.Models
{
    /// <summary>
    /// class which can represent the schema of customer 
    /// </summary>
    public class Customer
    {
        #region Public Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        #endregion
    }
}