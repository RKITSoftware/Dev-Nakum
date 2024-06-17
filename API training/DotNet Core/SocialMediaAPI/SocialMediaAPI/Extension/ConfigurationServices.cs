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
            services.AddTransient<IUse01Service, BLUse01>();

            // Adds transient services for post-related functionalities.
            services.AddTransient<IPos01Service, BLPos01>();

            // Adds transient services for comment-related functionalities.
            services.AddTransient<ICom01Service, BLCom01>();

            // Adds scoped services for managing followers.
            services.AddScoped<IFol01Service, BLFol01>();

            services.AddTransient<IDBUse01,DBUse01>();
            services.AddTransient<IDBPos01,DBPos01>();
            services.AddTransient<IDBCom01,DBCom01>();

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

