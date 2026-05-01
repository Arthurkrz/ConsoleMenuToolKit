using ConsoleMenu.IOC;
using ConsoleMenu.ManualTests;
using ConsoleMenu.ManualTests.IOC;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .InjectServices()
    .InjectHandlers();

services.AddConsoleMenu();

var startup = new Startup(services.BuildServiceProvider());

 //await startup.ExecuteWithoutHandlers();
 await startup.ExecuteWithHandlers();