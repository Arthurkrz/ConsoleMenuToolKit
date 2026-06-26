# ConsoleMenuToolKit

A flexible and extensible console menu toolkit for .NET.

Easily create interactive CLI menus with support for:
- Simple actions
- Custom handlers
- Asynchronous operations
- Optional dependency injection

## 🚀 Quick Example

### Synchronous
```csharp
var menu = new ConsoleMenuSetup();

menu.AddOption(1, "Say Hello", () => Console.WriteLine("Hello!"));
menu.AddExitOption(2, "Exit");

menu.Run();
```

### Asynchronous
```csharp
var menu = new ConsoleMenuSetup();

menu.AddAsyncOption(1, "Load Data", async () => 
{
    await Task.Delay(500);
    Console.WriteLine("Data loaded.");
});

menu.AddExitOption(2, "Exit");

await menu.RunAsync();
```

## 🛠 Setup Options

*ConsoleMenuToolKit can be used in two ways.*

- **Simple (no handlers, no DI)**

Ideal for small applications or quick scripts.

```csharp
public static void Main(string[] args)
{
    var inventoryService = new InventoryService();
    var orderService = new OrderService(inventoryService);

    var menu = new ConsoleMenuSetup();

    menu.AddOption(1, "Create order", () => orderService.CreateOrder());
    menu.AddOption(2, "Generate sale report", () => inventoryService.GenerateSaleReport());
    menu.AddExitOption(3, "Exit");

    menu.Run();
}
```

- **Advanced (handlers + dependency injection)**

Recommended for larger or more structured applications.

```csharp
public static void Main(string[] args)
{
    var services = new ServiceCollection()
        .InjectServices()
        .InjectHandlers();

    services.AddConsoleMenu();

    var serviceProvider = services.BuildServiceProvider();

    var menu = new MenuConfiguration(serviceProvider);
    menu.Run();
}

public class MenuConfiguration
{
    private readonly IServiceProvider _serviceProvider;

    public MenuConfiguration(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;    
    }

    public void Run()
    {
        var selector = _serviceProvider.GetRequiredService<IConsoleMenuSelector>();
        var executor = _serviceProvider.GetRequiredService<IConsoleMenuExecutor>();

        var menu = new ConsoleMenuSetup(selector, executor);

        menu.AddHandlerOption(1, "Create order", "create-order")
            .AddHandlerOption(2, "Generate sale report", "generate-sale-report")
            .AddExitOption(3, "Exit");

        menu.Run();
    }
}
```

## 🧩 Handlers

Handlers allow you to separate menu configuration from execution logic.

In simple scenarios, you can attach logic directly to a menu option:

```csharp
menu.AddOption(1, "Create order", () => orderService.CreateOrder());
```

For larger applications, this approach can become hard to maintain.
Handlers solve this by moving execution logic into dedicated classes.

### Using Handlers

Instead of attaching an action, you assign a **Key** to the option:

```csharp
menu.AddHandlerOption(1, "Create order", "create-order");
```

When the option is selected, the corresponding handler is resolved and executed.

### Creating a Handler

A handler implements `IConsoleMenuHandler` and defines:
- A `Key` used to map the option
- An `Execute()` method containing the logic

```csharp
public class CreateOrderHandler : IConsoleMenuHandler
{
    private readonly IOrderService _orderService;

    public CreateOrderHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public void Execute() => _orderService.CreateOrder();

    public string Key => "create-order";
}
```

### Why Use Handlers?

Handlers are useful when:
- Menu logic should stay clean and readable
- Execution logic needs to be reusable
- Dependency injection is used
- Business logic should be separated from UI flow

## 📄 License
This project is licensed under the MIT License.