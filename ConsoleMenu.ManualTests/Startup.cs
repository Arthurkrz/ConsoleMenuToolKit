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

        public async Task ExecuteWithoutHandlersAsync()
        {
            ISpecialInventoryService specialInventoryService = new SpecialInventoryService();
            IInventoryService inventoryService = new InventoryService();

            ISpecialOrderService specialOrderService = new SpecialOrderService(specialInventoryService);
            IOrderService orderService = new OrderService(inventoryService);

            IAuditService auditService = new AuditService();
            IReportService reportService = new ReportService(auditService);

            var mainMenu = new ConsoleMenuSetup();
            var subMenuAllItems = new ConsoleMenuSetup();
            var subMenuSpecificItems = new ConsoleMenuSetup();

            mainMenu.UseSelectionType(ConsoleMenuSelectionType.ReadAfterConfirm);
            subMenuAllItems.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);
            subMenuSpecificItems.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);

            subMenuSpecificItems
                .AddOption(1, "Special Item 1", () => 
                    specialOrderService.CreateOrderSpecificProductAsync("Special Item 1"))

                .AddOption(2, "Special Item 2", () => 
                    specialOrderService.CreateOrderSpecificProductAsync("Special Item 2"))

                .AddReturnOption(3, "Back to main menu");

            subMenuAllItems
                .AddOption(1, "Generate report for all special items", () => 
                    reportService.GenerateReportForAllSpecialItemsAsync())

                .AddSubMenuOption(2, "Generate report for specific special items", subMenuSpecificItems)

                .AddReturnOption(3, "Back to main menu");

            mainMenu
                .AddOption(1, "Create order", () => 
                    orderService.CreateOrderAsync())

                .AddOption(2, "Generate daily report", () => 
                    reportService.GenerateDailyReportAsync())

                .AddSubMenuOption(3, "Generate daily reports for special items", subMenuAllItems)

                .AddExitOption(4, "Exit");

            await mainMenu.Run();
        }

        public async Task ExecuteWithHandlersAsync()
        {
            var menu = new ConsoleMenuSetup(_consoleMenuSelector, _consoleMenuExecutor);

            menu.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);

            menu.AddHandlerOption(1, "Create order", "create-order")
                .AddHandlerOption(2, "Generate daily report", "generate-daily-report")
                .AddExitOption(3, "Exit");

            await menu.Run();
        }
    }
}
