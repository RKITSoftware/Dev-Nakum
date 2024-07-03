using E_CommerceAPI.Attributes;
using E_CommerceAPI.BL;
using E_CommerceAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace E_CommerceAPI.Controllers
{
    public class CLProductsController : ApiController
    {
        #region Private Member 
        BLProducts _objBLProducts;
        #endregion

        #region Constructor
        public CLProductsController()
        {
            _objBLProducts = new BLProducts();
        }
        #endregion

        #region Public Method

        [JwtAuthorization]
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("api/products")]
        public IHttpActionResult AddProduct([FromBody] Pro01 objPro01)
        {
            bool product = _objBLProducts.AddProduct(objPro01);
            if(!product)
            {
                return BadRequest("Something went wrong");
            }
            return Ok("Product added successfully");
        }


        [JwtAuthorization]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("api/products")]
        public IHttpActionResult UpdateProduct([FromBody] Pro01 objPro01)
        {
            bool product = _objBLProducts.UpdateProduct(objPro01);
            if (!product)
            {
                return BadRequest("Something went wrong");
            }
            return Ok("Product updated successfully");
        }



        [JwtAuthorization]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/products")]
        public IHttpActionResult DeleteProduct([FromBody] JObject productID)
        {
            bool product = _objBLProducts.DeleteProduct(productID);
            if (!product)
            {
                return BadRequest("Something went wrong");
            }
            return Ok("Product deleted successfully");
        }


        [HttpGet]
        [Route("api/products")]
        public IHttpActionResult GetAllProducts()
        {
            return Ok(_objBLProducts.GetAllProducts());
        }



        [HttpGet]
        [Route("api/products/{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            object product = _objBLProducts.GetProductWithCategoryName(id);

            if(product == null)
            {
                return BadRequest("Product is not found");
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("api/products/filter/{id}")]
        public IHttpActionResult GetProductCategoryWise(int id)
        {
            object product = _objBLProducts.GetProductCategoryWise(id);

            if (product == null)
            {
                return BadRequest("Products is not found");
            }
            return Ok(product);
        }
        #endregion
    }
}
