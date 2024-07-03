using SealedClassAPI.Models;
using System.Collections.Generic;

namespace SealedClassAPI.BL
{
    /// <summary>
    /// manage the child services
    /// </summary>
    public class BLChild
    {
        #region Private Member

        /// <summary>
        /// child id
        /// </summary>
        private static int _id = 1;

        /// <summary>
        /// list of the children
        /// </summary>
        private static List<Child> _lstChilds = new List<Child>();
        #endregion


        #region Public Method

        /// <summary>
        /// Get all list of the children
        /// </summary>
        /// <returns>list of the children</returns>
        public List<Child> GetChildren()
        {
            return _lstChilds;
        }

        /// <summary>
        /// Add child into the list and added parent object into parent list
        /// </summary>
        /// <param name="objChild">object of the child</param>
        public void AddChild(Child objChild)
        {
            objChild.Id = _id++;
            objChild.Parent.Id = BLParents.parentId++;
            _lstChilds.Add(objChild);

            BLParents objBLParents = new BLParents();
            objBLParents.CreateParents(objChild.Parent);
        }
        #endregion
    }
}