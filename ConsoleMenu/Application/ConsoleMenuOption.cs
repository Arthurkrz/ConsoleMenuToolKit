using ConsoleMenu.Enum;

namespace ConsoleMenu.Application
{
    /// <summary>
    /// Class representing an option in the console menu. It contains properties for the option's ID, 
    /// display value, kind (Action, Handler, SubMenu, Return, ReturnToMain or Exit), and optional 
    /// action, sub-menu, handler key or sub-menu key depending on the kind. The class includes 
    /// static factory methods for creating options of different kinds, ensuring 
    /// that the necessary properties are set correctly based on the option type.
    /// </summary>
    public sealed class ConsoleMenuOption
    {
        private ConsoleMenuOption(int id, string value, ConsoleMenuOptionKind kind, Func<Task>? asyncAction = null, string? handlerKey = null, ConsoleMenuSetup? subMenu = null, string? subMenuKey = null)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, "ID must be greater than zero.");
            ArgumentException.ThrowIfNullOrWhiteSpace(value, "Value cannot be empty.");

            Id = id;
            Value = value;
            Kind = kind;
            AsyncAction = asyncAction;
            HandlerKey = handlerKey;
            SubMenu = subMenu;
            SubMenuKey = subMenuKey;
        }

        /// <summary>
        /// Unique identifier for the menu option. Must be greater than zero.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Name of the menu option.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Enum value for identifying the type of the menu option, which can be Action, 
        /// Handler, or Exit. This determines how the option will be executed when selected.
        /// </summary>
        public ConsoleMenuOptionKind Kind { get; }

        /// <summary>
        /// Action delegate for simple configuration. Must be provided when Kind is Action, 
        /// and should be null when Kind is Handler or Exit.
        /// </summary>
        public Func<Task>? AsyncAction { get; }

        /// <summary>
        /// Key for handler binding, used when the option kind is Handler. 
        /// Cannot be empty or whitespace when Kind is Handler.
        /// </summary>
        public string? HandlerKey { get; }

        /// <summary>
        /// Sub-menu for nested menu options, used when the user creates 
        /// a sub-menu manually with its associated sub-menus.
        /// </summary>
        public ConsoleMenuSetup? SubMenu { get; }

        /// <summary>
        /// Key for sub-menu binding, used when the user 
        /// chooses to not create sub-menus manually.
        /// </summary>
        public string? SubMenuKey { get; }

        /// <summary>
        /// Creates a menu option of kind Action, used for simple configuration. 
        /// The HandlerKey is set to null for this kind, and an Action delegate must be provided.
        /// </summary>
        /// <param name="id">Index of the option in console.</param>
        /// <param name="value">Label of the option in console.</param>
        /// <param name="action">Simple action to be performed through a function.</param>
        /// <returns>The option with an Action configured.</returns>
        /// <exception cref="ArgumentNullException">Thrown when 
        /// <paramref name="action"/> is null.</exception>
        public static ConsoleMenuOption Create(int id, string value, Action action)
        {
            ArgumentNullException.ThrowIfNull(action);

            return new ConsoleMenuOption(id, value, ConsoleMenuOptionKind.Action, () =>
            {
                action();
                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// Creates a menu option of kind Action, used for simple asynchoronus configuration. 
        /// The HandlerKey is set to null for this kind, and a Func of type Task with 
        /// an Action delegate must be provided.
        /// </summary>
        /// <param name="id">Index of the option in console.</param>
        /// <param name="value">Label of the option in console.</param>
        /// <param name="action">Asynchoronous action to be executed.</param>
        /// <returns>The option with an asynchronous action configured.</returns>
        /// <exception cref="ArgumentNullException">Thrown when 
        /// <paramref name="action"/> is null.</exception>
        public static ConsoleMenuOption CreateAsync(int id, string value, Func<Task> action)
        {
            ArgumentNullException.ThrowIfNull(action);

            return new ConsoleMenuOption(id, value, ConsoleMenuOptionKind.Action, action);
        }

        /// <summary>
        /// Creates a menu option of kind Handler, used for complex configuration. 
        /// The Action is set to null for this kind, and the HandlerKey must be
        /// provided to link the option to a handler in the executor.
        /// </summary>
        /// <param name="id">Index of the option in console.</param>
        /// <param name="value">Label of the option in console.</param>
        /// <param name="handlerKey">Key to link the option to the action.</param>
        /// <returns>The option with a handler configured.</returns>
        /// <exception cref="ArgumentException">Thrown when 
        /// <paramref name="handlerKey"/> is null or empty.</exception>
        public static ConsoleMenuOption CreateWithHandler(int id, string value, string handlerKey)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(handlerKey, "Handler key cannot be empty.");

            return new ConsoleMenuOption(id, value, ConsoleMenuOptionKind.Handler, null, handlerKey);
        }

        /// <summary>
        /// Creates a sub-menu option with its internal options and nested menus.
        /// </summary>
        /// <param name="id">Index of the option in console.</param>
        /// <param name="value">Label of the option in console.</param>
        /// <param name="subMenu">Menu where this sub-menu is located.</param>
        /// <returns>The 'sub-menu' option.</returns>
        /// <exception cref="ArgumentNullException">Thrown when 
        /// <paramref name="subMenu"/> null.</exception>
        public static ConsoleMenuOption CreateSubMenu(int id, string value, ConsoleMenuSetup subMenu)
        {
            ArgumentNullException.ThrowIfNull(subMenu, "SubMenu cannot be null.");

            return new ConsoleMenuOption(id, value, ConsoleMenuOptionKind.SubMenu, subMenu: subMenu);
        }

        /// <summary>
        /// Creates a menu option of kind SubMenu using a key, allowing for a simple 
        /// creation instead of manually creating sub-menus with their nested 
        /// sub-menus in a specific order.
        /// </summary>
        /// <param name="id">Index of the option in console.</param>
        /// <param name="value">Label of the option in console.</param>
        /// <param name="subMenuKey">Key to link the option to the sub-menu.</param>
        /// <returns>The 'sub-menu' option.</returns>
        /// <exception cref="ArgumentException">Thrown when 
        /// <paramref name="subMenuKey"/> is null or empty.</exception>
        public static ConsoleMenuOption CreateSubMenuWithKey(int id, string value, string subMenuKey)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(subMenuKey, "SubMenu key cannot be empty.");

            return new ConsoleMenuOption(id, value, ConsoleMenuOptionKind.SubMenu, subMenuKey: subMenuKey);
        }

        /// <summary>
        /// Creates an option to return to the previous sub-menu.
        /// </summary>
        /// <param name="id">Index of the option in console.</param>
        /// <param name="value">Label of the option in console.</param>
        /// <returns>The 'return' option.</returns>
        public static ConsoleMenuOption CreateReturn(int id, string value) =>
            new ConsoleMenuOption(id, value, ConsoleMenuOptionKind.Return);

        /// <summary>
        /// Creates an option to return to the main menu.
        /// </summary>
        /// <param name="id">Index of the option in console.</param>
        /// <param name="value">Label of the option in console.</param>
        /// <returns>The 'return to main menu' option.</returns>
        public static ConsoleMenuOption CreateReturnToMain(int id, string value) =>
            new ConsoleMenuOption(id, value, ConsoleMenuOptionKind.ReturnToMain);

        /// <summary>
        /// Creates an exit option, setting the kind to Exit and leaving action and handlerKey as null.
        /// </summary>
        /// <param name="id">Index of the option in console.</param>
        /// <param name="value">Label of the option in console.</param>
        /// <returns>The 'exit' option.</returns>
        public static ConsoleMenuOption CreateExit(int id, string value) =>
            new ConsoleMenuOption(id, value, ConsoleMenuOptionKind.Exit);
    }
}
