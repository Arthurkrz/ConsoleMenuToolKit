using ConsoleMenu.IOC;
using ConsoleMenu.ManualTests;
using ConsoleMenu.ManualTests.IOC;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .InjectServices()
    .InjectHandlers();

services.AddConsoleMenu();

var startup = new Startup(services.BuildServiceProvider());

while (true)
{
    Console.WriteLine("Select option for test type:" +
                      "\n1 - Test without handlers." +
                      "\n2 - Test with handlers." +
                      "\n0 - Exit.");

    if (int.TryParse(Console.ReadLine(), out var option) 
        && option > 0 && option <= 2)
    {
        Console.Clear();

        if (option == 1) startup.ExecuteWithoutHandlers();
        else await startup.ExecuteWithHandlersAsync();
    }

    else if (option == 0) break;

    else
    {
        Console.Clear();
        Console.WriteLine("Invalid Input.\n");
    }
}