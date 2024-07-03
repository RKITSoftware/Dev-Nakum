using E_CommerceAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace E_CommerceAPI.BL
{
    public class BLCarts
    {
        #region Private Member
        private readonly IDbConnectionFactory _dbFactory;
        #endregion

        #region Contructor
        public BLCarts()
        {
            _dbFactory = BLDbConnection.Instance;
        }
        #endregion

        #region Private Method
        private int GetLastOrderIdOfUser(string userId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var result = db.Select<Car01>().LastOrDefault(c => c.R01F02 == userId);

                if(result == null)
                    return -1;
                return result.R01F01;
            }
        }

        private bool RemoveCartOrder(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.DeleteById<Car01>(id) > 0;
            }
        }
        #endregion

        #region Public Method
        public bool AddProductIntoCart(Car02 objCat02)
        {
            using(IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Insert<Car02>(objCat02) > 0;
            }
        }

        
        public object AddToCart(List<Car02> lstCar02, string userId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                Car01 objCar01 = new Car01();
                objCar01.R01F02 = userId;
                db.Insert(objCar01);

                int cartId = GetLastOrderIdOfUser(userId);
                int totalPrice = 0;

                if (cartId == -1)
                {
                    return null;
                }

                foreach (var item in lstCar02)
                {
                    item.R02F02 = cartId;

                    BLProducts objBLProducts = new BLProducts();

                    // find the products price
                    Pro01 objPro01 = objBLProducts.GetProduct(item.R02F03);
                    if (objPro01 == null)
                    {
                        return null;
                    }

                    int productPrice = objPro01.O01F04;

                    // calculate the total price
                    totalPrice += productPrice * item.R02F04;

                    bool cartItems = AddProductIntoCart(item);
                    if (cartItems == false)
                    {
                        RemoveCartOrder(cartId);
                        return null;
                    }
                }

                object result = new
                {
                    CartId = cartId,
                    TotalPrice = totalPrice
                };
                return result;
            }
        }

        public Ord01 GetOrderDetails(int cartId)
        {
            using(IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Select<Ord01>().FirstOrDefault<Ord01>(o=>o.D01F02 ==  cartId);
            }
        }


        public int PlaceOrder (Ord01 objOrd01)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                bool result = db.Insert<Ord01>(objOrd01) > 0;

                if (result)
                {
                    return GetOrderDetails(objOrd01.D01F02).D01F01;
                }
                return -1;
            }
        }
        #endregion
    }
}