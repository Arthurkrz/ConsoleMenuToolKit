using ConsoleMenuToolKit.Application;
using ConsoleMenuToolKit.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenuToolKit.DependencyInjection
{
    /// <summary>
    /// Dependency Injection class for complex configuration.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the necessary services for the console menu to the service collection.
        /// </summary>
        /// <param name="services">Necessary parameter for dependency injection.</param>
        /// <returns>Returns itself, allowing for chained calls.</returns>
        public static IServiceCollection AddConsoleMenu(this IServiceCollection services)
        {
            services.AddSingleton<IConsoleMenuWrapper, ConsoleMenuWrapper>();
            services.AddTransient<IConsoleMenuSelector, ConsoleMenuSelector>();
            services.AddTransient<IConsoleMenuExecutor, ConsoleMenuExecutor>();

            return services;
        }
    }
}
