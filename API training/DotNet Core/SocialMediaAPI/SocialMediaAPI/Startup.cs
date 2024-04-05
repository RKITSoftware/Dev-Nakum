using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMediaAPI.BL;
using SocialMediaAPI.Extension;
using SocialMediaAPI.Interface;
using System.Configuration;
using System.Text;

namespace SocialMediaAPI
{
    /// <summary>
    /// start class for configuring the all services
    /// </summary>
    public class Startup
    {
        public IConfiguration configRoot { get; }

        /// <summary>
        /// Initialize a new instance of the startup class 
        /// </summary>
        /// <param name="configuration">application's configurations</param>
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        /// <summary>
        /// configures service for the application
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            //add controller services
            services.AddControllers();

            //endpoint services
            services.AddEndpointsApiExplorer();

            // swagger service
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "jwt token",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                // Add security requirement for JWT authentication
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddJwtAuthentication(configRoot);
            services.AddInterfaceServices();
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app"> application builder.</param>
        /// <param name="environment"> hosting environment.</param>
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.UseAuthentication();
            app.UseAuthorization();


            app.Run();
        }
    }
}
