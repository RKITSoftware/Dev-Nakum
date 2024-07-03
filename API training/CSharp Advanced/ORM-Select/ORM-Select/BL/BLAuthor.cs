using ORM_Select.Models;
using ORM_Select.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Web;

namespace ORM_Select.BL
{
    public class BLAuthor
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
        public BLAuthor()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            if (_dbFactory == null)
            {
                throw new Exception("Database connection is not found");
            }

            objResponse = new Response();   
        } 

        #endregion


        public Response GetAuthorFilterBirth()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                int agesAgo = DateTime.Today.AddYears(-44).Year;
                var ans = db.Select<Aut01>(x=>x.T01F03 >= new DateTime(agesAgo,1,1) && x.T01F03 <= new DateTime(agesAgo,12,31));

                objResponse.Data = ans;
                return objResponse;
            }
        }
    
        public Response SqlIn()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var ans = db.Select<Aut01>(x => Sql.In(x.T01F04, "London", "Madrid", "Berlin"));
                objResponse.Data = ans;
                return objResponse;
            }
        }

        
    }
}