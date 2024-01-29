using SealedClassAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace SealedClassAPI.Controllers
{
    /// <summary>
    /// class which can handle all the operation related to children
    /// </summary>
    public class CLChildsController : ApiController
    {
        #region Private Member
        private static int _id = 1;
        private static List<Child> lstChilds = new List<Child>();
        #endregion

        /// <summary>
        /// Get all the parents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/children")]
        public IHttpActionResult GetAllChilds()
        {
            return Ok(lstChilds);
        }

        /// <summary>
        /// Create parents
        /// </summary>
        /// <param name="parents">Parents object</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/children")]
        public IHttpActionResult CreateChild(Child child)
        {
            Child ObjChild = new Child();
            ObjChild.Id = _id++;
            ObjChild.Name = child.Name;
            ObjChild.Age = child.Age;
            ObjChild.Parent = child.Parent;
            
            lstChilds.Add(ObjChild);

            Parents objParents = new Parents();
            objParents.Id = CLParentsController.parentId++;
            objParents.Name = child.Parent.Name;
            objParents.Age = child.Parent.Age;

            CLParentsController.lstParents.Add(objParents);
            return Ok(ObjChild);
        }
    }
}
