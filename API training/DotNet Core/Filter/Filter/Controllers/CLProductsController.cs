using Filter.Business_Logic;
using Filter.Filter;
using Filter.Model;
using Microsoft.AspNetCore.Mvc;

namespace Filter.Controllers
{
    [Route("api/products")]
    [ApiController]
    [ActionFilter("Controller")]
    [ResultFilter("Controller")]
    public class CLProductsController : ControllerBase
    {
        #region Private Member
        private BLProducts _objProducts;
        #endregion

        #region Constructor
        public CLProductsController()
        {
            _objProducts = new BLProducts();
        }
        #endregion

        #region Public Method
        [AuthorizationFilter]
        [HttpPost]
        public IActionResult AddProducts(Pro01 objPro01)
        {
            _objProducts.AddProduct(objPro01);
            return Ok("Products added successfully");
        }

        [AuthorizationFilter]
        [ActionFilter("Action")]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_objProducts.GetAllProducts());
        }

        [ResourceFilter("Action")]
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(_objProducts.GetProduct(id));
        }
        #endregion
    }
}
