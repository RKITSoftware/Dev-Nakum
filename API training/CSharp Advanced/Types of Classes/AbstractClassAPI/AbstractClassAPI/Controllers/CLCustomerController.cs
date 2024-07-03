using AbstractClassAPI.BL;
using AbstractClassAPI.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbstractClassAPI.Controllers
{
    public class CLCustomerController : CLBaseCustomerController
    {
        #region Private Member

        /// <summary>
        /// Create the object of the customer services
        /// </summary>
        private readonly BLCustomer _objBLCustomer;
        #endregion


        #region Constructor

        /// <summary>
        /// Initialize the object of the customer service
        /// </summary>
        public CLCustomerController()
        {
            _objBLCustomer = new BLCustomer();
        }
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
            Customer objCustomer = _objBLCustomer.GetCustomer(id, lstCustomer);
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
            return Request.CreateResponse(HttpStatusCode.OK, _objBLCustomer.GetAll(lstCustomer));
        }

        /// <summary>
        /// Create customer
        /// Add customer object into list 
        /// </summary>
        /// <param name="objCustomer">customer object</param>
        /// <returns>response message</returns>
        [HttpPost]
        [Route("api/customers")]
        public override HttpResponseMessage CreateCustomer(Customer objCustomer)
        {
            _objBLCustomer.AddCustomer(objCustomer, lstCustomer);
            return Request.CreateResponse(HttpStatusCode.OK,"Customer is added into list");
        }

        
    }
}