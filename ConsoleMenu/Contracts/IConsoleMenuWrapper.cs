namespace ConsoleMenu.Contracts
{
    /// <summary>
    /// Interface to abstract direct console operations, allowing specific customizations.
    /// </summary>
    public interface IConsoleMenuWrapper
    {
        /// <summary>
        /// Reads a key press from the console and returns the corresponding ConsoleKeyInfo.
        /// </summary>
        /// <returns></returns>
        ConsoleKeyInfo ReadKey();

        /// <summary>
        /// Clears the console. This method can be implemented to provide a custom clearing behavior, 
        /// such as adding a transition effect or logging the cleared content before clearing the console.
        /// </summary>
        void Clear();

        string ReadLine();

        /// <summary>
        /// Writes a message prompting the user to press any key to continue, and waits for a key press.
        /// </summary>
        void ContinueAfterInput();

        /// <summary>
        /// Writes a new line in the console.
        /// </summary>
        /// <param name="value"></param>
        void WriteLine(string value = null!);

        /// <summary>
        /// Writes a new line in the console with the specified color.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        void WriteLineColored(string value, ConsoleColor color);
    }
}