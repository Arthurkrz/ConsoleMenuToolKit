using ConsoleMenu.Application;
using ConsoleMenu.Contracts;
using ConsoleMenu.Enum;
using ConsoleMenu.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenu.Tests
{
    public class ConsoleMenuExecutorTests
    {
        private ConsoleMenuExecutor _sut = new([], [], new FakeWrapper());

        [Fact]
        public async Task ExecuteAsync_ShouldRunAction_WithNormalCreate()
        {
            // Arrange
            var called = false;
            var option = ConsoleMenuOption.Create(1, "Test", () => called = true);

            // Act
            var result = await _sut.ExecuteAsync(option);

            // Assert
            Assert.True(called);
            Assert.Equal(ConsoleMenuExecutionResult.Continue, result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldRunAction_WithCreateAsync()
        {
            // Arrange
            var called = false;
            var option = ConsoleMenuOption.CreateAsync(1, "Test", async () =>
            {
                called = true;
                await Task.CompletedTask;
            });

            // Act
            var result = await _sut.ExecuteAsync(option);

            // Assert
            Assert.True(called);
            Assert.Equal(ConsoleMenuExecutionResult.Continue, result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldRunHandler()
        {
            // Arrange
            var handler = new FakeHandler("Test");
            _sut = new ConsoleMenuExecutor([handler], [], new FakeWrapper());

            var option = ConsoleMenuOption.CreateWithHandler(1, "Test", "Test");

            // Act
            var result = await _sut.ExecuteAsync(option);

            // Assert
            Assert.True(handler.IsExecuted);
            Assert.Equal(ConsoleMenuExecutionResult.Continue, result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldRunSubMenu_WhenSubMenuKeyExists()
        {
            // Arrange
            var exit = ConsoleMenuOption.CreateExit(1, "Test");

            var selector = new FakeSelector([exit]);
            var executor = new FakeExecutor();

            var subMenu = new ConsoleMenuSetup(selector, executor)
                .AddExitOption(1, "Test");

            var subMenus = new[] { new FakeSubMenu("Test", subMenu) };

            var sut = new ConsoleMenuExecutor([], subMenus, new FakeWrapper());

            var option = ConsoleMenuOption.CreateSubMenuWithKey(1, "Test", "Test");

            // Act
            var results = await sut.ExecuteAsync(option);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Exit, results);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnReturn()
        {
            // Arrange
            var option = ConsoleMenuOption.CreateReturn(1, "Test");

            // Act
            var result = await _sut.ExecuteAsync(option);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Return, result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnReturnToMain()
        {
            // Arrange
            var option = ConsoleMenuOption.CreateReturnToMain(1, "Test");

            // Act
            var result = await _sut.ExecuteAsync(option);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.ReturnToMain, result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldRunSubMenuAndReturnExit_WhenSubMenuExits()
        {
            // Arrange
            var exit = ConsoleMenuOption.CreateExit(1, "Test");

            var selector = new FakeSelector([exit]);
            var executor = new FakeExecutor();

            var subMenu = new ConsoleMenuSetup(selector, executor)
                .AddExitOption(1, "Test");

            var option = ConsoleMenuOption.CreateSubMenu(1, "Test", subMenu);

            // Act
            var result = await _sut.ExecuteAsync(option);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Exit, result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldRunSubMenuAndReturnReturnToMain_WhenSubMenuReturnsToMain()
        {
            // Arrange
            var returnToMain = ConsoleMenuOption.CreateReturnToMain(1, "Test");

            var selector = new FakeSelector([returnToMain]);
            var executor = new FakeExecutor();

            var subMenu = new ConsoleMenuSetup(selector, executor)
                .AddReturnToMainOption(1, "Test");

            var option = ConsoleMenuOption.CreateSubMenu(1, "Test", subMenu);

            // Act
            var result = await _sut.ExecuteAsync(option);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.ReturnToMain, result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnExit()
        {
            // Arrange
            var option = ConsoleMenuOption.CreateExit(1, "Test");

            // Act
            var result = await _sut.ExecuteAsync(option);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Exit, result);
        }

        [Fact]
        public void ConsoleMenuExecutorConstruction_ShouldThrowException_WhenDuplicateHandlers()
        {
            // Arrange
            var handlers = new[]
            {
                new FakeHandler("Test"),
                new FakeHandler("Test")
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                new ConsoleMenuExecutor(handlers, [], new FakeWrapper()));
        }

        [Fact]
        public void ConsoleMenuExecutorConstruction_ShouldThrowException_WhenDuplicateSubMenus()
        {
            // Arrange
            var subMenus = new[]
            {
                new FakeSubMenu("Test"),
                new FakeSubMenu("Test")
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                new ConsoleMenuExecutor([], subMenus, new FakeWrapper()));
        }

        [Fact]
        public void ConsoleMenuExecutorConstruction_ShouldThrowException_WhenDIInjectsDuplicateHandlers()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddSingleton<IConsoleMenuHandler>(new FakeHandler("Test"));
            services.AddSingleton<IConsoleMenuHandler>(new FakeHandler("Test"));

            services.AddSingleton<IConsoleMenuWrapper, FakeWrapper>();

            var provider = services.BuildServiceProvider();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(
                provider.GetRequiredService<IConsoleMenuExecutor>);
        }

        [Fact]
        public void ConsoleMenuExecutorConstruction_ShouldThrowException_WhenDIInjectsDuplicateSubMenus()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddSingleton<IConsoleMenuSubMenu>(new FakeSubMenu("Test"));
            services.AddSingleton<IConsoleMenuSubMenu>(new FakeSubMenu("Test"));

            services.AddSingleton<IConsoleMenuWrapper, FakeWrapper>();

            var provider = services.BuildServiceProvider();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(
                provider.GetRequiredService<IConsoleMenuExecutor>);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenHandlerKeyNotFound()
        {
            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _sut.ExecuteAsync(ConsoleMenuOption.CreateWithHandler(1, "Test", "Test")));
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenSubMenuKeyNotFound()
        {
            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _sut.ExecuteAsync(ConsoleMenuOption.CreateSubMenuWithKey(1, "Test", "Test")));
        }
    }
}
