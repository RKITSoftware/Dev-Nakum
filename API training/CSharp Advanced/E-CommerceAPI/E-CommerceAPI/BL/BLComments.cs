using E_CommerceAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;

namespace E_CommerceAPI.BL
{
    public class BLComments
    {
        #region Private Member 
        private readonly IDbConnectionFactory _dbFactory;
        #endregion

        #region Constructor
        public BLComments()
        {
            _dbFactory = BLDbConnection.Instance;
        }
        #endregion

        #region Public Method

        public bool AddComments(string userId, Com01 objCom01)
        {
            using(IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    Com01 comments = objCom01;
                    comments.M01F03 = userId;

                    db.Insert<Com01>(comments);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public object GetAllCommentsByProductId(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var query = db.From<Com01>()
                            .Join<Use01>((c, u) => c.M01F03 == u.E01F01)
                            .Join<Pro01>((c,p) => c.M01F02 == p.O01F01)
                            .Where<Com01>(x=>x.M01F02 == id);


                var result = db.SelectMulti<Com01, Use01,Pro01>(query)
                                .Select(s => new {
                                    ProductId = s.Item1.M01F02,
                                    ProductName = s.Item3.O01F02,
                                    Username = s.Item2.E01F02,
                                    Comment = s.Item1.M01F04
                                }).ToList();
                if(result.Count > 0)
                {
                    return result;
                }
                return null;
            }
        }

        public object GetAllComments()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var query = db.From<Com01>()
                            .Join<Use01>((r, u) => r.M01F03 == u.E01F01)
                            .Join<Pro01>((r, p) => r.M01F02 == p.O01F01);


                var result = db.SelectMulti<Com01, Use01, Pro01>(query)
                                .Select((s => new
                                {
                                    CommentId = s.Item1.M01F01,
                                    ProductId = s.Item1.M01F02,
                                    ProductName = s.Item3.O01F02,
                                    Username = s.Item2.E01F02,
                                    Comment = s.Item1.M01F04,

                                })).ToList();

                if (result.Count > 0)
                {
                    return result;
                }
                return null;
            }
        }

        public bool DeleteComment(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.DeleteById<Com01>(id) > 0;
            }
        }

        public object GetCommentsByUser(string userId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var query = db.From<Com01>()
                                .Join<Pro01>((r, p) => r.M01F02 == p.O01F01)
                                .Join<Use01>((r, u) => r.M01F03 == u.E01F01)
                                .Where<Com01>(x => x.M01F03 == userId);


                var result = db.SelectMulti<Com01, Pro01, Use01>(query)
                                .Select((s => new
                                {
                                    Username = s.Item3.E01F02,
                                    ProductName = s.Item2.O01F02,
                                    ProductDescription = s.Item2.O01F03,
                                    Comment = s.Item1.M01F04
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