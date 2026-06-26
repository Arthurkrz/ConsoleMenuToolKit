namespace ConsoleMenuToolKit.Enum
{
    /// <summary>
    /// Represents the result of a menu option, indicating 
    /// whether to continue displaying the menu, return 
    /// to a previous sub-menu, return to
    /// the main menu or exit it.
    /// </summary>
    public enum ConsoleMenuExecutionResult
    {
        /// <summary>
        /// Indicates that the menu loop should continue, 
        /// allowing the user to select another option 
        /// or perform additional actions.
        /// </summary>
        Continue = 1,

        /// <summary>
        /// Indicates that the menu should return 
        /// to the previous sub-menu.
        /// </summary>
        Return = 2,

        /// <summary>
        /// Indicates that the menu should 
        /// return to the main menu.
        /// </summary>
        ReturnToMain = 3,

        /// <summary>
        /// Indicates that the menu should be exited, 
        /// terminating the menu loop with the application 
        /// proceeding to any subsequent code after the menu execution.
        /// </summary>
        Exit = 4
    }
}
