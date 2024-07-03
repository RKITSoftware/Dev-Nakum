using Middleware___webapi.Business_Logic;
using System.Text;

namespace Middleware___webapi.Middleware
{
    /// <summary>
    /// Custom middleware that is handle the authentication
    /// </summary>
    public class CustomMiddleware
    {
        #region Private Member
        private readonly RequestDelegate _next;
        private readonly string _authorizationHeaderName;
        #endregion

        #region Constructor
        public CustomMiddleware(RequestDelegate next,string authorizationHeaderName)
        {
            _next = next;
            _authorizationHeaderName = authorizationHeaderName;   
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Call the middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            // to check particular request 
            if (context.Request.Path.StartsWithSegments("/api/users/validate") )
            {
                // to check authorization header is exist or not
                if (!context.Request.Headers.ContainsKey(_authorizationHeaderName))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Authorization Required");
                    return;
                }

                // Extract credentials from header (assuming Basic authentication)
                var authorizationHeader = context.Request.Headers[_authorizationHeaderName].FirstOrDefault();
                if (string.IsNullOrEmpty(authorizationHeader))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid authorization header");
                    return;
                }

                // get the decrypted username and password
                var decrypt = authorizationHeader.Split(' ')[1];
                var user = Encoding.UTF8.GetString(Convert.FromBase64String(decrypt)).Split(':');

                // get original username and password
                string username = user[0];
                string password = user[1];

                // Validate credentials 
                BLUsers objBLUsers = new BLUsers();
                var authenticationService = context.RequestServices.GetService<BLUsers>();
                if (!objBLUsers.ValidateCredentials(username, password))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid username or password");
                    return;
                }

                // Authentication successful, proceed with request
                _next(context);

            }
            else
            {
                // for another request
                await _next(context);
            }
            // display message
            Console.WriteLine("outgoing response");
        }
        #endregion
    }

    
}
