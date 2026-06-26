using ConsoleMenuToolKit.Contracts;
using ConsoleMenuToolKit.Enum;

namespace ConsoleMenuToolKit.Application
{
    /// <summary>
    /// Class responsible for obtaining a menu option from the user. It implements 
    /// the IConsoleMenuSelector interface and uses an IConsoleWrapper to interact with the console. 
    /// The class validates the provided options, displays them to the user, and 
    /// handles user input to ensure a valid selection is made. If the user input is 
    /// invalid, it prompts the user to try again until a valid option is selected. 
    /// This class is essential for facilitating user interaction with the console menu system.
    /// </summary>
    public class ConsoleMenuSelector : IConsoleMenuSelector
    {
        private readonly IConsoleMenuWrapper _console;

        /// <summary>
        /// Constructor for ConsoleMenuSelector, which takes an IConsoleWrapper as a dependency. 
        /// This allows the selector to perform console operations such as reading user input and 
        /// writing output. The IConsoleWrapper abstraction enables flexibility in how console 
        /// interactions are implemented, allowing for custom behavior or testing without relying on direct console calls.
        /// </summary>
        /// <param name="console">ConsoleWrapper instance to perform console operations.</param>
        public ConsoleMenuSelector(IConsoleMenuWrapper console)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
        }

        /// <summary>
        /// Obtains a menu option from the user. The method first validates that the 
        /// provided options are not null and contain at least one option. It then 
        /// checks for duplicate option IDs to ensure that each option can be uniquely identified. 
        /// The options are displayed to the user, and the method enters a loop where 
        /// it prompts the user to select an option by entering its ID. The user's input 
        /// is validated against the available options, and if a valid selection is made, 
        /// the corresponding ConsoleMenuOption is returned. If the input is invalid, 
        /// an error message is displayed, and the options are shown again until a valid selection is made.
        /// </summary>
        /// <param name="options">List of options provided by the user.</param>
        /// <param name="selectionType">Defines how the user should select the options.</param>
        /// <returns>The ConsoleMenuOption instance associated with the chosen option.</returns>
        public ConsoleMenuOption ObtainOption(IReadOnlyList<ConsoleMenuOption> options, ConsoleMenuSelectionType selectionType)
        {
            ValidateOptions(options);

            return selectionType == ConsoleMenuSelectionType.ArrowSelection 
                ? ObtainOptionByArrowSelection(options)
                : ObtainOptionByInput(options, selectionType);
        }

        private ConsoleMenuOption ObtainOptionByInput(IReadOnlyList<ConsoleMenuOption> options, ConsoleMenuSelectionType selectionType)
        {
            ShowOptions(options, selectionType);
            var selectedOption = AskAndMapOption(selectionType, options);

            _console.Clear();
            _console.WriteLineColored($"\"{selectedOption.Value}\" selected.\n", ConsoleColor.Cyan);
            return selectedOption;
        }

        private ConsoleMenuOption ObtainOptionByArrowSelection(IReadOnlyList<ConsoleMenuOption> options)
        {
            var selectedIndex = 0;
            var menuTop = _console.CursorTop;

            while (true)
            {
                _console.SetCursorPosition(0, menuTop);
                ShowOptions(options, ConsoleMenuSelectionType.ArrowSelection, selectedIndex);

                var key = _console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = selectedIndex == 0
                            ? options.Count - 1
                            : selectedIndex - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex = selectedIndex == options.Count - 1
                            ? 0
                            : selectedIndex + 1;
                        break;

                    case ConsoleKey.Enter:
                        var selectedOption = options[selectedIndex];

                        _console.Clear();
                        _console.WriteLineColored($"\"{selectedOption.Value}\" selected.\n", ConsoleColor.Cyan);
                        return selectedOption;
                }
            }
        }

        private ConsoleMenuOption AskAndMapOption(ConsoleMenuSelectionType selectionType, IReadOnlyList<ConsoleMenuOption> options)
        {
            while (true)
            {
                _console.WriteLine("\nSelect an option:");

                var input = selectionType switch
                {
                    ConsoleMenuSelectionType.InstantRead => 
                        _console.ReadKey().KeyChar.ToString(),

                    ConsoleMenuSelectionType.ReadAfterConfirm => 
                        _console.ReadLine() ?? string.Empty,

                    _ => throw new NotSupportedException(
                        $"Selection type '{selectionType}' is not supported.")
                };

                if (selectionType == ConsoleMenuSelectionType.InstantRead) _console.WriteLine("");

                if (!int.TryParse(input, out var inputAsNumber))
                {
                    _console.Clear();
                    _console.WriteLineColored($"\"{input}\" is not a valid option.\n", ConsoleColor.Red);
                    ShowOptions(options, selectionType);
                    continue;
                }

                var selectedOption = options.FirstOrDefault(x => x.Id == inputAsNumber);

                if (selectedOption is not null) return selectedOption;

                _console.Clear();
                _console.WriteLineColored($"\"{input}\" is not a valid option.\n", ConsoleColor.Red);
                ShowOptions(options, selectionType);
            }
        }

        private void ShowOptions(IReadOnlyList<ConsoleMenuOption> options, ConsoleMenuSelectionType selectionType, int selectedIndex = 0)
        {
            if (selectionType == ConsoleMenuSelectionType.ArrowSelection)
            {
                _console.WriteLine("Use ↑/↓ to move and Enter to select:\n");

                for (int i = 0; i < options.Count; i++)
                {
                    _console.ClearCurrentLine();

                    var prefix = i == selectedIndex ? ">> " : "   ";
                    _console.WriteLine($"{prefix}{options[i].Id} - {options[i].Value}");
                }

                return;
            }

            foreach (var option in options) _console.WriteLine(
                $"[{option.Id}] - {option.Value}");
        }

        private static void ValidateOptions(IReadOnlyList<ConsoleMenuOption> options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            if (options.Count == 0) throw new InvalidOperationException("No options provided.");

            var duplicateIds = options
                .GroupBy(x => x.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateIds.Count != 0)
                throw new InvalidOperationException($"Duplicate option IDs found: {string.Join(", ", duplicateIds)}");
        }
    }
}
