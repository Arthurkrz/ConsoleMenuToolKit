using ConsoleMenuToolKit.Application;
using ConsoleMenuToolKit.Enum;

namespace ConsoleMenuToolKit.Contracts
{
    /// <summary>
    /// Interface for executing a selected console menu option. Implementations of this 
    /// interface will handle the logic for executing different types of menu options 
    /// based on their kind (Action, Handler, SubMenu, Return, ReturnToMain or Exit) 
    /// and will manage any necessary interactions with the console or other 
    /// components of the application. The implemented method runs asynchronously, 
    /// but can also be used for synchronous actions.
    /// </summary>
    public interface IConsoleMenuExecutor
    {
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
        Task<ConsoleMenuExecutionResult> ExecuteAsync(ConsoleMenuOption option);
    }
}