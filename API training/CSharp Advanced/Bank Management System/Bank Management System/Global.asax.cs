using Bank_Management_System.Business_Logic;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Bank_Management_System
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ConfigureORMLite();
        }

        private void ConfigureORMLite()
        {
            BLDbConnection objBLDbConnection = new BLDbConnection();
            var dbFactory = new OrmLiteConnectionFactory(objBLDbConnection.GetConnectionString(), MySqlDialect.Provider);
            OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
            BLDbConnection.Instance = dbFactory;
        }
    }
}
