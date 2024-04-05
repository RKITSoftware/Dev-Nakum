using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMediaAPI.BL;
using System.Security.Claims;
using System.Security.Principal;

namespace SocialMediaAPI.Filter
{
    public class JwtAuthorizationAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authorizationHeader.FirstOrDefault()?.Split(' ').Length!=2 || authorizationHeader.FirstOrDefault()?.Split(' ')[0] != "Bearer")
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string token = authorizationHeader.FirstOrDefault()?.Split(' ')[1];

            //BLAuth objBLAuth = new BLAuth();
            //Dictionary<string,string> user =objBLAuth.VerifyToken(token);

            //if (user != null)
            //{
            //    GenericIdentity objGenericIdentity = new GenericIdentity(user["Id"]);
            //    objGenericIdentity.AddClaim(new Claim("Id", user["Id"].ToString()));
            //    objGenericIdentity.AddClaim(new Claim(ClaimTypes.Name, user["Name"]));
            //    objGenericIdentity.AddClaim(new Claim(ClaimTypes.Email, user["Email"]));
                
            //    IPrincipal principal = new GenericPrincipal(objGenericIdentity, user["Role"].Split(","));

            //    Thread.CurrentPrincipal = principal;

            //    context.HttpContext.User = new ClaimsPrincipal(principal);
            //}

        }
    }
}
