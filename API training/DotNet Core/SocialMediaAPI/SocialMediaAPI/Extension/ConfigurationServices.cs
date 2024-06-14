using Check_Id_Exist;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SocialMediaAPI.BL;
using SocialMediaAPI.DB;
using SocialMediaAPI.Interface;
using System.Text;

namespace SocialMediaAPI.Extension
{
    /// <summary>
    ///  Extension class for IServiceCollection.
    /// </summary>
    public static class ConfigurationServices
    {
        /// <summary>
        /// Adds interface services for dependency injection.
        /// </summary>
        /// <param name="services"></param>
        public static void AddInterfaceServices(this IServiceCollection services)
        {
            // adds transient services for validation
            services.AddTransient<Validation>();


            // Adds singleton service for user management.
            services.AddSingleton<IUserService, BLUse01>();

            // Adds transient services for post-related functionalities.
            services.AddTransient<IPostService, BLPos01>();

            // Adds transient services for comment-related functionalities.
            services.AddTransient<ICommentService, BLCom01>();

            // Adds scoped services for managing followers.
            services.AddScoped<IFollowersService, BLFol01>();

            services.AddTransient<IDBUse01,DBUse01>();

        }

        /// <summary>
        ///  Configures JWT authentication for the application.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Retrieves JWT issuer and key from the app settings.
            var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();

            // Adds JWT authentication with bearer token validation.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Configures token validation parameters.
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

