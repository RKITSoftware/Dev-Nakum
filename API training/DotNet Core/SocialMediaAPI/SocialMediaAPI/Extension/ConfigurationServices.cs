using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SocialMediaAPI.BL;
using SocialMediaAPI.Interface;
using System.Text;

namespace SocialMediaAPI.Extension
{
    public static class ConfigurationServices 
    {
        public static void AddInterfaceServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, BLUsers>();
            services.AddTransient<IPostService, BLPosts>();
            services.AddTransient<ICommentService, BLComments>();
            services.AddScoped<IFollowersService, BLFollowers>();       
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });
        }
    }
}
