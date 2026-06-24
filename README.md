# ConsoleMenuToolKit

A flexible and extensible console menu toolkit for .NET.

Easily create interactive CLI menus with support for:
- Synchronous and asynchronous actions
- Custom handlers
- Nested menus with bidirectional navigation
- Multiple selection modes
- Optional dependency injection (via ConsoleMenuToolKit.DependencyInjection)

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

menu.AddOptionAsync(1, "Load Data", async () => 
{
    await Task.Delay(500);
    Console.WriteLine("Data loaded.");
});

menu.AddExitOption(2, "Exit");

await menu.RunAsync();
```

## Installation

### Core Package
```bash
dotnet add package ConsoleMenuToolKit
```

### Dependency Injection Support (Optional)
```bash
dotnet add package ConsoleMenuToolKit.DependencyInjection
```

## Selection Modes
- **ReadAfterConfirm** (default) allows multi-digit with the 'Enter' key
- **InstantRead** reads the first key immediately
- **ArrowSelection** allows dynamic navigation using arrow keys

```csharp
menu.UseSelectionType(ConsoleMenuSelectionType.ReadAfterConfirm); // or
menu.UseSelectionType(ConsoleMenuSelectionType.InstantRead); // or
menu.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);
```

## Available Menu Options
- Simple synchronous actions (`AddOption`)
- Simple asynchronous actions (`AddOptionAsync`)
- Asynchronous actions in handlers mapped by a key (`AddHandlerOption`)
- Option to access nested menus (`AddSubMenuOption`)
- Option to access nested menus mapped by a key (`AddSubMenuOptionWithKey`)
- Option to return to the previous nested menu (`AddReturnOption`)
- Option to return to the main menu (`AddReturnToMainOption`)
- Option to exit the menu (`AddExitOption`)

## 🛠 Setup Options

_ConsoleMenuToolKit can be used in two ways._

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

- **Advanced (handlers + dependency injection using the DI package)**

Recommended for larger or more structured applications.

```csharp
public static async Task Main(string[] args)
{
    var services = new ServiceCollection()
        .InjectServices()
        .InjectHandlers();

    services.AddConsoleMenu();

    var serviceProvider = services.BuildServiceProvider();

    var menu = new MenuConfiguration(serviceProvider);
    await menu.RunAsync();
}

public class MenuConfiguration
{
    private readonly IServiceProvider _serviceProvider;

    public MenuConfiguration(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;    
    }

    public async Task RunAsync()
    {
        var selector = _serviceProvider.GetRequiredService<IConsoleMenuSelector>();
        var executor = _serviceProvider.GetRequiredService<IConsoleMenuExecutor>();

        var menu = new ConsoleMenuSetup(selector, executor);

        menu.AddHandlerOption(1, "Create order", "create-order")
            .AddHandlerOption(2, "Generate sale report", "generate-sale-report")
            .AddSubMenuOptionWithKey(
                3, "Generate report for a product", "report-specific-product")
            .AddReturnToMainOption(4, "Return to main menu")
            .AddExitOption(5, "Exit");

        await menu.RunAsync();
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
Handlers solve this by moving execution logic into dedicated classes. Instead of attaching an action, you assign a **Key** to the option:

```csharp
menu.AddHandlerOption(1, "Create order", "create-order");

public class CreateOrderHandler : IConsoleMenuHandler
{
    public string Key => "create-order";

    public Task ExecuteAsync()
    {
        Console.WriteLine("Order created.");
        return Task.CompletedTask;
    }
}
```

## 🧩 Nested Menus (Sub-Menus)

Sub-menus allow menus to be organized hierarchically. Users can navigate to child menus and return either to the previous menu or directly to the main menu.

In simple scenarios, submenus can be created manually, as seen in the example below where a user can generate sales reports for all items in the main menu, all electronics in the electronics sub-menu or a specific electronic type in the specific electronics sub-menu.

```csharp
var menu = new ConsoleMenuSetup(_consoleMenuSelector, _consoleMenuExecutor);
var subMenuElectronics = new ConsoleMenuSetup(_consoleMenuSelector, _consoleMenuExecutor);
var subMenuSpecificElectronics = new ConsoleMenuSetup(_consoleMenuSelector, _consoleMenuExecutor);

menu.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);
subMenuElectronics.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);
subMenuSpecificElectronics.UseSelectionType(ConsoleMenuSelectionType.ArrowSelection);

subMenuSpecificElectronics
    .AddHandlerOption(1, "Reports for phones", "reports-phones")
    .AddHandlerOption(2, "Reports for computers", "reports-computers")
    .AddReturnOption(3, "Back to previous menu")
    .AddReturnToMainOption(4, "Back to main menu")
    .AddExitOption(5, "Exit");

subMenuElectronics
    .AddHandlerOption(1, "Reports for all electronics", "reports-all-electronics")
    .AddSubMenuOption(2, "Reports for specific electronics", subMenuSpecificElectronics)
    .AddReturnToMainOption(3, "Back to main menu")
    .AddExitOption(4, "Exit");

menu
    .AddHandlerOption(2, "Generate daily reports all products", "reports-all")
    .AddSubMenuOption(3, "Generate daily reports for electronics", subMenuElectronics)
    .AddExitOption(4, "Exit");

await menu.RunAsync();
```

The hierarchy tree follows the order:
```
Main Menu
└── Electronics
    └── Specific Electronics
```

**Note**: Direct submenus must be created from the deepest level upward because the submenu instance is required when configuring the parent menu.

For larger applications, this approach becomes confusing and hard to maintain. For this reason, you can attach a **Key** to each nested menu, similarly to handler options. This way, menus can be created at any order and not necessarily in the same class.

```csharp
menu.AddSubMenuOptionWithKey(
    3,
    "Generate daily reports for electronics",
    "sub-all-electronics");

public class ElectronicsSubMenu : IConsoleMenuSubMenu
{
    public string Key => "sub-all-electronics";

    public ConsoleMenuSetup Build()
    {
        return new ConsoleMenuSetup()
            .AddHandlerOption(1, "Reports for all electronics", "reports-all-electronics")

            .AddSubMenuOptionWithKey(2, 
                "Reports for specific electronics","sub-specific-electronics")

            .AddReturnToMainOption(3, "Back to main menu")
            .AddExitOption(4, "Exit");
    }
}
```

## 📄 License
This project is licensed under the MIT License.