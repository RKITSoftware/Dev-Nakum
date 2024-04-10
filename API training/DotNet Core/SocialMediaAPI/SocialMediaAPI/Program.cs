namespace SocialMediaAPI
{
    public class Program
    {
        /// <summary>
        /// The entry point for the ASP.NET Core application.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            // Create a WebApplication builder instance
            var builder = WebApplication.CreateBuilder(args);

            // Create a new instance of the Startup class and inject the configuration
            var startup = new Startup(builder.Configuration);

            // Configure application services using the Startup class
            startup.ConfigureServices(builder.Services);

            // Build the WebApplication instance based on the configuration
            var app = builder.Build();

            // Configure the HTTP request pipeline using the Startup class and hosting environment
            startup.Configure(app, builder.Environment);
        }
    }
}