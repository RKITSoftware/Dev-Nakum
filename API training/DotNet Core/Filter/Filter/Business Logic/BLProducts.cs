using Filter.Model;

namespace Filter.Business_Logic
{
    public class BLProducts
    {
        #region Private Member
        private static int _id = 1;
        private static List<Pro01> _lstPro01 = new List<Pro01>();
        #endregion

        #region Public Method
        public void AddProduct(Pro01 objPro01)
        {
            objPro01.O01F01 = _id++;
            _lstPro01.Add(objPro01);
        }

         public List<Pro01> GetAllProducts()
         {
            return _lstPro01;
         }

        public Pro01 GetProduct(int id)
        {
            return _lstPro01.FirstOrDefault(p => p.O01F01 == id);
        }

        
        #endregion
    }
}
