namespace ConsoleMenu.Enum
{
    /// <summary>
    /// Defines the type of a console menu option, 
    /// which can be an action, a handler, a sub-menu, 
    /// a return command or an exit command.
    /// </summary>
    public enum ConsoleMenuOptionKind
    {
        /// <summary>
        /// Console menu option for simple Action delegate.
        /// </summary>
        Action = 1,

        /// <summary>
        /// Console menu option for handler execution, 
        /// which requires a registered handler key to 
        /// execute the corresponding handler logic.
        /// </summary>
        Handler = 2,

        /// <summary>
        /// Console menu option for sub-menu execution, 
        /// which may require a registered sub-menu key 
        /// or be given directly as an argument 
        /// of type ConsoleMenuOption.
        /// </summary>
        SubMenu = 3,

        /// <summary>
        /// Console menu option that signals the menu 
        /// to return to the previous sub-menu.
        /// </summary>
        Return = 4,

        /// <summary>
        /// Console menu option that signals the menu
        /// to return the main menu.
        /// </summary>
        ReturnToMain = 5,

        /// <summary>
        /// Console menu option that signals the menu 
        /// to exit when selected, terminating the menu loop.
        /// </summary>
        Exit = 6
    }
}
