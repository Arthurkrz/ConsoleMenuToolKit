using ConsoleMenuToolKit.Application;
using ConsoleMenuToolKit.Contracts;
using ConsoleMenuToolKit.Enum;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenuToolKit.ManualTests.SubMenus
{
    public class SubMenuSpecificItems : IConsoleMenuSubMenu
    {
        private readonly IServiceProvider _serviceProvider;

        public SubMenuSpecificItems(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string Key => "sub-specific-special-items";

        public ConsoleMenuSetup Build()
        {
            var selector = _serviceProvider.GetRequiredService<IConsoleMenuSelector>();
            var executor = _serviceProvider.GetRequiredService<IConsoleMenuExecutor>();

            return new ConsoleMenuSetup(selector, executor)
                .UseSelectionType(ConsoleMenuSelectionType.ArrowSelection)
                .AddHandlerOption(1, "Create order for Special Item 1", "create-order-specific-item-1")
                .AddHandlerOption(2, "Create order for Special Item 2", "create-order-specific-item-2")
                .AddReturnOption(3, "Back to previous menu")
                .AddReturnToMainOption(4, "Back to main menu")
                .AddExitOption(5, "Exit");
        }
    }
}
