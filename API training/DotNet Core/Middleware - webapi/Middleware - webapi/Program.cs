namespace Middleware___webapi
{
    /// <summary>
    /// Main class for executing the program
    /// </summary>
    public class Program
    {
        /// <summary>
        ///  Main method
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