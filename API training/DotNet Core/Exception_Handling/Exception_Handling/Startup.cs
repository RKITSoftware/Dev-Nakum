using Microsoft.AspNetCore.Builder;

namespace Exception_Handling
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
            services.AddSwaggerGen();

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
                //app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions()
                //{
                //    SourceCodeLineCount = 3,
                //});
                app.UseExceptionHandler("/api/error");
            }
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
