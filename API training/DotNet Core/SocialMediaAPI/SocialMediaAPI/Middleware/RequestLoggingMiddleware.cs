using NLog;

namespace SocialMediaAPI.Middleware
{
    /// <summary>
    /// RequestLoggingMiddleware class implements custom middleware for logging incoming requests and outgoing responses.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        #region Private Member
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly RequestDelegate _next;

        #endregion

        #region Constructor
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Invokes the middleware asynchronously.
        /// </summary>
        /// <param name="httpContext">The current HttpContext object representing the request.</param>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            // Log information about the incoming request
            _logger.Info($"Incoming request: {httpContext.Request.Method} {httpContext.Request.Path}");

            // Call the next middleware in the pipeline
            await _next(httpContext);

            // Log information about the outgoing response
            _logger.Info($"Outgoing response: {httpContext.Response.StatusCode}");
        }
        #endregion
    }
}
