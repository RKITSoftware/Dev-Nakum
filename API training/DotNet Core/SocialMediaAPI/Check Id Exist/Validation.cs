using Microsoft.Extensions.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;
using System.Linq.Expressions;

namespace Check_Id_Exist
{
    /// <summary>
    /// manage the validation based on Id to check whether id is exist or not in given model
    /// </summary>
    public class Validation
    {
        #region Private Member
        /// <summary>
        /// create the variable for connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// create the object of the connection factory for ORMLite
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// create the object of the configuration for gt the connection string
        /// </summary>
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public Validation(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }
        #endregion

        #region Public Method

        
        /// To check whether id is exist or not in given model
        /// </summary>
        /// <typeparam name="T">model type</typeparam>
        /// <param name="id">finding value</param>
        /// <param name="selector">expression ex. x => x.Id</param>
        /// <returns>true if value is exist or else false</returns>
        public bool IsExist<T>(int id, Expression<Func<T, int>> selector)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                // Modify the query to use the expression directly
                var predicate = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(selector.Body, Expression.Constant(id)),
                    selector.Parameters);

                return db.Exists(predicate);
            }
        }

        #endregion
    }
}