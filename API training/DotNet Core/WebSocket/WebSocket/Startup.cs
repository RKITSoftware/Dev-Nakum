using WS.Middleware;

namespace WS
{
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
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("defaultPolicy",
                    builder => builder
                        .WithOrigins("http://127.0.0.1:5500/")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });



            services.AddSingleton<WebSocketConnectionManager>();
            services.AddSingleton<ChatWebSocketHandler>();
            services.AddControllers();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapRazorPages();
            app.MapControllers();
            app.MapRazorPages();

            app.UseWebSockets();
            app.UseCors("defaultPolicy");


            app.Map("/chat", appBuilder =>
            {
                var webSocketOptions = new WebSocketOptions
                {
                    KeepAliveInterval = TimeSpan.FromMinutes(2)
                };

                appBuilder.UseWebSockets(webSocketOptions);

                appBuilder.Use(async (context, next) =>
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var webSocketHandler = context.RequestServices.GetRequiredService<ChatWebSocketHandler>();
                        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await webSocketHandler.HandleWebSocket(context, webSocket);
                    }
                    else
                    {
                        await next();
                    }
                });
            });
            app.Run();
        }
    }
}
