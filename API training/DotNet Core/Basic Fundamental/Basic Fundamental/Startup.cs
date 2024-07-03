namespace Basic_Fundamental
{
    /// <summary>
    /// The Startup class initializes and configures the ASP.NET Core application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// IConfiguration instance to access configuration settings.
        /// </summary>
        public IConfiguration configRoot
        {
            get;
        }

        /// <summary>
        /// Constructor to initialize IConfiguration.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        /// <summary>
        /// Configures the services required by the application.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); // Adds controller services.
            services.AddEndpointsApiExplorer(); // Adds API explorer services.
            services.AddSwaggerGen(); // Configures Swagger generation.
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configurations for non-development environment.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error"); // Handles exceptions.
                // Configures HTTP Strict Transport Security (HSTS).
                app.UseHsts();
            }

            // Configurations specific to development environment.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // Enables Swagger.
                app.UseSwaggerUI(); // Configures Swagger UI.
            }

            app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS.
            app.UseStaticFiles(); // Enables serving static files.
            app.UseRouting(); // Enables routing.
            app.UseAuthorization(); // Adds authorization middleware.
            app.MapControllers(); // Maps controllers.
            app.Run(); // Executes the request pipeline.
        }
    }
}
