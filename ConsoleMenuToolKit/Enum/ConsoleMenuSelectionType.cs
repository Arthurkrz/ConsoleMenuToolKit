namespace ConsoleMenuToolKit.Enum
{
    /// <summary>
    /// Defines the type of selection behavior for a console menu.
    /// </summary>
    public enum ConsoleMenuSelectionType
    {
        /// <summary>
        /// Indicates that the menu option should be read after the 
        /// user confirms their selection with the 'Enter' key.
        /// </summary>
        ReadAfterConfirm = 1,

        /// <summary>
        /// Indicates that the menu option should be read instantly as 
        /// the user types, without waiting for confirmation.
        /// </summary>
        InstantRead = 2,

        /// <summary>
        /// Indicates that the menu option should be selected using 
        /// arrow keys for navigation, allowing the user to move 
        /// through options and select one by pressing 'Enter'.
        /// </summary>
        ArrowSelection = 3
    }
}
