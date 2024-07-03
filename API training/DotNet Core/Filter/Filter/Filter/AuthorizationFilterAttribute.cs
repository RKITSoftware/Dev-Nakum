using Filter.Business_Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Filter.Filter
{
    /// <summary>
    /// authorization filter
    /// </summary>
    public class AuthorizationFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// authorize the request
        /// </summary>
        /// <param name="context">authorization context</param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            
            //if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            //{
            //    context.Result = new UnauthorizedResult();
            //    return;
            //}
            //var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
            //if (authorizationHeader.FirstOrDefault()?.Split(' ').Length != 2 || authorizationHeader.FirstOrDefault()?.Split(' ')[0] != "Bearer")
            //{
            //    context.Result = new UnauthorizedResult();
            //    return;
            //}

            //string token = authorizationHeader.FirstOrDefault()?.Split(' ')[1];

            //BLAuth objBLAuth = new BLAuth();
            //Dictionary<string, string> user =  objBLAuth.VerifyToken(token);

            //if (user == null)
            //{
            //    context.Result = new UnauthorizedResult();
            //    return;
            //}

            //// Create the claims and add identity to principal
            //var claims = new List<Claim>();
            //claims.Add(new Claim("Id", user["Id"]));
            //claims.Add(new Claim(ClaimTypes.Name, user["Name"]));
            //claims.Add(new Claim(ClaimTypes.Role, user["Role"]));

            //var identity = new ClaimsIdentity(claims, "Jwt");
            //context.HttpContext.User = new ClaimsPrincipal(identity);
        }
    }
}
