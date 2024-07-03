using E_CommerceAPI.Attributes;
using E_CommerceAPI.BL;
using E_CommerceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace E_CommerceAPI.Controllers
{
    public class CLOrdersController : ApiController
    {
        #region Private Member
        private BLCarts _objBLCarts;
        #endregion

        #region Constructor
        public CLOrdersController()
        {
            _objBLCarts = new BLCarts();
        }
        #endregion

        #region Private Method
        private string GetCurrentUser()
        {
            ClaimsPrincipal currentUser = User as ClaimsPrincipal;
            if (currentUser != null)
            {
                string userId = currentUser.FindFirst("Id")?.Value;

                return userId;
            }
            return null;
        }
        #endregion

        #region Public Member

        [JwtAuthorization]
        [HttpPost]
        [Route("api/addtocart")]
        public IHttpActionResult AddToCart(List<Car02> lstCar02)
        {
            string userId = GetCurrentUser();
            object carts = _objBLCarts.AddToCart(lstCar02,userId);

            if(carts == null)
            {
                return BadRequest("something went wrong");
            }

            return Ok(carts);
        }


        [JwtAuthorization]
        [HttpPost]
        [Route("api/orders")]
        public IHttpActionResult PlaceOrder(Ord01 objOrd01)
        {
            int orderId = _objBLCarts.PlaceOrder(objOrd01);

            if (orderId == -1)
            {
                return BadRequest("something went wrong");
            }
            return Ok(orderId);
        }
        #endregion
    }
}
