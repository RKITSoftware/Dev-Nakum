using PartialClassAPI.BL;
using PartialClassAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace PartialClassAPI.Controllers
{
    /// <summary>
    /// partial class which handle all API related to customer
    /// </summary>
    public class CLCustomerController : ApiController
    {
        #region Private Member
        /// <summary>
        /// create the object of the customer services
        /// </summary>
        private readonly BLCustomer _objBLCustomer;
        #endregion

        #region Constructor
        /// <summary>
        /// initialize the list 
        /// </summary>
        public CLCustomerController()
        {
            _objBLCustomer = new BLCustomer();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Get the customer from customer id 
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>response message</returns>
        [HttpGet]
        [Route("api/customers/{id}")]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer objCustomer = _objBLCustomer.GetCustomerById(id);

            if (objCustomer == null)
            {
                return Ok("Customer is not found");
            }
            return Ok(objCustomer); 
        }

        /// <summary>
        /// Get all the customer which is listed 
        /// </summary>
        /// <returns>response message</returns>
        [HttpGet]
        [Route("api/customers")]
        public IHttpActionResult GetAllCustomer()
        {
            List<Customer> lstCustomer = _objBLCustomer.GetCustomers();

            if (lstCustomer.Count > 0)
            {
                return Ok(lstCustomer);
            }
            return Ok("Not found any customer");
        }

        /// <summary>
        /// Create customer
        /// Add customer object into list 
        /// </summary>
        /// <param name="objCustomer">customer object</param>
        /// <returns>response message</returns>
        [HttpPost]
        [Route("api/customers")]
        public IHttpActionResult CreateCustomer(Customer objCustomer)
        {
            _objBLCustomer.CreateCustomer(objCustomer);
            return Ok("Customer is added into list");
        }

        /// <summary>
        /// Delete the customer from list of customer 
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>Response message</returns>
        [HttpDelete]
        [Route("api/customers/{id}")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            bool isDeleted = _objBLCustomer.DeleteCustomer(id);
            if(isDeleted)
            {
                return Ok("Customer is deleted successfully");
            }
            return Ok("Customer is not found");
        }

        #endregion
    }
}
