using ConsoleMenuToolKit.Application;
using ConsoleMenuToolKit.Contracts;
using ConsoleMenuToolKit.Enum;

namespace ConsoleMenuToolKit.Tests.Utilities
{
    public class FakeSelector : IConsoleMenuSelector
    {
        private readonly Queue<ConsoleMenuOption> _options;

        public FakeSelector(IEnumerable<ConsoleMenuOption> options)
        {
            _options = new Queue<ConsoleMenuOption>(options);
        }

        public ConsoleMenuOption ObtainOption(
            IReadOnlyList<ConsoleMenuOption> options, 
            ConsoleMenuSelectionType selectionType) => 
            _options.Dequeue();
    }
}
