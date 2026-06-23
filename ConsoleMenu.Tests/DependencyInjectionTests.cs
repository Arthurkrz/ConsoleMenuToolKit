using ConsoleMenu.Application;
using ConsoleMenu.Contracts;
using ConsoleMenu.DependencyInjection;
using ConsoleMenu.Enum;
using ConsoleMenu.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleMenu.Tests
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void DependencyInjection_ShouldResolveSelector()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddConsoleMenu();
            services.AddSingleton<IConsoleMenuWrapper, FakeWrapper>();

            var provider = services.BuildServiceProvider();

            // Act
            var selector = provider.GetRequiredService<IConsoleMenuSelector>();

            // Assert
            Assert.NotNull(selector);
        }

        [Fact]
        public void DependencyInjection_ShouldResolveExecutor()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddConsoleMenu();
            services.AddSingleton<IConsoleMenuWrapper, FakeWrapper>();

            var provider = services.BuildServiceProvider();

            // Act
            var executor = provider.GetRequiredService<IConsoleMenuExecutor>();

            // Assert
            Assert.NotNull(executor);
        }

        [Fact]
        public async Task DependencyInjection_ShouldInjectHandlersIntoExecutor()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddConsoleMenu();
            services.AddSingleton<IConsoleMenuWrapper, FakeWrapper>();
            services.AddSingleton<IConsoleMenuHandler>(new FakeHandler("Test"));

            var provider = services.BuildServiceProvider();
            var executor = provider.GetRequiredService<IConsoleMenuExecutor>();

            var option = ConsoleMenuOption.CreateWithHandler(1, "Test", "Test");

            // Act
            var result = await executor.ExecuteAsync(option);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Continue, result);
        }

        [Fact]
        public async Task DependencyInjection_ShouldInjectSubMenusIntoExecutor()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddConsoleMenu();
            services.AddSingleton<IConsoleMenuWrapper, FakeWrapper>();

            var exit = ConsoleMenuOption.CreateExit(1, "Test");
            var selector = new FakeSelector([exit]);
            var executor = new FakeExecutor();

            var subMenu = new ConsoleMenuSetup(selector, executor)
                .AddExitOption(1, "Test");

            services.AddSingleton<IConsoleMenuSubMenu>(new FakeSubMenu("Test", subMenu));

            var provider = services.BuildServiceProvider();
            var menuExecutor = provider.GetRequiredService<IConsoleMenuExecutor>();

            var option = ConsoleMenuOption.CreateSubMenuWithKey(1, "Test", "Test");

            // Act
            var result = await menuExecutor.ExecuteAsync(option);

            // Assert
            Assert.Equal(ConsoleMenuExecutionResult.Exit, result);
        }
    }
}
