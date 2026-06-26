namespace ConsoleMenuToolKit.Contracts
{
    /// <summary>
    /// Base interface for user-created handler classes.
    /// </summary>
    public interface IConsoleMenuHandler
    {
        /// <summary>
        /// Method that will be called when the handler is executed. Implement this 
        /// method with the logic you want to run when the handler is triggered.
        /// The method runs asynchronously by default to allow for any async 
        /// operations that may be needed within the handler's logic.
        /// </summary>
        Task ExecuteAsync();

        /// <summary>
        /// A unique key for handler binding. When creating a menu option that 
        /// uses a handler, you will specify this key to indicate which handler 
        /// should be executed when that option is selected.
        /// </summary>
        string Key { get; }
    }
}
