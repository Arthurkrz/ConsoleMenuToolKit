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

            var result = option.Kind switch
            {
                ConsoleMenuOptionKind.Exit => ConsoleMenuExecutionResult.Exit,

                ConsoleMenuOptionKind.Return => ConsoleMenuExecutionResult.Return,

                ConsoleMenuOptionKind.ReturnToMain => ConsoleMenuExecutionResult.ReturnToMain,

                _ => ConsoleMenuExecutionResult.Continue
            };

            return Task.FromResult(result);
        }
    }
}
