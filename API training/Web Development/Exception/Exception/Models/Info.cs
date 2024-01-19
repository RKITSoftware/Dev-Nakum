using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exception.Models
{
    /// <summary>
    /// Manage model of information
    /// </summary>
    public class Info
    {
        #region Public Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        #endregion
    }
}