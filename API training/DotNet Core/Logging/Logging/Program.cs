namespace Logging
{
    /// <summary>
    /// execution start with this class
    /// </summary>
    public class Program
    {
        /// <summary>
        ///  Entry point of the program
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services); // calling ConfigureServices method
            var app = builder.Build();
            startup.Configure(app, builder.Environment); // calling Configure method
        }
    }
}