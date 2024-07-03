using E_CommerceAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace E_CommerceAPI.BL
{
    public class BLCategories
    {
        #region Private Member
        private readonly IDbConnectionFactory _dbFactory;
        #endregion


        #region Constructor
        public BLCategories()
        {
            _dbFactory = BLDbConnection.Instance;
        }
        #endregion

        #region Public Method

        public bool AddCategory(Cat01 objCat01)
        {
            using(IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    db.Insert<Cat01>(objCat01); 
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public Cat01 GetCategory(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<Cat01>(id);
            }
        }

        public bool UpdateCategory(int id,char ch)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                Cat01 objCat01 = GetCategory(id);
                if(objCat01 != null)
                {
                    if (ch == 'I')
                    {
                        objCat01.T01F03 += 1;
                    }
                    else if (ch == 'D')
                    {
                        objCat01.T01F03 -= 1;
                    }

                    db.Update<Cat01>(objCat01);
                    return true;
                }
                return false;
            }
        }

        public List<Cat01> GetAllCategories()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Select<Cat01>();
            }
        }



        #endregion
    }
}