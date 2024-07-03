using ORM_Select.Models;
using ORM_Select.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;

namespace ORM_Select.BL
{
    public class BLPerson
    {
        #region Private Member

        /// <summary>
        /// Retrieve IDbConnectionFactory from the application context
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;
        #endregion

        #region Public Member
        /// <summary>
        /// create the object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the connection string
        /// </summary>
        /// <exception cref="Exception"></exception>
        public BLPerson()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            if (_dbFactory == null)
            {
                throw new Exception("Database connection is not found");
            }

            objResponse = new Response();
        }

        #endregion

        //public Response PeopleOver40()
        //{
        //    using (IDbConnection db = _dbFactory.OpenDbConnection())
        //    {
        //        //var query = db.From >
        //    }
        //}

        public Response Has42YearOlds()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                bool isExist = db.Exists<Per01>(new { R01F03 = 42 });

                if (isExist)
                {
                    objResponse.Message = "Person is exist";
                }
                else
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Person is not found";
                }
                return objResponse;
            }
        }

        public Response GetALLPerson()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var ans = db.Select<Per01>();
                objResponse.Data = ans;
                return objResponse;
            }
        }

        public Response FilterOnAge()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var query = db.From<Per01>().Where(x => x.R01F03 < 50).Select(x => x.R01F02);

                // for distinct record
                var ans = db.ColumnDistinct<string>(query);

                // for all record
                //var ans = db.Column<string>(query);

                objResponse.Data = ans;
                return objResponse;
            }
        }

        public Response Count()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var q = db.From<Per01>()    
                            .GroupBy(x => x.R01F03)
                            .Select(x => new { x.R01F03, Count = Sql.Count("*") })
                            .OrderByDescending("Count");

                var results = db.KeyValuePairs<string, int>(q);

                objResponse.Data = results;
                return objResponse;
            }
        }
    }
}