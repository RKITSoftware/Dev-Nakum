using SealedClassAPI.Models;
using System.Collections.Generic;

namespace SealedClassAPI.BL
{
    /// <summary>
    /// manage the parents services
    /// </summary>
    public sealed class BLParents
    {
        #region Public Member

        /// <summary>
        /// parent Id
        /// </summary>
        public static int parentId = 1;

        /// <summary>
        /// Parent list 
        /// </summary>
        public static List<Parents> lstParents = new List<Parents>();
        #endregion


        #region Public Method

        /// <summary>
        /// Get all list of the parents
        /// </summary>
        /// <returns>list of the parent</returns>
        public List<Parents> GetParents()
        {
            return lstParents;
        }

        /// <summary>
        /// Create the parent and added into list
        /// </summary>
        /// <param name="objParents">object of the parent</param>
        public void CreateParents(Parents objParents)
        {
            // if not set from the child
            if (objParents.Id == 0)
            {
                objParents.Id = parentId++;
            }
            lstParents.Add(objParents);
        }
        #endregion
    }
}