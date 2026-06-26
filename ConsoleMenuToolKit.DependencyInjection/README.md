# ConsoleMenuToolKit.DependencyInjection

Dependency injection extensions for ConsoleMenuToolKit. This package provides the `AddConsoleMenu()` extension method and enables automatic resolution of registered services, menu handlers and submenu providers through `Microsoft.Extensions.DependencyInjection`.

## 🚀 Installation

```bash
dotnet add package ConsoleMenuToolKit.DependencyInjection
```

## 🛠 Registering Services

```csharp
using ConsoleMenu.DependencyInjection;

var services = new ServiceCollection();

services.AddConsoleMenu();
services.AddSingleton/Transient/Scoped<IConsoleMenuHandler, YourHandler>();
services.AddSingleton/Transient/Scoped<IConsoleMenuSubMenu, YourSubMenu>();
```

## 📄 Notes

- This package is optional.
- Install only `ConsoleMenuToolKit` if dependency injection is not required.
- Handler and submenu registrations are automatically resolved by the menu executor.

## License

This project is licensed under the MIT License.