using ConsoleMenuToolKit.Application;
using ConsoleMenuToolKit.Contracts;
using ConsoleMenuToolKit.Enum;
using ConsoleMenuToolKit.ManualTests.Contracts.Service.Sync;
using ConsoleMenuToolKit.ManualTests.Services.Sync;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenuToolKit.ManualTests
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

        public void ExecuteWithoutHandlers()
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

            mainMenu.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);
            subMenuAllItems.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);
            subMenuSpecificItems.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);

            subMenuSpecificItems
                .AddOption(1, "Special Item 1", () =>
                    specialOrderService.CreateOrderSpecificProduct("Special Item 1"))

                .AddOption(2, "Special Item 2", () =>
                    specialOrderService.CreateOrderSpecificProduct("Special Item 2"))

                .AddReturnOption(3, "Back to previous menu")

                .AddReturnToMainOption(4, "Back to main menu")

                .AddExitOption(5, "Exit");

            subMenuAllItems
                .AddOption(1, "Generate report for all special items", () =>
                    reportService.GenerateReportForAllSpecialItems())

                .AddSubMenuOption(2, "Create order for specific special items", subMenuSpecificItems)

                .AddReturnToMainOption(3, "Back to main menu")

                .AddExitOption(4, "Exit");

            mainMenu
                .AddOption(1, "Create order", () => 
                    orderService.CreateOrder())

                .AddOption(2, "Generate daily report", () => 
                    reportService.GenerateDailyReport())

                .AddSubMenuOption(3, "Generate daily report or create order for special items", subMenuAllItems)

                .AddExitOption(4, "Exit");

            mainMenu.Run();
        }

        public async Task ExecuteWithHandlersAsync()
        {
            var mainMenu = new ConsoleMenuSetup(_consoleMenuSelector, _consoleMenuExecutor);

            mainMenu.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);

            mainMenu
                .AddHandlerOption(1, "Create order", "create-order")

                .AddHandlerOption(2, "Generate daily report", "generate-daily-report")
                
                .AddSubMenuOptionWithKey(3, "Generate daily report or create order for special items", 
                    "sub-all-special-items")
                
                .AddExitOption(4, "Exit");

            await mainMenu.RunAsync();
        }
    }
}
