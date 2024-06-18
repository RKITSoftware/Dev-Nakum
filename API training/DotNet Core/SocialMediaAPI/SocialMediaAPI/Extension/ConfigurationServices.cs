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


            services.AddTransient<IUSE01Service, BLUSE01>();
            services.AddTransient<IPOS01Service, BLPOS01>();
            services.AddTransient<ICOM01Service, BLCOM01>();
            services.AddTransient<IFOL01Service, BLFOL01>();

            services.AddTransient<IDBUSE01,DBUSE01>();
            services.AddTransient<IDBPOS01,DBPOS01>();
            services.AddTransient<IDBCOM01,DBCOM01>();

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

