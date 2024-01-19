using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Exception
{
    /// <summary>
    /// Handle all the type of custom exception
    /// </summary>
    public class BLCustomExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// when an exception will occurred find the type and indicate the error 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            string errorMsg = "";

            // get the exception type
            Type exceptionType = actionExecutedContext.Exception.GetType();
            if(exceptionType==typeof(UnauthorizedAccessException))
            {
                errorMsg = "unauthorized Access !!";
                httpStatusCode = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NullReferenceException))
            {
                errorMsg = "Data is not found !!";
                httpStatusCode = HttpStatusCode.NotFound;
            }
            else    
            {
                errorMsg = "Something went wrong";
                httpStatusCode = HttpStatusCode.InternalServerError;
            }

            // generate a response message
            HttpResponseMessage response = new HttpResponseMessage(httpStatusCode) 
            { 
                Content = new StringContent(errorMsg)
            };

            actionExecutedContext.Response = response;  

            base.OnException(actionExecutedContext);
        }
    }
}