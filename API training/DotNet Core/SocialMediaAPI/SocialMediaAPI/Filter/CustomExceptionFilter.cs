using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using SocialMediaAPI.Model;
using System.Net;

namespace SocialMediaAPI.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.Error(context.Exception, "An unhandled exception occurred.");

            // Create the standardized response model
            Response objResponse = new Response
            {
                IsError = true,
                Message = "An error occurred while processing your request.",
                Data = null
            };

            // Customize the response
            context.Result = new ObjectResult(objResponse)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            // Set the exception as handled
            context.ExceptionHandled = true;
        }
    }

}
