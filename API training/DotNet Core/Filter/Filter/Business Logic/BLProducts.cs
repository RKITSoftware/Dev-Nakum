using Filter.Model;

namespace Filter.Business_Logic
{
    /// <summary>
    /// manage the product services
    /// </summary>
    public class BLProducts
    {
        #region Private Member

        /// <summary>
        /// product id
        /// </summary>
        private static int _id = 1;

        /// <summary>
        /// manage the all products
        /// </summary>
        private static List<Pro01> _lstPro01 = new List<Pro01>();
        #endregion

        #region Public Method

        /// <summary>
        /// Add products into list
        /// </summary>
        /// <param name="objPro01">object of the product</param>
        public void AddProduct(Pro01 objPro01)
        {
            objPro01.O01F01 = _id++;
            _lstPro01.Add(objPro01);
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
        /// get product details based on product id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>product details</returns>
        public Pro01 GetProduct(int id)
        {
            return _lstPro01.FirstOrDefault(p => p.O01F01 == id);
        }


        #endregion
    }
}
