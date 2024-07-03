using E_CommerceAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace E_CommerceAPI.BL
{
    public class BLReviews
    {
        #region Private Member
        private readonly IDbConnectionFactory _dbFactory;
        #endregion

        #region Constructor
        public BLReviews()
        {
            _dbFactory = BLDbConnection.Instance;
        }
        #endregion

        #region Public Method
        
        public Rev01 GetReviews(string userId, int productId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Single<Rev01>(rev => rev.V01F03 == userId & rev.V01F02==productId);
            }
        }

        public bool CreateReviews(string userId,Rev01 objRev01)
        {
            using(IDbConnection db = _dbFactory.OpenDbConnection())
            {
                Rev01 review = GetReviews(userId, objRev01.V01F02);
                if (review != null)
                {
                    review.V01F04 = objRev01.V01F04;
                    return db.Update<Rev01>(review) > 0;
                }
                else
                {
                    try
                    {
                        Rev01 rev = objRev01;
                        rev.V01F03 = userId;

                        db.Insert<Rev01>(rev);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

            }
        }


        public object GetReviewsByProductId(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var query = db.From<Rev01>()
                            .Join<Use01>((r, u) => r.V01F03 == u.E01F01)
                            .Join<Pro01>((r, p) => r.V01F02 == p.O01F01)
                            .Where<Rev01>((x => x.V01F02 == id));


                var result = db.SelectMulti<Rev01, Use01, Pro01>(query)
                                .Select((s => new
                                {
                                    ProductId = s.Item1.V01F02,
                                    ProductName = s.Item3.O01F02,
                                    Username = s.Item2.E01F02,
                                    Review = s.Item1.V01F04 + " star"
                                })).ToList();

                if (result.Count > 0)
                {
                    return result;
                }
                return null;
            }
        }


        public bool DeleteReview(int id) 
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.DeleteById<Rev01>(id) > 0;
            }
        }


        public object GetReviewsByUser(string userId)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var query = db.From<Rev01>()
                                .Join<Pro01>((r, p) => r.V01F02 == p.O01F01)
                                .Join<Use01>((r, u) => r.V01F03 == u.E01F01)
                                .Where<Rev01>(x => x.V01F03 == userId);


                var result = db.SelectMulti<Rev01, Pro01, Use01>(query)
                                .Select((s => new
                                {
                                    Username = s.Item3.E01F02,
                                    ProductName = s.Item2.O01F02,
                                    ProductDescription = s.Item2.O01F03,
                                    Review = s.Item1.V01F04 + " star"
                                })).ToList();

                if (result.Count > 0)
                {
                    return result;
                }
                return null;
            }
        }
        
        public object GetAllReviews()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var query = db.From<Rev01>()
                            .Join<Use01>((r, u) => r.V01F03 == u.E01F01)
                            .Join<Pro01>((r, p) => r.V01F02 == p.O01F01);


                var result = db.SelectMulti<Rev01, Use01, Pro01>(query)
                                .Select((s => new
                                {
                                    ReviewId = s.Item1.V01F01,
                                    ProductId = s.Item1.V01F02,
                                    ProductName = s.Item3.O01F02,
                                    Username = s.Item2.E01F02,
                                    Review = s.Item1.V01F04 + " star",

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