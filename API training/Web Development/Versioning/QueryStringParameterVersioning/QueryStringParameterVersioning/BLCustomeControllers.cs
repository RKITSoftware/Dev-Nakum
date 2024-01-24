using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace QueryStringParameterVersioning
{
    /// <summary>
    /// to select custom controller based on query string parameter
    /// </summary>
    public class BLCustomControllers : DefaultHttpControllerSelector
    {
        #region Private Member
        private HttpConfiguration _config;
        #endregion

        #region Controlller
        /// <summary>
        /// Initializes configuration
        /// </summary>
        /// <param name="config"></param>
        public BLCustomControllers(HttpConfiguration config) : base(config)
        {
            _config = config;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// select the specified controller
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // get the web api controller from project
            IDictionary<string,HttpControllerDescriptor> controllers = GetControllerMapping();

            //get all the route data
            IHttpRouteData routeData = request.GetRouteData();

            //get the controller name 
            string controllerName = routeData.Values["controller"].ToString();

            //default version number is 1
            string versionNumber = "1";

            //get the query string from request
            NameValueCollection versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);

            //if query string is exist assign the version number 
            if (versionQueryString["v"]!=null)
            {
                versionNumber = versionQueryString["v"];
            }

            // append the version name in controller name 
            if(versionNumber == "1")
            {
                controllerName = controllerName + "V1";
            }
            else
            {
                controllerName = controllerName + "V2";
            }

            // find the specific controller name and return the appropriate controller as an output
            if(controllers.TryGetValue(controllerName, out HttpControllerDescriptor controllerDescriptor)) 
            { 
                return controllerDescriptor; 
            }
            return null;
        }
        #endregion
    }
}