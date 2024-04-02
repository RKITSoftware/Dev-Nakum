using Dependency_Injection.BL;
using Dependency_Injection.Interface;

namespace Dependency_Injection.Extension
{
    /// <summary>
    /// class for manage the extension method
    /// </summary>
    public static class AddLifeTimeService
    {
        /// <summary>
        /// Extension method for adding the services into startup class
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IUsers, BLUsers>();
            services.AddScoped<IBank, BLBank>();
            services.AddTransient<IOtpGenerate, BLOTP>();

            return services;
        }
    }
}
