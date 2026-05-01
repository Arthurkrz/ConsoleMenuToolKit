using ConsoleMenu.Application;
using ConsoleMenu.Enum;

namespace ConsoleMenu.Contracts
{
    /// <summary>
    /// Interface for executing a selected console menu option. Implementations of this 
    /// interface will handle the logic for executing different types of menu options 
    /// based on their kind (Action, Handler, or Exit) and will manage any necessary 
    /// interactions with the console or other components of the application.
    /// The implemented method runs asynchronously, but can also be used for synchronous actions.
    /// </summary>
    public interface IConsoleMenuExecutor
    {
        /// <summary>
        /// Executes the provided console menu option based on its kind. For Action options, 
        /// it will invoke the associated action delegate. For Handler options, it will 
        /// look up and execute the corresponding handler based on the HandlerKey. 
        /// For Exit options, it will return a result indicating that the menu should be exited. 
        /// The method also handles any customizations in the console such as waiting for 
        /// user input and clearing the console after execution.
        /// This method runs asynchronously, but can also be used for synchronous actions.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Task<ConsoleMenuExecutionResult> ExecuteAsync(ConsoleMenuOption option);
    }
}