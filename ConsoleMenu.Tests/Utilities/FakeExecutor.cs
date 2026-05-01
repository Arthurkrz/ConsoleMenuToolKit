using ConsoleMenu.Application;
using ConsoleMenu.Contracts;
using ConsoleMenu.Enum;

namespace ConsoleMenu.Tests.Utilities
{
    public class FakeExecutor : IConsoleMenuExecutor
    {
        public int ExecutionCount { get; private set; }

        public Task<ConsoleMenuExecutionResult> ExecuteAsync(ConsoleMenuOption option)
        {
            ExecutionCount++;

            var result = option.Kind == 
                ConsoleMenuOptionKind.Exit ?
                ConsoleMenuExecutionResult.Exit :
                ConsoleMenuExecutionResult.Continue;

            return Task.FromResult(result);
        }
    }
}
