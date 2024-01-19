using Authentication_Authorization.Basic_Auth;
using Authentication_Authorization.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Authentication_Authorization.Controllers
{
    /// <summary>
    /// Manage the all products
    /// based on roles - executes the request
    /// </summary>
    [Authentication]
    public class CLProductsController : ApiController
    {
        #region Private Member
        private BLProductServices objBLProductServices;
        #endregion

        #region Constructor 
        /// <summary>
        ///  to create instance od product services 
        /// </summary>
        public CLProductsController()
        {
            objBLProductServices = BLProductServices.Instance;
        }
        #endregion

        #region Get - Products
        /// <summary>
        /// user and admin can see all list of products 
        /// </summary>
        /// <returns>list of products</returns>
        [HttpGet]
        [Route("api/products")]
        [Authorization(Roles = "user,admin")]
        public HttpResponseMessage GetAllProducts()
        {
            return Request.CreateResponse(HttpStatusCode.OK, objBLProductServices.LstProducts);
        }
        #endregion

        #region Post - Craete the products 
        /// <summary>
        /// create the products 
        /// </summary>
        /// <param name="product"></param>
        /// <returns>created products</returns>
        [HttpPost]
        [Route("api/products")]
        [Authorization(Roles = "admin")]
        public HttpResponseMessage CreateProduct(Products product)
        {
            objBLProductServices.LstProducts.Add(product);
            return Request.CreateResponse(HttpStatusCode.Created, product);
        }
        #endregion

        #region Put - Update Product
        /// <summary>
        /// update the products based on product id
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="product">updated product</param>
        /// <returns>successful updated product message</returns>
        [HttpPut]
        [Route("api/products/{id}")]
        [Authorization(Roles = "admin")]
        public HttpResponseMessage UpdateProduct(int id, Products product)
        {
            // get the product from product id 
            Products pr = objBLProductServices.LstProducts.FirstOrDefault(p=>p.Id==id);
            if (pr == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Product {id} not found");
            }

            // update the product
            pr.Price = product.Price;
            pr.Name = product.Name;
            pr.Description = product.Description;
            
            return Request.CreateResponse(HttpStatusCode.OK, "Update successfully");
        }
        #endregion

        #region Delete - Delete Product
        /// <summary>
        /// Delete the product based on product id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>delete product message</returns>
        [HttpDelete]
        [Route("api/products/{id}")]
        [Authorization(Roles = "admin")]

        public HttpResponseMessage DeleteProduct(int id)
        {
            // get the product from product id 
            Products pr = objBLProductServices.LstProducts.FirstOrDefault(p => p.Id == id);
            if (pr == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Product {id} not found");
            }
            //delete the product
            objBLProductServices.LstProducts.Remove(pr);

            return Request.CreateResponse(HttpStatusCode.OK, "Deleted Successfully");
        }
        #endregion
    }
}
