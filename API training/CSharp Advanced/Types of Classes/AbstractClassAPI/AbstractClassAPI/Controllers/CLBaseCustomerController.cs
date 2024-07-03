using AbstractClassAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbstractClassAPI.Controllers
{
    /// <summary>
    /// Abstract controller which can contains all the declaration of method
    /// </summary>
    public abstract class CLBaseCustomerController : ApiController
    {
        #region Protected Member
        /// <summary>
        /// static list of customer
        /// </summary>
        protected static List<Customer> lstCustomer = new List<Customer>();
        #endregion

        #region Public Abstract Method

        /// <summary>
        /// Get the customer from customer id 
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>response message</returns>
        public abstract HttpResponseMessage GetCustomer(int customerId);

        /// <summary>
        /// Get all the customer which is listed 
        /// </summary>
        /// <returns>response message</returns>
        public abstract HttpResponseMessage GetAllCustomer();

        /// <summary>
        /// Create customer
        /// Add customer object into list 
        /// </summary>
        /// <param name="objCustomer">customer object</param>
        /// <returns>response message</returns>
        public abstract HttpResponseMessage CreateCustomer(Customer customer);
        #endregion

        /// <summary>
        /// Delete the customer from list of customer 
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>Response message</returns>
        [HttpDelete]
        [Route("api/customers/{id}")]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            Customer objCustomer = lstCustomer.FirstOrDefault(c => c.Id == id);
            if(objCustomer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,$"Customer id {id} is not found");
            }

            lstCustomer.Remove(objCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, $"Successfully deleted the customer with customer id {id}");
        }

    }
}
