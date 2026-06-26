using ConsoleMenuToolKit.Contracts;

namespace ConsoleMenuToolKit.Tests.Utilities
{
    public class FakeHandler : IConsoleMenuHandler
    {
        public string Key { get; }
        public bool IsExecuted { get; private set; }

        public FakeHandler(string key)
        {
            Key = key;
        }

        public Task ExecuteAsync()
        {
            IsExecuted = true;
            return Task.CompletedTask;
        }
    }
}
