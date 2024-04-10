using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMediaAPI.BL;
using SocialMediaAPI.Extension;
using SocialMediaAPI.Interface;
using SocialMediaAPI.Middleware;
using System.Configuration;
using System.Text;

namespace SocialMediaAPI
{
    /// <summary>
    /// Startup class responsible for configuring services and the HTTP request pipeline for the ASP.NET Core API.
    /// </summary>
    public class Startup
    {
        public IConfiguration configRoot { get; }

        /// <summary>
        /// Initializes a new instance of the Startup class.
        /// </summary>
        /// <param name="configuration">The application's configuration settings.</param>
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        /// <summary>
        /// Configures services used by the application.
        /// </summary>
        /// <param name="services">The collection of services to be registered.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register core ASP.NET Core services
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // Swagger configuration (details likely in omitted code)
            services.AddSwaggerGen(options =>
            {
                options.JwtConfiguration();
            });

            // Add AutoMapper for data mapping between models and DTOs
            services.AddAutoMapper(typeof(Startup).Assembly);

            // Configure JWT authentication using application configuration (details likely in a separate method)
            services.AddJwtAuthentication(configRoot);

            // Register services defined by interfaces (details likely in a separate method)
            services.AddInterfaceServices();
        }

        /// <summary>
        /// Configures the HTTP request pipeline for processing incoming requests.
        /// </summary>
        /// <param name="app">The WebApplication builder object.</param>
        /// <param name="env">The IWebHostEnvironment object representing the hosting environment.</param>
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure exception handling, security headers, and developer tools based on environment
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
                app.UseDeveloperExceptionPage();
            }

            // Add custom middleware for request logging 
            app.UseMiddleware<RequestLoggingMiddleware>();

            // Configure request processing pipeline
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            // Enable authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Start listening for incoming requests
            app.Run();
        }
    }
}
