using E_CommerceAPI.BL;
using E_CommerceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace E_CommerceAPI.Controllers
{
    public class CLCategoriesController : ApiController
    {
        #region Private Member
        private BLCategories _objBLCategories;
        #endregion

        #region Constructor
        public CLCategoriesController()
        {
            _objBLCategories = new BLCategories();
        }
        #endregion


        #region Public Method
      
        [HttpPost]
        [Route("api/categories")]
        public IHttpActionResult AddCategory(Cat01 objCat01)
        {
            bool category = _objBLCategories.AddCategory(objCat01);
            if(category)
            {
                return Ok("Category added successfully");    
            }
            return BadRequest("something went wrong");
        }


        [HttpGet]
        [Route("api/categories")]
        public IHttpActionResult GetAllCategories()
        {
            return Ok(_objBLCategories.GetAllCategories());
        }
        #endregion
    }
}
