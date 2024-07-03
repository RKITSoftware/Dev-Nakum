using SealedClassAPI.BL;
using SealedClassAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace SealedClassAPI.Controllers
{
    /// <summary>
    /// class which can handle all the operation related to children
    /// </summary>
    public class CLChildController : ApiController
    {
        #region Private Member
        /// <summary>
        /// create the object of the child services
        /// </summary>
        private readonly BLChild _objBLChild;
        #endregion


        #region Constrctor

        /// <summary>
        /// initialize the object of the child services
        /// </summary>
        public CLChildController()
        {
            _objBLChild = new BLChild();
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Get all the children
        /// </summary>
        /// <returns>list of the child</returns>
        [HttpGet]
        [Route("api/children")]
        public IHttpActionResult GetAllChild()
        {
            List<Child> lstChildren = _objBLChild.GetChildren();
            return Ok(lstChildren);
        }

        /// <summary>
        /// create and added child into the list
        /// </summary>
        /// <param name="objChild">object of the child</param>
        /// <returns>response messages</returns>
        [HttpPost]
        [Route("api/children")]
        public IHttpActionResult CreateChild(Child objChild)
        {
           _objBLChild.AddChild(objChild);
            return Ok("Child and Parent are added into the list");
        }

        #endregion
    }
}
