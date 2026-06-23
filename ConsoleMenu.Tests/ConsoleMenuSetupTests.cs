using ConsoleMenu.Application;
using ConsoleMenu.Enum;
using ConsoleMenu.Tests.Utilities;

namespace ConsoleMenu.Tests
{
    public class ConsoleMenuSetupTests
    {
        [Fact]
        public async Task RunAsync_ShouldLoopUntilExit()
        {
            // Arrange
            var option = ConsoleMenuOption.Create(1, "Test", () => { });
            var exit = ConsoleMenuOption.CreateExit(5, "Test");

            var selector = new FakeSelector([option, option, exit]);
            var executor = new FakeExecutor();

            var sut = new ConsoleMenuSetup(selector, executor);
            sut.AddOption(1, "Test", () => { });
            sut.AddExitOption(2, "Test");

            // Act
            await sut.RunAsync();

            // Assert
            Assert.Equal(3, executor.ExecutionCount);
        }

        [Fact]
        public async Task RunInternalAsync_ShouldReturnContinue_WhenReturnSelected()
        {
            // Arrange
            var returnOption = ConsoleMenuOption.CreateReturn(1, "Test");

            var selector = new FakeSelector([returnOption]);
            var executor = new FakeExecutor();

            // Act
            var sut = new ConsoleMenuSetup(selector, executor)
                .AddReturnOption(1, "Test");

            var result = await sut.RunInternalAsync();

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Continue, result);
        }

        [Fact]
        public async Task RunInternalAsync_ShouldReturnReturnToMain_WhenReturnToMainSelectedNotMainMenu()
        {
            // Arrange
            var returnToMain = ConsoleMenuOption.CreateReturnToMain(1, "Test");

            var selector = new FakeSelector([returnToMain]);
            var executor = new FakeExecutor();

            // Act
            var sut = new ConsoleMenuSetup(selector, executor)
                .AddReturnToMainOption(1, "Test");

            var result = await sut.RunInternalAsync();

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.ReturnToMain, result);
        }

        [Fact]
        public async Task RunInternalAsync_ShouldContinueUntilExit_WhenReturnToMainSelectedOnMainMenu()
        {
            // Arrange
            var returnToMain = ConsoleMenuOption.CreateReturnToMain(1, "Test");
            var exit = ConsoleMenuOption.CreateExit(2, "Test");

            var selector = new FakeSelector([returnToMain, exit]);
            var executor = new FakeExecutor();

            // Act
            var sut = new ConsoleMenuSetup(selector, executor)
                .AddReturnToMainOption(1, "Test")
                .AddExitOption(2, "Test");

            var result = await sut.RunInternalAsync(isMainMenu: true);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Exit, result);
            Assert.Equal(2, executor.ExecutionCount);
        }

        [Fact]
        public async Task RunInternalAsync_ShouldReturnExit_WhenExitSelected()
        {
            // Arrange
            var exit = ConsoleMenuOption.CreateExit(1, "Test");

            var selector = new FakeSelector([exit]);
            var executor = new FakeExecutor();

            // Act
            var sut = new ConsoleMenuSetup(selector, executor)
                .AddExitOption(1, "Test");

            var result = await sut.RunInternalAsync();

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Exit, result);
        }
    }
}
