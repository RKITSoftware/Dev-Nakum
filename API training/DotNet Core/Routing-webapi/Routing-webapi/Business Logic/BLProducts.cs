using Microsoft.AspNetCore.Mvc;
using Routing_webapi.Model;

namespace Routing_webapi.Business_Logic
{
    /// <summary>
    /// business logic of products
    /// </summary>
    public class BLProducts
    {
        #region Private Member
        /// <summary>
        /// Product's Id
        /// </summary>
        private static int _id = 1;

        /// <summary>
        /// manage the list of all the products
        /// </summary>
        private static List<Pro01> _lstPro01 = new List<Pro01>();
        #endregion


        #region Public Method

        /// <summary>
        /// Add products into list
        /// </summary>
        /// <param name="objPro01"></param>
        /// <returns></returns>
        public EmptyResult AddProduct(Pro01 objPro01)
        {
            objPro01.O01F01 = _id++;
            _lstPro01.Add(objPro01);
            return new EmptyResult();
        }

        /// <summary>
        /// get all the products
        /// </summary>
        /// <returns>list of all the products</returns>
        public List<Pro01> GetAllProducts()
        {
            return _lstPro01;
        }

        /// <summary>
        /// Get the products based on product id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>object of product</returns>
        public JsonResult GetProductById(int id)
        {
            Pro01 objPro01 = _lstPro01.FirstOrDefault(p => p.O01F01 == id);
            return new JsonResult(objPro01);
        }
        #endregion
    }
}
