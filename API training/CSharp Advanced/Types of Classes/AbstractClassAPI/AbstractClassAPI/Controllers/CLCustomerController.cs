using AbstractClassAPI.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbstractClassAPI.Controllers
{
    public class CLCustomerController : CLBaseCustomerController
    {
        #region Private Member
        private static int _id = 1;
        #endregion
        /// <summary>
        /// Get the customer from customer id 
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>response message</returns>
        [HttpGet]
        [Route("api/customers/{id}")]
        public override HttpResponseMessage GetCustomer(int id)
        {
            Customer objCustomer = lstCustomer.FirstOrDefault(x => x.Id == id);
            if(objCustomer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Customer id {id} is not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, objCustomer);
        }

        /// <summary>
        /// Get all the customer which is listed 
        /// </summary>
        /// <returns>response message</returns>
        [HttpGet]
        [Route("api/customers")]
        public override HttpResponseMessage GetAllCustomer()
        {
            return Request.CreateResponse(HttpStatusCode.OK, lstCustomer);
        }

        /// <summary>
        /// Create customer
        /// Add customer object into list 
        /// </summary>
        /// <param name="objCustomer">customer object</param>
        /// <returns>response message</returns>
        [HttpPost]
        [Route("api/customers")]
        public override HttpResponseMessage CreateCustomer(Customer customer)
        {
            Customer objCustomer = new Customer();
            objCustomer.Id = _id++;
            objCustomer.Name = customer.Name;
            objCustomer.Age = customer.Age;
            objCustomer.City = customer.City;

            lstCustomer.Add(objCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objCustomer);
        }

        
    }
}