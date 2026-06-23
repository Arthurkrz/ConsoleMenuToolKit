using ConsoleMenu.Application;
using ConsoleMenu.Enum;
using ConsoleMenu.Tests.Utilities;

namespace ConsoleMenu.Tests
{
    public class ConsoleMenuSelectorTests
    {
        private readonly FakeWrapper _console = new();
        private readonly ConsoleMenuSelector _sut;

        public ConsoleMenuSelectorTests()
        {
            _sut = new ConsoleMenuSelector(_console);
        }

        [Fact]
        public void ObtainOption_ShouldReturnCorrectOption_WithInstantRead()
        {
            // Arrange
            _console.EnqueueKey('2');

            var options = new List<ConsoleMenuOption>
            {
                ConsoleMenuOption.Create(1, "Test", () => { }),
                ConsoleMenuOption.Create(2, "Test", () => { })
            };

            // Act
            var result = _sut.ObtainOption(options, ConsoleMenuSelectionType.InstantRead);

            // Assert
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public void ObtainOption_ShouldReturnCorrectOption_WithReadAfterConfirm()
        {
            // Arrange
            _console.EnqueueLine("10");

            var options = new List<ConsoleMenuOption>
            {
                ConsoleMenuOption.Create(1, "Test", () => { }),
                ConsoleMenuOption.Create(10, "Test", () => { })
            };

            // Act
            var result = _sut.ObtainOption(options,
                ConsoleMenuSelectionType.ReadAfterConfirm);

            // Assert
            Assert.Equal(10, result.Id);
            Assert.Equal("Test", result.Value);
        }

        [Fact]
        public void ObtainOption_ShouldAllowRetry_WithInstantRead()
        {
            // Arrange
            _console.EnqueueKey('9');
            _console.EnqueueKey('1');

            var options = new List<ConsoleMenuOption>
                { ConsoleMenuOption.Create(1, "Test", () => { }) };

            // Act
            var result = _sut.ObtainOption(options, 
                ConsoleMenuSelectionType.InstantRead);

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Value);
            Assert.Contains(_console.ColoredLines, x => x.Color == ConsoleColor.Red);
        }

        [Fact]
        public void ObtainOption_ShouldAllowRetry_WithReadAfterConfirm()
        {
            // Arrange
            _console.EnqueueLine("abc");
            _console.EnqueueLine("1");

            var options = new List<ConsoleMenuOption>
                { ConsoleMenuOption.Create(1, "Test", () => {}) };

            // Act
            var result = _sut.ObtainOption(options, 
                ConsoleMenuSelectionType.ReadAfterConfirm);

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Value);
            Assert.Contains(_console.ColoredLines, x => x.Color == ConsoleColor.Red);
        }

        [Fact]
        public void ObtainOption_ShouldReturnFirstOption_WhenEnterPressedWithArrowSelection()
        {
            // Arrange
            _console.EnqueueKey((char)ConsoleKey.Enter);

            var options = new List<ConsoleMenuOption>
            {
                ConsoleMenuOption.Create(1, "Test", () => { }),
                ConsoleMenuOption.Create(2, "Test", () => { })
            };

            // Act
            var result = _sut.ObtainOption(options, ConsoleMenuSelectionType.ArrowSelection);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void ObtainOption_ShouldMoveDown_WhenDownArrowPressedWithArrowSelection()
        {
            // Arrange
            _console.EnqueueKey((char)ConsoleKey.DownArrow);
            _console.EnqueueKey((char)ConsoleKey.Enter);

            var options = new List<ConsoleMenuOption>
            {
                ConsoleMenuOption.Create(1, "Test", () => { }),
                ConsoleMenuOption.Create(2, "Test", () => { })
            };

            // Act
            var result = _sut.ObtainOption(options, ConsoleMenuSelectionType.ArrowSelection);

            // Assert
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public void ObtainOption_ShouldMoveUp_WhenUpArrowPressedWithArrowSelection()
        {
            // Arrange
            _console.EnqueueKey((char)ConsoleKey.DownArrow);
            _console.EnqueueKey((char)ConsoleKey.DownArrow);
            _console.EnqueueKey((char)ConsoleKey.UpArrow);
            _console.EnqueueKey((char)ConsoleKey.Enter);

            var options = new List<ConsoleMenuOption>
            {
                ConsoleMenuOption.Create(1, "Test", () => { }),
                ConsoleMenuOption.Create(2, "Test", () => { }),
                ConsoleMenuOption.Create(3, "Test", () => { })
            };

            // Act
            var result = _sut.ObtainOption(options, ConsoleMenuSelectionType.ArrowSelection);

            // Assert
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public void ObtainOption_ShouldWrapToLastOption_WhenUpArrowPressedOnFirstOptionWithArrowSelection()
        {
            // Arrange
            _console.EnqueueKey((char)ConsoleKey.UpArrow);
            _console.EnqueueKey((char)ConsoleKey.Enter);

            var options = new List<ConsoleMenuOption>
            {
                ConsoleMenuOption.Create(1, "Test", () => { }),
                ConsoleMenuOption.Create(2, "Test", () => { }),
                ConsoleMenuOption.Create(3, "Test", () => { })
            };

            // Act
            var result = _sut.ObtainOption(options, ConsoleMenuSelectionType.ArrowSelection);

            // Assert
            Assert.Equal(3, result.Id);
        }

        [Fact]
        public void ObtainOption_ShouldWrapToFirstOption_WhenDownArrowPressedOnLastOptionWithArrowSelection()
        {
            // Arrange
            _console.EnqueueKey((char)ConsoleKey.DownArrow);
            _console.EnqueueKey((char)ConsoleKey.DownArrow);
            _console.EnqueueKey((char)ConsoleKey.Enter);

            var options = new List<ConsoleMenuOption>
            {
                ConsoleMenuOption.Create(1, "Test", () => { }),
                ConsoleMenuOption.Create(2, "Test", () => { })
            };

            // Act
            var result = _sut.ObtainOption(options, ConsoleMenuSelectionType.ArrowSelection);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void ObtainOption_ShouldThrowException_WhenNoOptionsProvided()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => 
                _sut.ObtainOption([], ConsoleMenuSelectionType.InstantRead));
        }

        [Fact]
        public void ObtainOption_ShouldThrowException_WhenDuplicateIds()
        {
            // Arrange
            var options = new List<ConsoleMenuOption>
            {
                ConsoleMenuOption.Create(1, "Test", () => { }),
                ConsoleMenuOption.Create(1, "Test", () => { })
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(
                () => _sut.ObtainOption(options, ConsoleMenuSelectionType.InstantRead));
        }
    }
}
