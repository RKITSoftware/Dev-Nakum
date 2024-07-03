using E_CommerceAPI.Models;
using Newtonsoft.Json.Linq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace E_CommerceAPI.BL
{
    public class BLProducts
    {
        #region Private Member
        private readonly IDbConnectionFactory _dbFactory;
        private BLCategories _objBLCategories;
        #endregion

        #region Constructor
        public BLProducts()
        {
            _dbFactory = BLDbConnection.Instance;
            _objBLCategories = new BLCategories();
        }
        #endregion

        #region Public Method
        public bool AddProduct(Pro01 objPro01)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    db.Insert<Pro01>(objPro01);
                    _objBLCategories.UpdateCategory(objPro01.O01F06,'I');
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<Pro01> GetAllProducts()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Select<Pro01>();
            }
        }

        public Pro01 GetProduct(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {

                return db.SingleById<Pro01>(id);
            }
        }

        public object GetProductWithCategoryName(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var query = db.From<Pro01>()
                                .Join<Cat01>((p, c) => p.O01F06 == c.T01F01)
                                .Where<Pro01>(x => x.O01F01 == id);

                var result = db.SelectMulti<Pro01, Cat01>(query)
                                .Select((s => new
                                {
                                    ProductId = s.Item1.O01F01,
                                    Name = s.Item1.O01F02,
                                    Description = s.Item1.O01F03,
                                    Price = s.Item1.O01F04,
                                    Stocks = s.Item1.O01F05,
                                    Category = s.Item2.T01F02
                                })).ToList();

                if (result.Count > 0)
                {
                    return result;
                }
                return null;
            }
        }

        public bool UpdateProduct(Pro01 objPro01)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                Pro01 products = GetProduct(objPro01.O01F01);
                if (products!=null)
                {
                    return db.Update<Pro01>(objPro01) > 0;
                }
                return false;
            }
        }


        public bool DeleteProduct(JObject productID)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                int id = Convert.ToInt32(productID["id"]);
                Pro01 products = GetProduct(id);
                if (products != null)
                {
                    bool isDeleted =  db.DeleteById<Pro01>(id) > 0;

                    if (isDeleted)
                    {
                        _objBLCategories.UpdateCategory(products.O01F06,'D');
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }


        public object GetProductCategoryWise(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var query = db.From<Pro01>()
                                .Join<Cat01>((p, c) => p.O01F06 == c.T01F01)
                                .Where<Pro01>(x => x.O01F06 == id);

                var result = db.SelectMulti<Pro01,Cat01>(query)
                                .Select((s=> new
                                {
                                    ProductId = s.Item1.O01F01,
                                    Name = s.Item1.O01F02,
                                    Description = s.Item1.O01F03,
                                    Price = s.Item1.O01F04,
                                    Stocks = s.Item1.O01F05,
                                    Category = s.Item2.T01F02
                                })).ToList();   

                if (result.Count > 0)
                {
                    return result;
                }
                return null;    
            }
        }

        #endregion
    }
}