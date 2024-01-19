using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace JWT.Models
{
    public class BLEmployeeService
    {
        #region Private Member
        private static BLEmployeeService _instance;
        #endregion

        #region Public Properties

        /// <summary>
        /// Create the singleton instance 
        /// if object is created - return that object 
        /// else create the new object 
        /// </summary>
        public static BLEmployeeService Instance
        {
            get
            {
                if( _instance == null)
                {
                    _instance = new BLEmployeeService();
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public List<Employee> LstEmployees { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        ///     assign the list of product to static list
        /// </summary>
        public BLEmployeeService()
        {
            LstEmployees = new List<Employee>()
            {
                new Employee { Id = 1,FirstName="Dev",LastName="Nakum",Email="dev@gmail.com", Gender="Male"},
                new Employee { Id = 2,FirstName="Kishan",LastName="Nakum",Email="kishan@gmail.com", Gender="Male"},
                new Employee { Id = 3,FirstName="Raj",LastName="Mandaviya",Email="raj@gmail.com", Gender="Male"},
                new Employee { Id = 4,FirstName="Suman",LastName="Patel",Email="suman@gmail.com", Gender="Female"},
                new Employee { Id = 5,FirstName="Tushar",LastName="Gohil",Email="tushar@gmail.com", Gender="Male"},
            };
        }
        #endregion
    }
}