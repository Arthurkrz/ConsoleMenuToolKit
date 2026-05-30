using ConsoleMenu.Contracts;
using ConsoleMenu.ManualTests.Contracts.Service.Async;
using ConsoleMenu.ManualTests.Contracts.Service.Sync;
using ConsoleMenu.ManualTests.Handlers;
using ConsoleMenu.ManualTests.Services.Async;
using ConsoleMenu.ManualTests.Services.Sync;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenu.ManualTests.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuditServiceAsync, AuditServiceAsync>();
            services.AddSingleton<ISpecialInventoryServiceAsync, SpecialInventoryServiceAsync>();
            services.AddSingleton<IInventoryServiceAsync, InventoryServiceAsync>();
            services.AddSingleton<ISpecialOrderServiceAsync, SpecialOrderServiceAsync>();
            services.AddSingleton<IOrderServiceAsync, OrderServiceAsync>();
            services.AddSingleton<IReportServiceAsync, ReportServiceAsync>();

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
            services.AddSingleton<IConsoleMenuHandler, CreateOrderSpecialItem1Handler>();
            services.AddSingleton<IConsoleMenuHandler, CreateOrderSpecialItem2Handler>();
            services.AddSingleton<IConsoleMenuHandler, GenerateDailyReportHandler>();
            services.AddSingleton<IConsoleMenuHandler, GenerateReportAllSpecialItemsHandler>();
            return services;
        }
    }
}
