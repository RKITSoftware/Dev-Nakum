using Routing_webapi.Model;

namespace Routing_webapi.Business_Logic
{
    /// <summary>
    /// business logic of products
    /// </summary>
    public class BLProducts
    {
        #region Private Member
        private static int _id = 1;
        private static List<Pro01> _lstPro01 = new List<Pro01>();
        #endregion


        #region Public Method

        /// <summary>
        /// Add products into list
        /// </summary>
        /// <param name="objPro01"></param>
        /// <returns></returns>
        public bool AddProduct(Pro01 objPro01)
        {
            objPro01.O01F01 = _id++;
            _lstPro01.Add(objPro01);
            return true;
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
        public Pro01 GetProductById(int id)
        {
            return _lstPro01.FirstOrDefault(p => p.O01F01 == id);
        }
        #endregion
    }
}
