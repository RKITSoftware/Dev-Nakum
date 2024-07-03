using Middleware___webapi.Extension;
using Middleware___webapi.Middleware;

namespace Middleware___webapi
{
    /// <summary>
    ///     Call the specific services and middleware
    /// </summary>
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.BasicAuthConfiguration();
            });
            services.AddHttpLogging(o => { });
        }

        public void ConfigureLogging(ILoggingBuilder logging)
        {
            // Clear default providers if needed
            // logging.ClearProviders();

            // Add Console provider for development
            logging.AddConsole();

            // You can add other providers here, like:
            // logging.AddDebug(); // Debug provider for debugging
            // logging.AddFile("path/to/logfile.txt"); // File provider for persistent logging
        }

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


            app.UseHttpLogging();
            app.UseMiddleware<CustomMiddleware>("Authorization");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllers();
            app.MapRazorPages();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("from non terminal middleware 1");
                await next();
            });
            app.Use(async (context, next) =>
            {
                Console.WriteLine("from non terminal middleware 2");
                await next();

            });
            app.Run();
        }
    }
}