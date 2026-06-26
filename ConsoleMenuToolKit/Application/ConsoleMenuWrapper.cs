using ConsoleMenuToolKit.Contracts;

namespace ConsoleMenuToolKit.Application
{
    /// <summary>
    /// Default console wrapper used by the menu system to isolate direct
    /// <see cref="Console"/> access behind an abstraction.
    /// </summary>
    /// <remarks>
    /// Some methods require an interactive console and may throw an
    /// <see cref="InvalidOperationException"/> when input or output is redirected.
    /// </remarks>
    public class ConsoleMenuWrapper : IConsoleMenuWrapper
    {
        /// <summary>
        /// Gets the current top position of the cursor in the console.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when console output is redirected.
        /// </exception>
        public int CursorTop
        {
            get
            {
                if (Console.IsOutputRedirected)
                    throw new InvalidOperationException("CursorTop requires an interactive console. Output is redirected.");

                return Console.CursorTop;
            }
        }

        /// <summary>
        /// Reads a key press from the console without displaying it.
        /// </summary>
        /// <returns>The key information for the pressed key.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when console input is redirected.
        /// </exception>
        public ConsoleKeyInfo ReadKey()
        {
            if (Console.IsInputRedirected)
                throw new InvalidOperationException("ReadKey requires an interactive console. Input is redirected.");

            return Console.ReadKey(intercept: true);
        }

        /// <summary>
        /// Clears the console screen.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when console output is redirected.
        /// </exception>
        public void Clear()
        {
            if (Console.IsOutputRedirected)
                throw new InvalidOperationException("Clear requires an interactive console. Output is redirected.");

            Console.Clear();
        }

        /// <summary>
        /// Reads a line of input from the console.
        /// </summary>
        /// <returns>The read line, or an empty string when no line is available.</returns>
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
        /// Writes a line of text to the console using the specified foreground color.
        /// The previous foreground color is restored after writing.
        /// </summary>
        /// <param name="value">The text to write.</param>
        /// <param name="color">The foreground color used while writing.</param>
        public void WriteLineColored(string value, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;

            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(value);
            }
            finally
            {
                Console.ForegroundColor = previousColor;
            }
        }

        /// <summary>
        /// Clears the current console line and resets the cursor to the beginning of that line.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when console output is redirected.
        /// </exception>
        public void ClearCurrentLine()
        {
            if (Console.IsOutputRedirected)
                throw new InvalidOperationException("ClearCurrentLine requires an interactive console. Output is redirected.");

            var currentTop = CursorTop;
            var width = Math.Max(0, Console.WindowWidth - 1);

            SetCursorPosition(0, currentTop);
            Console.Write(new string(' ', width));
            SetCursorPosition(0, currentTop);
        }

        /// <summary>
        /// Sets the cursor position in the console.
        /// </summary>
        /// <param name="left">The column position.</param>
        /// <param name="top">The row position.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when console output is redirected.
        /// </exception>
        public void SetCursorPosition(int left, int top)
        {
            if (Console.IsOutputRedirected)
                throw new InvalidOperationException("SetCursorPosition requires an interactive console. Output is redirected.");

            Console.SetCursorPosition(left, top);
        }
    }
}
