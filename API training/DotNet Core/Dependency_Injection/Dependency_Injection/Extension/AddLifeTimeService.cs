using Dependency_Injection.BL;
using Dependency_Injection.Interface;

namespace Dependency_Injection.Extension
{
    /// <summary>
    /// class for manage the extension method
    /// </summary>
    public static class AddLifeTimeService
    {
        #region Public Method
        /// <summary>
        /// Extension method for adding the services into start-up class
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //created once when they are request - same interface is used entire the application lifetime
            services.AddSingleton<IUsers, BLUsers>();

            //for one request - same instance is used life time,
            //instance not be shared with new one - previous data will be lost
            services.AddScoped<IBank, BLBank>();

            //like a written a "new" keyword
            //created every time
            services.AddTransient<IOtpGenerate, BLOTP>();

            return services;
        } 
        #endregion
    }
}
