using ConsoleMenuToolKit.Application;

namespace ConsoleMenuToolKit.Contracts
{
    /// <summary>
    /// Base interface for sub-menus, allowing the 
    /// implementations of nested menus in the console.
    /// </summary>
    public interface IConsoleMenuSubMenu
    {
        /// <summary>
        /// Builds and returns the ConsoleMenuSetup 
        /// instance associated with this submenu provider.
        /// </summary>
        /// <returns>ConsoleMenuSetup instance with the 
        /// necessary RunInternalAsync() method, not 
        /// present for IConsoleMenuSubMenu instances.</returns>
        ConsoleMenuSetup Build();

        /// <summary>
        /// A unique key for sub-menu binding. 
        /// When creating a menu option that uses 
        /// a sub-menu, you will specify this key 
        /// to indicate which sub-menu option 
        /// should be executed when 
        /// that option is selected.
        /// </summary>
        string Key { get; }
    }
}
