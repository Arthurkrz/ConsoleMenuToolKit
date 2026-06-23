using ConsoleMenu.Contracts;

namespace ConsoleMenu.Application
{
    /// <summary>
    /// A wrapper around the Console class to abstract away 
    /// direct interactions with the console and allow customizations.
    /// </summary>
    public class ConsoleMenuWrapper : IConsoleMenuWrapper
    {
        /// <summary>
        /// Gets the current top position of the cursor in the console.
        /// </summary>
        public int CursorTop => Console.CursorTop;

        /// <summary>
        /// Reads a key press from the console and 
        /// returns the corresponding ConsoleKeyInfo.
        /// </summary>
        /// <returns>The read key.</returns>
        public ConsoleKeyInfo ReadKey() => Console.ReadKey();

        /// <summary>
        /// Clears the console. This method can be implemented to 
        /// provide a custom clearing behavior, such as adding a transition 
        /// effect or logging the cleared content before clearing the console.
        /// </summary>
        public void Clear() => Console.Clear();

        /// <summary>
        /// Reads a line of input from the console and returns it as a string.
        /// </summary>
        /// <returns>The read line or an empty line.</returns>
        public string ReadLine() => Console.ReadLine() ?? string.Empty;

        /// <summary>
        /// Writes a message prompting the user to 
        /// press any key to continue, and waits for a key press.
        /// </summary>
        public void ContinueAfterInput()
        {
            WriteLineColored("\nPress any key to continue!", ConsoleColor.Green);
            ReadKey();
        }

        /// <summary>
        /// Writes a new line in the console.
        /// </summary>
        /// <param name="value">Line to be written.</param>
        public void WriteLine(string value = null!) => Console.WriteLine(value);

        /// <summary>
        /// Writes a new line in the console with the specified color.
        /// </summary>
        /// <param name="value">Line to be written.</param>
        /// <param name="color">Color of the written line.</param>
        public void WriteLineColored(string value, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ForegroundColor = previousColor;
        }

        /// <summary>
        /// Clears the current line in the console by overwriting it with 
        /// spaces and resetting the cursor position to the start of the line.
        /// </summary>
        public void ClearCurrentLine()
        {
            SetCursorPosition(0, CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            SetCursorPosition(0, CursorTop);
        }

        /// <summary>
        /// Sets the cursor position in the console 
        /// to the specified left and top coordinates.
        /// </summary>
        /// <param name="left">Left coordinates.</param>
        /// <param name="top">Top coordinates.</param>
        public void SetCursorPosition(int left, int top) =>
            Console.SetCursorPosition(left, top);
    }
}
