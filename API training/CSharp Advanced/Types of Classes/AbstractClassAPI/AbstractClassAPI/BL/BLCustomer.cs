using AbstractClassAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace AbstractClassAPI.BL
{
    /// <summary>
    /// manage the customer service
    /// </summary>
    public class BLCustomer
    {
        #region Private Member
        private static int _id = 1;
        #endregion


        /// <summary>
        /// Get customer by Id
        /// </summary>
        /// <param name="id">customer Id</param>
        /// <param name="lstCustomer">list of the customer</param>
        /// <returns>object of the customer</returns>
        public Customer GetCustomer(int id, List<Customer> lstCustomer)
        {
            Customer objCustomer = lstCustomer.FirstOrDefault(x => x.Id == id);
            return objCustomer;
        }

        /// <summary>
        /// Get all the customer
        /// </summary>
        /// <param name="lstCustomer">customer list</param>
        /// <returns>list of the customer</returns>
        public List<Customer> GetAll(List<Customer> lstCustomer)
        {
            return lstCustomer;
        }

        /// <summary>
        /// Add customer into list
        /// </summary>
        /// <param name="objCustomer">object of the customer</param>
        /// <param name="lstCustomer">list of the all customer</param>
        public void AddCustomer(Customer objCustomer, List<Customer> lstCustomer)
        {
            objCustomer.Id = _id++;
            lstCustomer.Add(objCustomer);
        }
    }
}