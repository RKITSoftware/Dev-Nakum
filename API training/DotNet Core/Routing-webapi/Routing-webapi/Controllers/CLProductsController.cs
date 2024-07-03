using Microsoft.AspNetCore.Mvc;
using Routing_webapi.Business_Logic;
using Routing_webapi.Model;

namespace Routing_webapi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class CLProductsController : ControllerBase
    {
        #region Private Member

        /// <summary>
        /// Create the object of product services
        /// </summary>
        private readonly BLProducts _objBLProducts;
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the object of the product services
        /// </summary>
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
            _objBLProducts.AddProduct(objPro01);
            return Ok("Product added successfully");
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>list of all the products</returns>
        [HttpGet]
        public IActionResult GetALLProducts()
        {
            return Ok(_objBLProducts.GetAllProducts());
        }

        /// <summary>
        /// get the products based on product id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>object of the product</returns>
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            JsonResult product = _objBLProducts.GetProductById(id);
            if (product.Value == null)
            {
                return NotFound("Products is not found");
            }
            return Ok(product.Value);
        }


        // Action Method

        /// <summary>
        /// Display the content
        /// </summary>
        /// <returns></returns>
        [HttpGet("content")]
        public ContentResult ContentResult()
        {
            return Content("Hello from content result");
        }

        #endregion
    }
}
