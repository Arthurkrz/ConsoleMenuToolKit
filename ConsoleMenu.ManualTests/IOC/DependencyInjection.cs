using ConsoleMenu.Contracts;
using ConsoleMenu.ManualTests.Contracts.Service;
using ConsoleMenu.ManualTests.Handlers;
using ConsoleMenu.ManualTests.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenu.ManualTests.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuditService, AuditService>();
            services.AddSingleton<ISpecialInventoryService, SpecialInventoryService>();
            services.AddSingleton<IInventoryService, InventoryService>();
            services.AddSingleton<ISpecialOrderService, SpecialOrderService>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IReportService, ReportService>();
            return services;
        }

        public static IServiceCollection InjectHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IConsoleMenuHandler, CreateOrderHandler>();
            services.AddSingleton<IConsoleMenuHandler, GenerateDailyReportHandler>();
            return services;
        }
    }
}
