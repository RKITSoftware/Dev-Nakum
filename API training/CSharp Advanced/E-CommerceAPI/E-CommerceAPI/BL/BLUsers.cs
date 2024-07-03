using E_CommerceAPI.Models;
using Newtonsoft.Json.Linq;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;
using System;
using System.Collections.Generic;
using System.Data;

namespace E_CommerceAPI.BL
{
    public class BLUsers
    {
        #region Private Member
        private readonly IDbConnectionFactory _dbFactory;
        private BLHashing _objBLHashing;
        #endregion

        #region Constructor
        public BLUsers()
        {
            _dbFactory = BLDbConnection.Instance;
            _objBLHashing = new BLHashing();
        }
        #endregion

        #region Public Method
        public Use01 SignUp(Use01 objUse01)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    objUse01.E01F01 = guid.ToString();  

                    objUse01.E01F04 = _objBLHashing.HashPassword(objUse01.E01F04);
                    db.Insert(objUse01);
                    return objUse01;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        public object Login(Use01 objUse01)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                Use01 user = db.SingleWhere<Use01>("E01F03", objUse01.E01F03);
                //List<Use01> user = db.Select<Use01>(objUse01.E01F03);
                if (user != null)
                {
                    if (_objBLHashing.Verify(objUse01.E01F04, user.E01F04))
                    {
                        BLAuth objBLAuth = new BLAuth();
                        string jwt = objBLAuth.GenerateJWT(user.E01F01, user.E01F02, user.E01F03, user.E01F05);
                        string role = user.E01F05;

                        return new {
                            jwt,
                            role
                        };
                    }
                    return null;
                }
                return null;

            }
        }


        public bool UpdateUserName(string id, Use01 objUse01)
        {
            using(IDbConnection db = _dbFactory.OpenDbConnection())
            {
                Use01 user = db.SingleById<Use01>(id);
                if (user != null)
                {
                    user.E01F02 = objUse01.E01F02;
                    return db.Update<Use01>(user) > 0;
                }
                return false;
            }
        }


        public string ChangePassword(string id, JObject objPassword)
        {
            using(IDbConnection db = _dbFactory.OpenDbConnection())
            {
                Use01 user = db.SingleById<Use01>(id);
                if (user!=null)
                {
                    string oldpaasword = objPassword["oldPassword"].ToString();
                    if (_objBLHashing.Verify(oldpaasword, user.E01F04))
                    {
                        user.E01F04 = _objBLHashing.HashPassword(objPassword["newPassword"].ToString());
                        bool update = db.Update<Use01>(user) > 0;
                        if(update)
                        {
                            return "password is changed successfully";
                        }
                        else
                        {
                            return "something went wrong";
                        }
                    }
                    else
                    {
                        return "old password does not match";
                    }
                }
                else
                {
                    return "User not found";
                }
            }
        }

        public bool DeleteUser(string id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.DeleteById<Use01>(id) > 0;
            }
        }

        public List<Use01> GetAllUsers()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Select<Use01>();
            }
        }


        public Use01 GetUsers(string id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<Use01>(id);
            }
        }
        #endregion
    }
}