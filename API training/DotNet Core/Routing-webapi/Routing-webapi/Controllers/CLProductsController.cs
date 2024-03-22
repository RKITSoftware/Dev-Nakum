using Microsoft.AspNetCore.Mvc;
using Routing_webapi.Business_Logic;
using Routing_webapi.Model;
using System.Dynamic;

namespace Routing_webapi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class CLProductsController : ControllerBase
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
        /// <summary>
        ///  Add product into list
        /// </summary>
        /// <param name="objPro01">object of the product</param>
        /// <returns>response message</returns>
        [HttpPost]
        public IActionResult AddProducts([FromBody] Pro01 objPro01)
        {
            bool products = _objBLProducts.AddProduct(objPro01);
            if(products)
            {
                return Ok("Product added successfully");
            }
            return BadRequest("Something went wrong");
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>list of all the products</returns>
        [HttpGet]
        public IActionResult GetALLProducts() { return Ok(_objBLProducts.GetAllProducts()); }

        /// <summary>
        /// get the products based on product id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>object of the product</returns>
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            Pro01 product = _objBLProducts.GetProductById(id);
            if (product == null)
            {
                return NotFound("Products is not found");
            }
            return Ok(product);
        }


        // Action Method
        /// <summary>
        /// Display the content
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/content")]
        public ContentResult ContentResult()
        {
            return Content("Hello from content result");
        }

        #endregion
    }
}
