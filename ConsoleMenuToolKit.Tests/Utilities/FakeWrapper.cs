using ConsoleMenuToolKit.Contracts;

namespace ConsoleMenuToolKit.Tests.Utilities
{
    public class FakeWrapper : IConsoleMenuWrapper
    {
        public bool SkipPause { get; set; } = true;

        private readonly Queue<ConsoleKeyInfo> _keys = new();
        private readonly Queue<string> _lines = new();

        public List<string> WrittenLines { get; } = new();
        public List<(string Message, ConsoleColor Color)> ColoredLines { get; } = new();

        public void WriteLine(string value = null!) => WrittenLines.Add(value ?? string.Empty);

        public void WriteLineColored(string value, ConsoleColor color) => ColoredLines.Add((value, color));

        public void EnqueueKey(char key) => _keys.Enqueue(new ConsoleKeyInfo(key, (ConsoleKey)char.ToUpperInvariant(key), false, false, false));

        public void EnqueueLine(string input) => _lines.Enqueue(input);

        public void ContinueAfterInput()
        {
            if (SkipPause) return;

            ColoredLines.Add(("\nPress any key to continue!", ConsoleColor.Green));
            ReadKey();
        }

        public ConsoleKeyInfo ReadKey()
        {
            if (_keys.Count == 0)
                throw new InvalidOperationException("No input provided");

            return _keys.Dequeue();
        }

        public string ReadLine()
        {
            if (_lines.Count == 0)
                throw new InvalidOperationException("No input provided.");

            return _lines.Dequeue();
        }

        public int CursorTop => 0;

        public void ClearCurrentLine() { }

        public void SetCursorPosition(int left, int top) { }

        public void Clear() { }
    }
}
