using E_CommerceAPI.BL;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace E_CommerceAPI
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
            var dbFactory = new OrmLiteConnectionFactory(BLDbConnection.GetConnectionString(), MySqlDialect.Provider);
            OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
            BLDbConnection.Instance = dbFactory;
        }
    }
}
