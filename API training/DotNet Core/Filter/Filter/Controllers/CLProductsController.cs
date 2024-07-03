using Filter.Business_Logic;
using Filter.Filter;
using Filter.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Filter.Controllers
{
    /// <summary>
    /// Manage the products api
    /// </summary>
    [Route("api/products")]
    [ApiController]
    [ActionAsyncFilter("Controller products")]
    [ResultFilter("Controller products")]
    public class CLProductsController : ControllerBase
    {
        #region Private Member

        /// <summary>
        /// create the product services
        /// </summary>
        private readonly BLProducts _objProducts;
        private readonly IMemoryCache _cache;
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the product services
        /// </summary>
        public CLProductsController(IMemoryCache cache)
        {
            _objProducts = new BLProducts();
            _cache = cache;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Add Products into list
        /// </summary>
        /// <param name="objPro01">object of the product</param>
        /// <returns>response message</returns>
        [Authorize(Roles ="A")]
        //[AuthorizationFilter]
        [HttpPost]
        public IActionResult AddProducts(Pro01 objPro01)
        {
            _objProducts.AddProduct(objPro01);
            return Ok("Products added successfully");
        }


        /// <summary>
        /// display all the products
        /// </summary>
        /// <returns>list of all the products</returns>
        [Authorize]
        //[AuthorizationFilter]
        [ActionAsyncFilter("Action GetAllProducts")]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_objProducts.GetAllProducts());
        }


        /// <summary>
        /// Get products by id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>product details</returns>
        //[ResourceAsyncFilter]
        [ServiceFilter(typeof(ResourceAsyncFilterAttribute))]
        [HttpGet("{id}")]   
        public IActionResult GetProduct(int id) 
        {
            return Ok(_objProducts.GetProduct(id));
        }
        #endregion
    }
}
