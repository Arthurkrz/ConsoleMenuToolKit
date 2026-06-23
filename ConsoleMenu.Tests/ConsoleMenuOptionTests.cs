using ConsoleMenu.Application;

namespace ConsoleMenu.Tests
{
    public class ConsoleMenuOptionTests
    {
        [Fact]
        public void Create_ShouldThrowException_WhenNullAction()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                ConsoleMenuOption.Create(0, "Invalid", null!));
        }

        [Fact]
        public void CreateAsync_ShouldThrowException_WhenNullAction()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                ConsoleMenuOption.Create(0, "Invalid", null!));
        }

        [Fact]
        public void CreateWithHandler_ShouldThrowException_WhenEmptyKey()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                ConsoleMenuOption.CreateWithHandler(1, "Test", ""));
        }

        [Fact]
        public void CreateSubMenu_ShouldThrowException_WhenNullSubMenu()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                ConsoleMenuOption.CreateSubMenu(0, "Invalid", null!));
        }

        [Fact]
        public void CreateSubMenuWithKey_ShouldThrowException_WhenEmptyKey()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                ConsoleMenuOption.CreateSubMenuWithKey(1, "Test", ""));
        }
    }
}
