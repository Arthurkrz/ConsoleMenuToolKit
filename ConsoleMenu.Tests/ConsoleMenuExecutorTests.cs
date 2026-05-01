using ConsoleMenu.Application;
using ConsoleMenu.Contracts;
using ConsoleMenu.Enum;
using ConsoleMenu.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenu.Tests
{
    public class ConsoleMenuExecutorTests
    {
        private ConsoleMenuExecutor _sut = new(Array.Empty<IConsoleMenuHandler>(), new FakeWrapper());

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
            var handler = new FakeHandler("test");
            _sut = new ConsoleMenuExecutor(new[] { handler }, new FakeWrapper());

            var option = ConsoleMenuOption.CreateWithHandler(1, "Test", "test");

            // Act
            var result = await _sut.ExecuteAsync(option);

            // Assert
            Assert.True(handler.IsExecuted);
            Assert.Equal(ConsoleMenuExecutionResult.Continue, result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldExit()
        {
            // Arrange
            var option = ConsoleMenuOption.CreateExit(1, "Exit");

            // Act
            var result = await _sut.ExecuteAsync(option);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Exit, result);
        }

        [Fact]
        public void ExecuteAsync_ShouldThrowException_WhenDuplicateHandlers()
        {
            // Arrange
            var handlers = new[]
            {
                new FakeHandler("duplicate"),
                new FakeHandler("duplicate")
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                new ConsoleMenuExecutor(handlers, new FakeWrapper()));
        }

        [Fact]
        public void ExecuteAsync_ShouldThrowException_WhenDIInjectsDuplicateHandlers()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddSingleton<IConsoleMenuHandler>(new FakeHandler("key"));
            services.AddSingleton<IConsoleMenuHandler>(new FakeHandler("key"));

            services.AddSingleton<IConsoleMenuWrapper, FakeWrapper>();

            var provider = services.BuildServiceProvider();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                provider.GetRequiredService<IConsoleMenuExecutor>());
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenHandlerNotFound()
        {
            // Arrange
            var option = ConsoleMenuOption.CreateWithHandler(1, "Test", "missing");

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _sut.ExecuteAsync(option));
        }
    }
}
