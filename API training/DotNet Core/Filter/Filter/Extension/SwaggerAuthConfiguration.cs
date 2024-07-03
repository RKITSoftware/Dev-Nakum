using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Filter.Extension
{
    /// <summary>
    /// This static class provides extension methods for configuring JWT authentication within Swagger documentation.
    /// </summary>
    public static class SwaggerAuthConfiguration
    {
        /// <summary>
        /// Configures SwaggerGenOptions to include JWT token-based authentication information.
        /// </summary>
        /// <param name="options">The SwaggerGenOptions instance to configure.</param>
        public static void JwtConfiguration(this SwaggerGenOptions options)
        {
            // Add a security definition named "Bearer" for JWT token authentication in the header
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT token required for authorization",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
            });

            // Add a security requirement for all endpoints using the "Bearer" security definition
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme  // Reference the "Bearer" security definition
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()  // No additional scopes required for this application (optional)
                }
            });
        }
    }
}
