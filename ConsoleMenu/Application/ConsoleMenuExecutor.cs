using ConsoleMenu.Contracts;
using ConsoleMenu.Enum;

namespace ConsoleMenu.Application
{
    /// <summary>
    /// Class responsible for executing menu options selected by the user.
    /// It supports action-based options, handler-based options, submenu navigation,
    /// and menu control options such as Return, ReturnToMain, and Exit.
    /// The executor coordinates the execution flow by invoking delegates,
    /// resolving and executing registered handlers, resolving submenus either
    /// directly or through dependency injection, and returning the appropriate
    /// execution result to the menu loop. The class also validates registered 
    /// handlers and submenu providers to prevent duplicate keys and 
    /// ensure predictable runtime behavior.
    /// </summary>
    public class ConsoleMenuExecutor : IConsoleMenuExecutor
    {
        private readonly IEnumerable<IConsoleMenuHandler> _handlers;
        private readonly IEnumerable<IConsoleMenuSubMenu> _subMenus;
        private readonly IConsoleMenuWrapper _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMenuExecutor"/> class.
        /// Receives the registered menu handlers, submenu providers, and console wrapper
        /// used during execution. The constructor validates that handler keys and submenu
        /// keys are unique to prevent ambiguous resolution at runtime. If no handlers 
        /// or submenu providers are supplied, empty collections are used.
        /// </summary>
        /// <param name="handlers">Collection of registered menu 
        /// handlers used for handler-based options.</param>
        /// <param name="subMenus">Collection of registered submenu 
        /// providers used for key-based submenu resolution.</param>
        /// <param name="console">Console wrapper used for user 
        /// interaction and console operations.</param>
        public ConsoleMenuExecutor(IEnumerable<IConsoleMenuHandler>? handlers, IEnumerable<IConsoleMenuSubMenu>? subMenus, IConsoleMenuWrapper console)
        {
            _handlers = handlers?.ToList() ?? [];
            _subMenus = subMenus?.ToList() ?? [];
            _console = console;

            ValidateDuplicateHandlerKeys(_handlers);
            ValidateDuplicateSubMenuKeys(_subMenus);
        }

        /// <summary>
        /// Executes the provided console menu option according to its configured kind.
        /// Action options execute their associated delegate. Handler options resolve and
        /// execute a registered handler by key. SubMenu options execute either the directly
        /// configured submenu or a submenu resolved by key from the registered submenu
        /// providers. Return, ReturnToMain, and Exit options return the corresponding
        /// execution result so the menu loop can navigate correctly.
        /// </summary>
        /// <param name="option">Option selected by the user.</param>
        /// <returns>A task containing the execution result that tells the menu loop 
        /// whether to continue, return to the previous menu, return 
        /// to the main menu, or exit.</returns>
        /// <exception cref="ArgumentNullException">Thrown when 
        /// <paramref name="option"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a handler 
        /// option has no registered handler, when a submenu option
        /// cannot be resolved by direct instance or key, or 
        /// when the option kind is unsupported.</exception>
        public async Task<ConsoleMenuExecutionResult> ExecuteAsync(ConsoleMenuOption option)
        {
            ArgumentNullException.ThrowIfNull(option);

            switch (option.Kind)
            {
                case ConsoleMenuOptionKind.Action:
                    await option.AsyncAction!();
                    _console.ContinueAfterInput();
                    _console.Clear();

                    return ConsoleMenuExecutionResult.Continue;

                case ConsoleMenuOptionKind.Handler:
                    var handler = _handlers.FirstOrDefault(x => x.Key == option.HandlerKey) 
                        ?? throw new InvalidOperationException($"Option '{option.HandlerKey}' " +
                            $"has no handler registered.");

                    await handler.ExecuteAsync();
                    _console.ContinueAfterInput();
                    _console.Clear();
                    
                    return ConsoleMenuExecutionResult.Continue;

                case ConsoleMenuOptionKind.SubMenu:
                    var subMenu = option.SubMenu;

                    if (subMenu is null)
                    {
                        var provider = _subMenus.FirstOrDefault(x => x.Key == option.SubMenuKey) ??
                            throw new InvalidOperationException($"Option '{option.Value}' " +
                                $"has no submenu key configured.");

                        subMenu = provider.Build();
                    }

                    var subMenuResult = await subMenu!.RunInternalAsync();

                    return subMenuResult is ConsoleMenuExecutionResult.Exit 
                        or ConsoleMenuExecutionResult.ReturnToMain 
                            ? subMenuResult 
                            : ConsoleMenuExecutionResult.Continue;

                case ConsoleMenuOptionKind.Return:
                    return ConsoleMenuExecutionResult.Return;

                case ConsoleMenuOptionKind.ReturnToMain:
                    return ConsoleMenuExecutionResult.ReturnToMain;

                case ConsoleMenuOptionKind.Exit:
                    return ConsoleMenuExecutionResult.Exit;

                default:
                    throw new InvalidOperationException(
                        $"Unsupported option kind '{option.Kind}'.");
            }
        }

        private static void ValidateDuplicateHandlerKeys(IEnumerable<IConsoleMenuHandler> handlers)
        {
            var duplicateKeys = handlers
                .GroupBy(x => x.Key)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateKeys.Count != 0)
                throw new InvalidOperationException($"Duplicate handler keys found: " +
                    $"{string.Join(", ", duplicateKeys)}");
        }

        private static void ValidateDuplicateSubMenuKeys(IEnumerable<IConsoleMenuSubMenu> subMenus)
        {
            var duplicateKeys = subMenus
                .GroupBy(x => x.Key)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateKeys.Count != 0)
                throw new InvalidOperationException($"Duplicate submenu keys found: " +
                    $"{string.Join(", ", duplicateKeys)}");
        }
    }
}
