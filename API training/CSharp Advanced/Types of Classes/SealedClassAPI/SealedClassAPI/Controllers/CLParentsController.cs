using SealedClassAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace SealedClassAPI.Controllers
{
    /// <summary>
    /// class which can handle all the operation related to parents
    /// </summary>
    public sealed class CLParentsController : ApiController
    {
        #region Public Member
        public static int parentId = 1;
        public static List<Parents> lstParents = new List<Parents>();
        #endregion

        /// <summary>
        /// Get all the parents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/parents")]
        public IHttpActionResult GetAllParents()
        {
            return Ok(lstParents);
        }

        /// <summary>
        /// Create parents
        /// </summary>
        /// <param name="parents">Parents object</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/parents")]
        public  IHttpActionResult CreateParents(Parents parents)
        {
            Parents ObjParents = new Parents(); 
            ObjParents.Id = parentId++;
            ObjParents.Name = parents.Name;
            ObjParents.Age = parents.Age;

            lstParents.Add(ObjParents);
            return Ok(ObjParents);
        }
    }
}
