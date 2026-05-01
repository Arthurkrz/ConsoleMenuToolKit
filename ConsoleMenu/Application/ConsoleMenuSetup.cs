using ConsoleMenu.Contracts;
using ConsoleMenu.Enum;

namespace ConsoleMenu.Application
{
    /// <summary>
    /// Class for configuration of the console menu. This class serves as 
    /// the main entry point for setting up and running the console menu in 
    /// an application. The AddOption method has a different asynchronous 
    /// version. The Run method works asynchronously by default.
    /// </summary>
    public sealed class ConsoleMenuSetup
    {
        private readonly List<ConsoleMenuOption> _options = new();

        private readonly IConsoleMenuSelector _selector;
        private readonly IConsoleMenuExecutor _executor;

        private ConsoleMenuSelectionType _selectionType =
            ConsoleMenuSelectionType.ReadAfterConfirm;

        /// <summary>
        /// Default constructor for ConsoleMenuSetup that allows for quick 
        /// setup of a console menu with default behavior. It initializes 
        /// the menu selector and executor with default implementations that use a console wrapper.
        /// </summary>
        public ConsoleMenuSetup(ConsoleMenuSelectionType selectionType = ConsoleMenuSelectionType.ReadAfterConfirm) 
            : this(new ConsoleMenuSelector(new ConsoleMenuWrapper()), 
                   new ConsoleMenuExecutor([], new ConsoleMenuWrapper())) { }

        /// <summary>
        /// Constructor for ConsoleMenuSetup that allows for dependency injection 
        /// of the menu selector and executor. The provided selector and executor 
        /// are assigned to the internal fields, and the menu options can be 
        /// configured using the AddOption, AddHandlerOption, and AddExitOption 
        /// methods before running the menu with the Run method.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="executor"></param>
        public ConsoleMenuSetup(IConsoleMenuSelector selector, IConsoleMenuExecutor executor)
        {
            _selector = selector;
            _executor = executor;
        }

        public ConsoleMenuSetup UseSelectionType(ConsoleMenuSelectionType selectionType)
        {
            _selectionType = selectionType;
            return this;
        }

        /// <summary>
        /// Adds an option to the console menu with a simple action. This method 
        /// is used for simple configurations where the option directly executes 
        /// a provided Action delegate when selected. The option is added to the 
        /// internal list of options, and the method returns the ConsoleMenuSetup 
        /// instance to allow for fluent chaining of option additions.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public ConsoleMenuSetup AddOption(int id, string value, Action action)
        {
            _options.Add(ConsoleMenuOption.Create(id, value, action));
            return this;
        }

        /// <summary>
        /// Adds an option to the console menu with a Func of type Task with an 
        /// asynchronous action. This method is used for simple asynchronous 
        /// configurations where the option directly executes a provided
        /// Action delegate when selected. The option is added to the 
        /// internal list of options, and the method returns the 
        /// ConsoleMenuSetup instance to allow for fluent 
        /// chaining of option additions.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="asyncAction"></param>
        /// <returns></returns>
        public ConsoleMenuSetup AddOptionAsync(int id, string value, Func<Task> asyncAction)
        {
            _options.Add(ConsoleMenuOption.CreateAsync(id, value, asyncAction));
            return this;
        }

        /// <summary>
        /// Adds an option to the console menu that is linked to a handler. 
        /// This method is used for more complex configurations where the option
        /// does not directly execute an Action delegate but instead is associated 
        /// with a handler key. When this option is selected, the executor will 
        /// look up the corresponding handler based on the provided handler key and execute it. 
        /// The option is added to the internal list of options, and the method returns 
        /// the ConsoleMenuSetup instance to allow for fluent chaining of option additions.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="handlerKey"></param>
        /// <returns></returns>
        public ConsoleMenuSetup AddHandlerOption(int id, string value, string handlerKey)
        {
            _options.Add(ConsoleMenuOption.CreateWithHandler(id, value, handlerKey));
            return this;
        }

        /// <summary>
        /// Adds an exit option to the console menu. This method is used to add an option 
        /// that, when selected, will signal the menu to exit. The option is added to the 
        /// internal list of options, and the method returns the ConsoleMenuSetup instance 
        /// to allow for fluent chaining of option additions.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConsoleMenuSetup AddExitOption(int id, string value)
        {
            _options.Add(ConsoleMenuOption.CreateExit(id, value));
            return this;
        }

        /// <summary>
        /// Runs the console menu. This method enters a loop where it continuously prompts 
        /// the user to select an option from the menu, executes the selected option using 
        /// the executor, and checks the result of the execution. If the execution result 
        /// indicates that the menu should exit (e.g., when an exit option is selected), the 
        /// loop breaks and the method returns, effectively ending the menu interaction. 
        /// Otherwise, it continues to prompt the user for another selection. 
        /// This method relies on the selector to obtain valid user input and on the
        /// executor to handle the execution logic for each selected option.
        /// The method works asynchronously by default, although its name does 
        /// not include "Async" for simplicity in usage.
        /// </summary>
        public async Task Run()
        {
            while (true)
            {
                var selectedOption = _selector.ObtainOption(_options, _selectionType);
                var result = await _executor.ExecuteAsync(selectedOption);

                if (result == ConsoleMenuExecutionResult.Exit) break;
            }
        }
    }
}
