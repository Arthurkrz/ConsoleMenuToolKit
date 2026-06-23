using ConsoleMenu.Contracts;
using ConsoleMenu.Enum;

namespace ConsoleMenu.Application
{
    /// <summary>
    /// Provides the main configuration and execution entry point for a console menu.
    /// </summary>
    /// <remarks>
    /// A <see cref="ConsoleMenuSetup"/> instance owns a collection of menu options and
    /// coordinates option selection through an <see cref="IConsoleMenuSelector"/> and
    /// execution through an <see cref="IConsoleMenuExecutor"/>.
    /// Menus can contain action options, asynchronous action options, handler options,
    /// direct submenu options, key-based submenu options, return options, return-to-main
    /// options, and exit options.
    /// </remarks>
    public sealed class ConsoleMenuSetup
    {
        private readonly List<ConsoleMenuOption> _options = [];

        private readonly IConsoleMenuSelector _selector;
        private readonly IConsoleMenuExecutor _executor;

        private ConsoleMenuSelectionType _selectionType =
            ConsoleMenuSelectionType.ReadAfterConfirm;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMenuSetup"/> class using
        /// the default selector, executor, and console wrapper implementations.
        /// </summary>
        /// <param name="selectionType">
        /// The selection mode used by this menu. The default is
        /// <see cref="ConsoleMenuSelectionType.ReadAfterConfirm"/>.
        /// </param>
        public ConsoleMenuSetup(ConsoleMenuSelectionType selectionType = ConsoleMenuSelectionType.ReadAfterConfirm) 
            : this(new ConsoleMenuSelector(new ConsoleMenuWrapper()), 
                   new ConsoleMenuExecutor([], [], new ConsoleMenuWrapper())) { _selectionType = selectionType; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMenuSetup"/> class using
        /// the provided selector and executor.
        /// </summary>
        /// <param name="selector">
        /// The selector responsible for displaying the available options and obtaining
        /// the selected option from the user.
        /// </param>
        /// <param name="executor">
        /// The executor responsible for executing the selected option and returning the
        /// corresponding menu execution result.
        /// </param>
        public ConsoleMenuSetup(IConsoleMenuSelector selector, IConsoleMenuExecutor executor)
        {
            _selector = selector;
            _executor = executor;
        }

        /// <summary>
        /// Sets the selection mode used by this menu.
        /// </summary>
        /// <param name="selectionType">
        /// The selection mode to use when presenting and selecting options.
        /// </param>
        /// <returns>
        /// The current <see cref="ConsoleMenuSetup"/> instance for fluent configuration.
        /// </returns>
        public ConsoleMenuSetup UseSelectionType(ConsoleMenuSelectionType selectionType)
        {
            _selectionType = selectionType;
            return this;
        }

        /// <summary>
        /// Adds a synchronous action option to the menu.
        /// </summary>
        /// <param name="id">The unique option identifier.</param>
        /// <param name="value">The text displayed for the option.</param>
        /// <param name="action">The action executed when the option is selected.</param>
        /// <returns>The current <see cref="ConsoleMenuSetup"/> instance for fluent configuration.</returns>
        public ConsoleMenuSetup AddOption(int id, string value, Action action)
        {
            _options.Add(ConsoleMenuOption.Create(id, value, action));
            return this;
        }

        /// <summary>
        /// Adds an asynchronous action option to the menu.
        /// </summary>
        /// <param name="id">The unique option identifier.</param>
        /// <param name="value">The text displayed for the option.</param>
        /// <param name="asyncAction">The asynchronous action executed when the option is selected.</param>
        /// <returns>The current <see cref="ConsoleMenuSetup"/> instance for fluent configuration.</returns>
        public ConsoleMenuSetup AddOptionAsync(int id, string value, Func<Task> asyncAction)
        {
            _options.Add(ConsoleMenuOption.CreateAsync(id, value, asyncAction));
            return this;
        }

        /// <summary>
        /// Adds a handler-based option to the menu.
        /// </summary>
        /// <param name="id">The unique option identifier.</param>
        /// <param name="value">The text displayed for the option.</param>
        /// <param name="handlerKey">
        /// The key used by the executor to resolve the registered handler.
        /// </param>
        /// <returns>The current <see cref="ConsoleMenuSetup"/> instance for fluent configuration.</returns>
        public ConsoleMenuSetup AddHandlerOption(int id, string value, string handlerKey)
        {
            _options.Add(ConsoleMenuOption.CreateWithHandler(id, value, handlerKey));
            return this;
        }

        /// <summary>
        /// Adds a direct submenu option to the menu.
        /// </summary>
        /// <param name="id">The unique option identifier.</param>
        /// <param name="value">The text displayed for the option.</param>
        /// <param name="subMenu">The submenu executed when this option is selected.</param>
        /// <returns>The current <see cref="ConsoleMenuSetup"/> instance for fluent configuration.</returns>
        public ConsoleMenuSetup AddSubMenuOption(int id, string value, ConsoleMenuSetup subMenu)
        {
            _options.Add(ConsoleMenuOption.CreateSubMenu(id, value, subMenu));
            return this;
        }

        /// <summary>
        /// Adds a key-based submenu option to the menu.
        /// </summary>
        /// <param name="id">The unique option identifier.</param>
        /// <param name="value">The text displayed for the option.</param>
        /// <param name="subMenuKey">
        /// The key used by the executor to resolve a registered submenu provider.
        /// </param>
        /// <returns>The current <see cref="ConsoleMenuSetup"/> instance for fluent configuration.</returns>
        public ConsoleMenuSetup AddSubMenuOptionWithKey(int id, string value, string subMenuKey)
        {
            _options.Add(ConsoleMenuOption.CreateSubMenuWithKey(id, value, subMenuKey));
            return this;
        }

        /// <summary>
        /// Adds an option that returns from the current menu to the previous menu.
        /// </summary>
        /// <param name="id">The unique option identifier.</param>
        /// <param name="value">The text displayed for the option.</param>
        /// <returns>The current <see cref="ConsoleMenuSetup"/> instance for fluent configuration.</returns>
        public ConsoleMenuSetup AddReturnOption(int id, string value)
        {
            _options.Add(ConsoleMenuOption.CreateReturn(id, value));
            return this;
        }

        /// <summary>
        /// Adds an option that returns from the current submenu chain to the main menu.
        /// </summary>
        /// <param name="id">The unique option identifier.</param>
        /// <param name="value">The text displayed for the option.</param>
        /// <returns>The current <see cref="ConsoleMenuSetup"/> instance for fluent configuration.</returns>
        public ConsoleMenuSetup AddReturnToMainOption(int id, string value)
        {
            _options.Add(ConsoleMenuOption.CreateReturnToMain(id, value));
            return this;
        }

        /// <summary>
        /// Adds an option that exits the menu flow.
        /// </summary>
        /// <param name="id">The unique option identifier.</param>
        /// <param name="value">The text displayed for the option.</param>
        /// <returns>The current <see cref="ConsoleMenuSetup"/> instance for fluent configuration.</returns>
        public ConsoleMenuSetup AddExitOption(int id, string value)
        {
            _options.Add(ConsoleMenuOption.CreateExit(id, value));
            return this;
        }

        /// <summary>
        /// Runs the menu loop internally and returns the resulting menu execution state.
        /// </summary>
        /// <param name="isMainMenu">
        /// Indicates whether this menu is the root menu. When true, ReturnToMain results
        /// are handled by continuing this menu instead of propagating upward.
        /// </param>
        /// <returns>
        /// A task containing the execution result produced by the menu loop.
        /// </returns>
        internal async Task<ConsoleMenuExecutionResult> RunInternalAsync(bool isMainMenu = false)
        {
            while (true)
            {
                var selectedOption = _selector.ObtainOption(_options, _selectionType);
                var result = await _executor.ExecuteAsync(selectedOption);

                if (result == ConsoleMenuExecutionResult.Continue) continue;

                if (result == ConsoleMenuExecutionResult.ReturnToMain && isMainMenu)
                    continue;

                return result switch
                {
                    ConsoleMenuExecutionResult.Return =>
                        ConsoleMenuExecutionResult.Continue,

                    ConsoleMenuExecutionResult.Exit =>
                        result,

                    ConsoleMenuExecutionResult.ReturnToMain =>
                        ConsoleMenuExecutionResult.ReturnToMain,

                    _ => throw new InvalidOperationException(
                        $"Unsupported execution result: {result}")
                };
            }
        }

        /// <summary>
        /// Runs the menu synchronously.
        /// </summary>
        /// <remarks>
        /// This method blocks the current thread until the menu finishes. For asynchronous
        /// applications, prefer <see cref="RunAsync"/>.
        /// </remarks>
        public void Run() => RunAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Runs the menu asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous menu execution.</returns>
        public async Task RunAsync() => await RunInternalAsync(isMainMenu: true);
    }
}
