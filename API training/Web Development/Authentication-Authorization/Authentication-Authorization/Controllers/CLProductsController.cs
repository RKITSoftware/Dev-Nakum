using Authentication_Authorization.Basic_Auth;
using Authentication_Authorization.BL;
using Authentication_Authorization.Models;
using System.Net;
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
        /// <summary>
        /// Create the object of product services
        /// </summary>
        private readonly BLProducts _objBLProducts;
        #endregion

        #region Constructor 
        /// <summary>
        ///  to create instance of product services 
        /// </summary>
        public CLProductsController()
        {
            _objBLProducts = new BLProducts();
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
        public IHttpActionResult GetAllProducts()
        {
           return Ok(_objBLProducts.GetAllProducts());
        }
        #endregion

        #region Post - Create the products 
        /// <summary>
        /// create the products 
        /// </summary>
        /// <param name="objProduct">object of the products</param>
        /// <returns>Response messages</returns>
        [HttpPost]
        [Route("api/products")]
        [Authorization(Roles = "admin")]
        public IHttpActionResult CreateProduct(Products objProduct)
        {
            _objBLProducts.AddProduct(objProduct);
            return Ok("Product added successfully");
        }
        #endregion

        #region Put - Update Product
        /// <summary>
        /// update the products based on product id
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="objProduct">updated product</param>
        /// <returns>response message</returns>
        [HttpPut]
        [Route("api/products/{id}")]
        [Authorization(Roles = "admin")]
        public IHttpActionResult UpdateProduct(int id, Products objProduct)
        {
            bool isUpdate = _objBLProducts.UpdateProduct(id, objProduct);
            if (isUpdate)
            {
                return Ok("Product updated successfully");
            }
            return Ok("Product id is not found");
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

        public IHttpActionResult RemoveProduct(int id)
        {
            bool isDelete = _objBLProducts.RemoveProduct(id);
            if (isDelete)
            {
                return Ok("Product deleted successfully");
            }
            return Ok("Product id is not found");
        }
        #endregion
    }
}
