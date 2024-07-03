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
    public class CLReviewsController : ApiController
    {
        #region Private Member
        private BLReviews _objBLReviews;
        #endregion

        #region Constructor
        public CLReviewsController()
        {
            _objBLReviews = new BLReviews();
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

        #region Public Method
        [JwtAuthorization]
        [HttpPost]
        [Route("api/reviews")]
        public IHttpActionResult CreateReviews(Rev01 objRev01)
        {
            string userId = GetCurrentUser();
            bool review = _objBLReviews.CreateReviews(userId,objRev01);
            if (review)
            {
                return Ok("Review added successfully");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }


        [JwtAuthorization]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/reviews/{id}")]
        public IHttpActionResult GetReviewsByProductId(int id)
        {
            object reviews = _objBLReviews.GetReviewsByProductId(id);
            
            if(reviews == null)
            {
                return BadRequest("Not found");
            }
            return Ok(reviews);
        }

        [JwtAuthorization]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/reviews")]
        public IHttpActionResult GetAllReviews()
        {
            object reviews = _objBLReviews.GetAllReviews();

            if (reviews!=null)
            {
                return Ok(reviews);
            }
            return BadRequest("Not found");
        }

        [JwtAuthorization]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/reviews/{id}")]
        public IHttpActionResult DeleteReview(int id)
        {
            bool reviews = _objBLReviews.DeleteReview(id);

            if (reviews)
            {
                return Ok("Review deleted successfully");
            }
            return BadRequest("Not found");
        }


        [JwtAuthorization]
        [HttpGet]
        [Route("api/reviews/me")]
        public IHttpActionResult GetReviewsByUser()
        {
            string userId = GetCurrentUser();
            object reviews = _objBLReviews.GetReviewsByUser(userId);
            if(reviews == null)
            {
                return BadRequest("You have not done any review");
            }
            return Ok(reviews);
        }
        #endregion
    }
}
