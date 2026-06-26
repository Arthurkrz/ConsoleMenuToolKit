using ConsoleMenuToolKit.Application;
using ConsoleMenuToolKit.Contracts;

namespace ConsoleMenuToolKit.Tests.Utilities
{
    public class FakeSubMenu : IConsoleMenuSubMenu
    {
        private readonly ConsoleMenuSetup _menu = null!;

        public string Key { get; }

        public FakeSubMenu(string key, ConsoleMenuSetup menu = null!)
        {
            _menu = menu;
            Key = key;
        }

        public ConsoleMenuSetup Build() => _menu;
    }
}
