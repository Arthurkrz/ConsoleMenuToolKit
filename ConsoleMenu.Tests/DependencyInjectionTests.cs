using ConsoleMenu.Application;
using ConsoleMenu.Contracts;
using ConsoleMenu.Enum;
using ConsoleMenu.IOC;
using ConsoleMenu.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenu.Tests
{
    public class DependencyInjectionTests
    {
        [Fact]
        public async Task DependencyInjection_ShouldInjectHandlersIntoExecutor()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddConsoleMenu();
            services.AddSingleton<IConsoleMenuWrapper, FakeWrapper>();
            services.AddSingleton<IConsoleMenuHandler>(new FakeHandler("test"));

            var provider = services.BuildServiceProvider();
            var executor = provider.GetRequiredService<IConsoleMenuExecutor>();

            var option = ConsoleMenuOption.CreateWithHandler(1, "Test", "test");

            // Act
            var result = await executor.ExecuteAsync(option);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Continue, result);
        }
    }
}
