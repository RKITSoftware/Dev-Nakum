namespace Basic_Fundamental
{
    /// <summary>
    /// Entry point of the ASP.NET Core application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method, the entry point of the application.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Creates a WebApplicationBuilder instance.
            var builder = WebApplication.CreateBuilder(args);

            // Initializes Startup with configuration settings.
            var startup = new Startup(builder.Configuration);

            // Calls the ConfigureServices method of the Startup class to configure services.
            startup.ConfigureServices(builder.Services);

            // Builds the application using the configured services.
            var app = builder.Build();

            // Calls the Configure method of the Startup class to configure the application's request pipeline.
            startup.Configure(app, builder.Environment);
        }
    }
}
