using ConsoleMenuToolKit.Application;
using ConsoleMenuToolKit.Contracts;
using ConsoleMenuToolKit.Enum;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenuToolKit.ManualTests.SubMenus
{
    public class SubMenuAllItems : IConsoleMenuSubMenu
    {
        private readonly IServiceProvider _serviceProvider;

        public SubMenuAllItems(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string Key => "sub-all-special-items";

        public ConsoleMenuSetup Build()
        {
            var selector = _serviceProvider.GetRequiredService<IConsoleMenuSelector>();
            var executor = _serviceProvider.GetRequiredService<IConsoleMenuExecutor>();

            return new ConsoleMenuSetup(selector, executor)
                .UseSelectionType(ConsoleMenuSelectionType.ArrowSelection)
                .AddHandlerOption(1, "Generate report for all special items", "generate-report-all-special-items")
                .AddSubMenuOptionWithKey(2, "Create order for specific special items", "sub-specific-special-items")
                .AddReturnToMainOption(3, "Back to main menu")
                .AddExitOption(4, "Exit");
        }
    }
}
