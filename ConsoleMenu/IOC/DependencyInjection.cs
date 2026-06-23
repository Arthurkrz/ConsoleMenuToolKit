using ConsoleMenu.Application;
using ConsoleMenu.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenu.IOC
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
            services.AddSingleton<IConsoleMenuSelector, ConsoleMenuSelector>();
            services.AddSingleton<IConsoleMenuExecutor, ConsoleMenuExecutor>();

            return services;
        }
    }
}