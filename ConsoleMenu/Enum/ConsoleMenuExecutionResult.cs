namespace ConsoleMenu.Enum
{
    /// <summary>
    /// Represents the result of a menu option, indicating 
    /// whether to continue displaying the menu or exit it.
    /// </summary>
    public enum ConsoleMenuExecutionResult
    {
        /// <summary>
        /// Indicates that the menu loop should continue, 
        /// allowing the user to select another option 
        /// or perform additional actions.
        /// </summary>
        Continue = 1,

        Return = 2,

        /// <summary>
        /// Indicates that the menu should be exited, 
        /// terminating the menu loop with the application 
        /// proceeding to any subsequent code after the menu execution.
        /// </summary>
        Exit = 3
    }
}
