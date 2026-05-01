using ConsoleMenu.Application;
using ConsoleMenu.Contracts;
using ConsoleMenu.Enum;
using ConsoleMenu.ManualTests.Contracts.Service;
using ConsoleMenu.ManualTests.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenu.ManualTests
{
    public class Startup
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IConsoleMenuSelector _consoleMenuSelector;
        private readonly IConsoleMenuExecutor _consoleMenuExecutor;

        public Startup(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _consoleMenuSelector = _serviceProvider.GetRequiredService<IConsoleMenuSelector>();
            _consoleMenuExecutor = _serviceProvider.GetRequiredService<IConsoleMenuExecutor>();
        }

        public async Task ExecuteWithoutHandlers()
        {
            IInventoryService inventoryService = new InventoryService();
            IOrderService orderService = new OrderService(inventoryService);

            IAuditService auditService = new AuditService();
            IReportService reportService = new ReportService(auditService);

            var menu = new ConsoleMenuSetup();

            menu.UseSelectionType(ConsoleMenuSelectionType.ReadAfterConfirm);

            menu.AddOption(1, "Create order", () => orderService.CreateOrderAsync())
                .AddOption(2, "Generate daily report", () => reportService.GenerateDailyReportAsync())
                .AddExitOption(3, "Exit");

            await menu.Run();
        }

        public async Task ExecuteWithHandlers()
        {
            var menu = new ConsoleMenuSetup(_consoleMenuSelector, _consoleMenuExecutor);

            menu.UseSelectionType(ConsoleMenuSelectionType.ReadAfterConfirm);

            menu.AddHandlerOption(1, "Create order", "create-order")
                .AddHandlerOption(2, "Generate daily report", "generate-daily-report")
                .AddExitOption(3, "Exit");

            await menu.Run();
        }
    }
}
