using PartialClassAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace PartialClassAPI.Controllers
{
    /// <summary>
    /// partial class which handle all API related to customer
    /// </summary>
    public partial class CLCustomer1Controller : ApiController
    {
        /// <summary>
        /// Get the customer from customer id 
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>response message</returns>
        [HttpGet]
        [Route("api/customers/{id}")]
        public HttpResponseMessage GetCustomer(int id)
        {
            Customer objCustomer = lstCustomer.FirstOrDefault(x => x.Id == id);
            if (objCustomer == null)
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
        public HttpResponseMessage GetAllCustomer()
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
        public HttpResponseMessage CreateCustomer(Customer customer)
        {
            Customer objCustomer = new Customer();
            objCustomer.Id = _id++;
            objCustomer.Name = customer.Name;
            objCustomer.Age = customer.Age;
            objCustomer.City = customer.City;

            lstCustomer.Add(objCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objCustomer);
        }

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
            if (objCustomer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Customer id {id} is not found");
            }

            lstCustomer.Remove(objCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, $"Successfully deleted the customer with customer id  {id}");
        }
    }
}
