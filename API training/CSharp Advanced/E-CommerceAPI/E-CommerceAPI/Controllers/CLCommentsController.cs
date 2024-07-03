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
    public class CLCommentsController : ApiController
    {
        #region Private Member
        private BLComments _objBLComments;
        #endregion

        #region Constructor
        public CLCommentsController()
        {
            _objBLComments = new BLComments();
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
        [Route("api/comments")]
        public IHttpActionResult AddComments(Com01 objCom01)
        {
            string userId = GetCurrentUser();
            bool comments = _objBLComments.AddComments(userId, objCom01);
            if(comments)
            {
                return Ok("comments added successfully");
            }
            return BadRequest("something went wrong");
        }


        [JwtAuthorization]
        [HttpGet]
        [Route("api/comments/{id}")]
        public IHttpActionResult GetAllCommentsByProductId(int id)
        {
            object comments = _objBLComments.GetAllCommentsByProductId(id);
            if (comments != null)
            {
                return Ok(comments);
            }
            return BadRequest("Comments are not founds");
        }


        [JwtAuthorization]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/comments")]
        public IHttpActionResult GetAllComments()
        {
            object comments = _objBLComments.GetAllComments();

            if (comments != null)
            {
                return Ok(comments);
            }
            return BadRequest("Not found");
        }

        [JwtAuthorization]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/comments/{id}")]
        public IHttpActionResult DeleteComment(int id)
        {
            bool comments = _objBLComments.DeleteComment(id);

            if (comments)
            {
                return Ok("Comment deleted successfully");
            }
            return BadRequest("Not found");
        }


        [JwtAuthorization]
        [HttpGet]
        [Route("api/comments/me")]
        public IHttpActionResult GetCommentsByUser()
        {
            string userId = GetCurrentUser();
            object comments = _objBLComments.GetCommentsByUser(userId);
            if (comments == null)
            {
                return BadRequest("You have not done any comment");
            }
            return Ok(comments);
        }
        #endregion
    }
}
