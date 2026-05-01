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
        /// Reads a key press from the console and 
        /// returns the corresponding ConsoleKeyInfo.
        /// </summary>
        /// <returns></returns>
        public ConsoleKeyInfo ReadKey() => Console.ReadKey();

        /// <summary>
        /// Clears the console. This method can be implemented to 
        /// provide a custom clearing behavior, such as adding a transition 
        /// effect or logging the cleared content before clearing the console.
        /// </summary>
        public void Clear() => Console.Clear();

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
        /// <param name="value"></param>
        public void WriteLine(string value = null!) => Console.WriteLine(value);

        /// <summary>
        /// Writes a new line in the console with the specified color.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        public void WriteLineColored(string value, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }
    }
}
