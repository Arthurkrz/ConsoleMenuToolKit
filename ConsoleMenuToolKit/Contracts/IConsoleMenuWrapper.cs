namespace ConsoleMenuToolKit.Contracts
{
    /// <summary>
    /// Interface to abstract direct console 
    /// operations, allowing specific customizations.
    /// </summary>
    public interface IConsoleMenuWrapper
    {
        /// <summary>
        /// Gets the current top position of the cursor in the console.
        /// </summary>
        int CursorTop { get; }

        /// <summary>
        /// Clears the current line in the console.
        /// </summary>
        void ClearCurrentLine();

        /// <summary>
        /// Sets the cursor position in the console 
        /// to the specified left and top coordinates.
        /// </summary>
        /// <param name="left">Left coordinates.</param>
        /// <param name="top">Top coordinates.</param>
        void SetCursorPosition(int left, int top);

        /// <summary>
        /// Reads a key press from the console and 
        /// returns the corresponding ConsoleKeyInfo.
        /// </summary>
        /// <returns>The pressed key.</returns>
        ConsoleKeyInfo ReadKey();

        /// <summary>
        /// Clears the console. This method can be 
        /// implemented to provide a custom clearing 
        /// behavior, such as adding a transition 
        /// effect or logging the cleared 
        /// content before clearing the console.
        /// </summary>
        void Clear();

        /// <summary>
        /// Reads a line of input from the 
        /// console and returns it as a string.
        /// </summary>
        /// <returns>The read string.</returns>
        string ReadLine();

        /// <summary>
        /// Writes a message prompting the user 
        /// to press any key to continue, 
        /// and waits for a key press.
        /// </summary>
        void ContinueAfterInput();

        /// <summary>
        /// Writes a new line in the console.
        /// </summary>
        /// <param name="value">Value to be written.</param>
        void WriteLine(string value = null!);

        /// <summary>
        /// Writes a new line in the console with the specified color.
        /// </summary>
        /// <param name="value">Value to be written.</param>
        /// <param name="color">Color of the value to be written.</param>
        void WriteLineColored(string value, ConsoleColor color);
    }
}