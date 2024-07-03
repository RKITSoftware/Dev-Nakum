using Filter.Extension;
using Filter.Filter;
using Microsoft.Extensions.Caching.Memory;

namespace Filter.Model
{
    /// <summary>
    /// Entry point of the execution
    /// Add filters, middleware 
    /// </summary>
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        /// <summary>
        /// Constructor to initialize IConfiguration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers with global filters
            services.AddControllers(options =>
            {
                options.Filters.Add(new ActionAsyncFilterAttribute("Global-async",10));
                options.Filters.Add(new ActionFilterAttribute("Global"));

                options.Filters.Add(new ExceptionAsyncFilter("Global-async"));
                options.Filters.Add(new ExceptionFilter("Global"));

                options.Filters.Add(new ResultAsyncFilter("Global-async"));
                options.Filters.Add(new ResultFilter("Global"));

            });

            // Add API explorer and Swagger documentation generation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.JwtConfiguration();
            });

            services.AddMemoryCache();
            services.AddSingleton<ResourceAsyncFilterAttribute>(sp =>
            {
                var cache = sp.GetRequiredService<IMemoryCache>();
                return new ResourceAsyncFilterAttribute("Action GetProduct", cache);
            });

            // Add JWT authentication
            services.AddJwtAuthentication(configRoot);
        }

        /// <summary>
        /// Configure the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Handle errors and enable HSTS in production
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Enable Swagger UI in development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Enable authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();

            // Start the application
            app.Run();
        }
    }
}
