using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace CustomHeaderVersioning.BL
{
    /// <summary>
    /// to validate and call the appropriate controller based on custom header
    /// </summary>
    public class BLCustomHeader : DefaultHttpControllerSelector
    {
        #region Private Member
        private readonly HttpConfiguration _config;
        #endregion

        #region Controlller
        /// <summary>
        /// Initializes configuration
        /// </summary>
        /// <param name="config"></param>
        public BLCustomHeader(HttpConfiguration config) : base(config)
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
            IDictionary<string, HttpControllerDescriptor> controllers = GetControllerMapping();

            //get all the route data
            IHttpRouteData routeData = request.GetRouteData();

            //get the controller name 
            string controllerName = routeData.Values["controller"].ToString();

            //default version number is 1
            string versionNumber = "1";

            // get the custom header
            string customHeader = "X-StudentService-Version";

            //check custom header is exist or not 
            if (request.Headers.Contains(customHeader))
            {
                // get hte value of version number using first or default
                versionNumber = request.Headers.GetValues(customHeader).FirstOrDefault();

                // if there is multiple headers separates using ',' and use only first header
                if (versionNumber.Contains(","))
                {
                    versionNumber = versionNumber.Substring(0, versionNumber.IndexOf(","));
                }
            }

            // append the version name in controller name 
            if (versionNumber == "1")
            {
                controllerName += "V1";
            }
            else
            {
                controllerName += "V2";
            }

            // find the specific controller name and return the appropriate controller as an output
            if (controllers.TryGetValue(controllerName, out HttpControllerDescriptor controllerDescriptor))
            {
                return controllerDescriptor;
            }
            return null;
        }
        #endregion
    }
}