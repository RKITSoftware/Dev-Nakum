using SealedClassAPI.BL;
using SealedClassAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace SealedClassAPI.Controllers
{
    /// <summary>
    /// class which can handle all the operation related to parents
    /// </summary>
    public class CLParentsController : ApiController
    {
        #region Private Member

        /// <summary>
        /// create the object of the child services
        /// </summary>
        private readonly BLParents _objBLParents;
        #endregion


        #region Constrctor

        /// <summary>
        /// initialize the object of the child services
        /// </summary>
        public CLParentsController()
        {
            _objBLParents = new BLParents();
        }
        #endregion

        /// <summary>
        /// Get all the parents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/parents")]
        public IHttpActionResult GetAllParents()
        {
            List<Parents> lstParents = _objBLParents.GetParents();
            return Ok(lstParents);
        }

        /// <summary>
        /// Create parents
        /// </summary>
        /// <param name="objParents">Parents object</param>
        /// <returns>response message</returns>
        [HttpPost]
        [Route("api/parents")]
        public  IHttpActionResult CreateParents(Parents objParents)
        {
            _objBLParents.CreateParents(objParents);
            return Ok("parent is added into the list");
        }
    }
}
