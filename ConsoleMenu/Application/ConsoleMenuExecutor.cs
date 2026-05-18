using ConsoleMenu.Contracts;
using ConsoleMenu.Enum;

namespace ConsoleMenu.Application
{
    /// <summary>
    /// Class responsible for execution of Action delegates or handler logic, dependending on the configuration type. 
    /// It implements the IConsoleMenuExecutor interface and uses an IConsoleWrapper to interact with the console. 
    /// The class validates the provided menu option, executes the corresponding action or handler logic based on 
    /// the option's kind, and manages console interactions such as waiting for user input and clearing the console after execution. 
    /// It also includes validation to ensure that there are no duplicate handler keys among the registered handlers, 
    /// which could lead to ambiguous behavior when executing handler options. 
    /// This class is essential for facilitating the execution of user-selected options in the console menu system.
    /// The implemented method runs asynchronously, but can also be used for synchronous actions.
    /// </summary>
    public class ConsoleMenuExecutor : IConsoleMenuExecutor
    {
        private readonly IEnumerable<IConsoleMenuHandler> _handlers;
        private readonly IConsoleMenuWrapper _console;

        /// <summary>
        /// Constructor for ConsoleMenuExecutor, which takes an optional collection of IConsoleMenuHandler instances 
        /// previously created by the user and an IConsoleWrapper as dependencies. The constructor validates that 
        /// there are no duplicate handler keys among the provided handlers to ensure that each handler can be 
        /// uniquely identified when executing handler options. If no handlers are provided, it initializes an 
        /// empty list of handlers. This setup allows for flexible configuration of the menu executor while 
        /// ensuring that the necessary components are in place for executing menu options effectively.
        /// </summary>
        /// <param name="handlers"></param>
        /// <param name="console"></param>
        public ConsoleMenuExecutor(IEnumerable<IConsoleMenuHandler>? handlers, IConsoleMenuWrapper console)
        {
            _handlers = handlers?.ToList() ?? new List<IConsoleMenuHandler>();
            ValidateDuplicateHandlerKeys(_handlers);

            _console = console;
        }

        /// <summary>
        /// Executes the provided console menu option based on its kind. For Action options, it will invoke the 
        /// associated action delegate. For Handler options, it will look up and execute the corresponding 
        /// handler based on the HandlerKey. For Exit options, it will return a result indicating that the 
        /// menu should be exited. The method also handles any customizations in the console such as waiting 
        /// for user input and clearing the console after execution. It includes validation to ensure that 
        /// Action options have an associated action and that Handler options have a valid handler key and 
        /// corresponding handler registered, throwing exceptions if these conditions are not met.
        /// This method runs asynchronously, but can also be used for synchronous actions.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<ConsoleMenuExecutionResult> ExecuteAsync(ConsoleMenuOption option)
        {
            ArgumentNullException.ThrowIfNull(option);

            switch (option.Kind)
            {
                case ConsoleMenuOptionKind.Action:
                    if (option.AsyncAction is null)
                        throw new InvalidOperationException($"Option '{option.Value}' has no action configured.");

                    await option.AsyncAction();
                    _console.ContinueAfterInput();
                    _console.Clear();
                    return ConsoleMenuExecutionResult.Continue;

                case ConsoleMenuOptionKind.Handler:
                    if (string.IsNullOrWhiteSpace(option.HandlerKey))
                        throw new InvalidOperationException($"Option '{option.HandlerKey}' has no handler registered.");

                    var handler = _handlers.FirstOrDefault(x => x.Key == option.HandlerKey) 
                        ?? throw new InvalidOperationException($"Option '{option.HandlerKey}' has no handler registered.");

                    await handler.ExecuteAsync();
                    _console.ContinueAfterInput();
                    _console.Clear();
                    return ConsoleMenuExecutionResult.Continue;

                case ConsoleMenuOptionKind.SubMenu:
                    if (option.SubMenu is null)
                        throw new InvalidOperationException($"Option '{option.Value}' has no submenu configured.");

                    var result = await option.SubMenu.RunInternalAsync();

                    return result == ConsoleMenuExecutionResult.Exit 
                        ? ConsoleMenuExecutionResult.Exit 
                        : ConsoleMenuExecutionResult.Continue;

                case ConsoleMenuOptionKind.Return:
                    return ConsoleMenuExecutionResult.Return;

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

            if (duplicateKeys.Any())
                throw new InvalidOperationException($"Duplicate handler keys found: {string.Join(", ", duplicateKeys)}");
        }
    }
}
