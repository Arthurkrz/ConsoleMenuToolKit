namespace ConsoleMenu.Enum
{
    /// <summary>
    /// Defines the type of a console menu option, 
    /// which can be an action, a handler, or an exit command.
    /// </summary>
    public enum ConsoleMenuOptionKind
    {
        /// <summary>
        /// Console menu option for simple Action delegate
        /// </summary>
        Action = 1,

        /// <summary>
        /// Console menu option for handler execution, 
        /// which requires a registered handler key to 
        /// execute the corresponding handler logic.
        /// </summary>
        Handler = 2,

        SubMenu = 3,

        Return = 4,

        /// <summary>
        /// Console menu option that signals the menu 
        /// to exit when selected, terminating the menu loop.
        /// </summary>
        Exit = 5
    }
}
