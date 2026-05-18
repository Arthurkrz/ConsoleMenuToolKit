using ConsoleMenu.Contracts;
using ConsoleMenu.Enum;

namespace ConsoleMenu.Application
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
        /// <param name="console"></param>
        public ConsoleMenuSelector(IConsoleMenuWrapper console)
        {
            _console = console;
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
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public ConsoleMenuOption ObtainOption(List<ConsoleMenuOption> options, ConsoleMenuSelectionType selectionType)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(options), "No options provided.");

            ValidateDuplicateIds(options);

            while (true)
            {
                var response = AskAndReadOption(selectionType, options);
                var selectedOption = MapToConsoleInputOption(response, options);

                if (selectedOption is not null)
                {
                    _console.Clear();
                    _console.WriteLineColored($"\"{selectedOption.Value}\" selected\n", ConsoleColor.Cyan);
                    return selectedOption;
                }

                _console.Clear();
                _console.WriteLineColored($"\"{response}\" is not a valid option.\n", ConsoleColor.Red);
                ShowOptions(options, selectionType);
            }
        }

        private ConsoleMenuOption? MapToConsoleInputOption(string userInput, List<ConsoleMenuOption> options)
        {
            if (!int.TryParse(userInput, out var inputAsNumber))
                return null;

            return options.FirstOrDefault(x => x.Id == inputAsNumber);
        }

        private string AskAndReadOption(ConsoleMenuSelectionType selectionType, List<ConsoleMenuOption> options)
        {
            _console.WriteLine("Select an option:");

            return selectionType switch
            {
                ConsoleMenuSelectionType.InstantRead => ReadInstantly(),
                ConsoleMenuSelectionType.ReadAfterConfirm => ReadAfterConfirm(),
                ConsoleMenuSelectionType.ArrowSelection => ReadByArrowSelection(options),

                _ => throw new NotSupportedException(
                    $"Selection type '{selectionType}' is not supported.")
            };
        }

        private void ShowOptions(List<ConsoleMenuOption> options, ConsoleMenuSelectionType selectionType, int? selectedIndex = 0)
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

            foreach (var option in options)
                _console.WriteLine($"[{option.Id}] - {option.Value}");
        }

        private static void ValidateDuplicateIds(List<ConsoleMenuOption> options)
        {
            var duplicateIds = options
                .GroupBy(x => x.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateIds.Any())
                throw new InvalidOperationException($"Duplicate option IDs found: {string.Join(", ", duplicateIds)}");
        }

        private string ReadInstantly()
        {
            var key = _console.ReadKey();
            _console.WriteLine("");
            return key.KeyChar.ToString();
        }

        private string ReadAfterConfirm() =>
            _console.ReadLine() ?? string.Empty;

        private string ReadByArrowSelection(List<ConsoleMenuOption> options)
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
                        selectedIndex = selectedIndex ==
                            options.Count - 1 ? 0
                            : selectedIndex + 1;
                        break;

                    case ConsoleKey.Enter:
                        return options[selectedIndex].Id.ToString();
                }
            }
        }
    }
}
