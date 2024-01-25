using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT.Models
{
    /// <summary>
    /// class which can contains schema of employees
    /// </summary>
    public class Employee
    {
        #region Public Properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        #endregion

        
    }




}