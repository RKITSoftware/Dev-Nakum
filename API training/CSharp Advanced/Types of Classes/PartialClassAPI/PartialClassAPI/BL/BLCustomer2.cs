using PartialClassAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PartialClassAPI.BL
{
    /// <summary>
    /// partial class that manage the customer services
    /// </summary>
    public partial class BLCustomer
    {
        #region Public Method

        /// <summary>
        /// get the customer based on customer id
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>object of the customer</returns>
        public Customer GetCustomerById(int id)
        {
            Customer objCustomer = _lstCustomer.FirstOrDefault(x => x.Id == id);
            return objCustomer;
        }


        /// <summary>
        /// get the all customer list
        /// </summary>
        /// <returns>list of the all customer</returns>
        public List<Customer> GetCustomers()
        {
            return _lstCustomer;
        }

        /// <summary>
        /// create the customer
        /// </summary>
        /// <param name="objCustomer">object of the customer</param>
        public void CreateCustomer(Customer objCustomer)
        {
            objCustomer.Id = _id++;
            _lstCustomer.Add(objCustomer);

        }

        /// <summary>
        /// delete the customer
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>true if customer is deleted successfully or else false</returns>
        public bool DeleteCustomer(int id)
        {
            Customer oBjCustomer = GetCustomerById(id);
            if(oBjCustomer != null)
            {
                _lstCustomer.Remove(oBjCustomer);
                return true;
            }
            return false;
        }
        #endregion
    }
}