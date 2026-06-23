using ConsoleMenu.Application;
using ConsoleMenu.Enum;

namespace ConsoleMenu.Contracts
{
    /// <summary>
    /// Interface for obtaining a menu option from the user. The implementation of this 
    /// interface is responsible for displaying the options to the user, reading their selection, 
    /// and validating it against the provided options. If the user input is invalid, it should 
    /// prompt the user to try again until a valid option is selected.
    /// </summary>
    public interface IConsoleMenuSelector
    {
        /// <summary>
        /// Obtains a menu option from the user. The method should display the provided options, 
        /// read the user's selection, and return the corresponding ConsoleMenuOption. 
        /// If the user input is invalid (e.g., not a number or not matching any option ID), 
        /// it should prompt the user to try again until a valid option is selected.
        /// </summary>
        /// <param name="options">List of available options provided by the user.</param>
        /// <param name="selectionType">Defines how the user should select the options.</param>
        /// <returns>The ConsoleMenuOption instance associated with the chosen option.</returns>
        ConsoleMenuOption ObtainOption(List<ConsoleMenuOption> options, ConsoleMenuSelectionType selectionType);
    }
}