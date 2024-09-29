using Kakuro.Converters;
using System.Globalization;

namespace Kakuro.Tests.Unit_Tests
{
    public class WindowWidthToTabItemWidthConverterTests
    {
        private readonly WindowWidthToTabItemWidthConverter _converter;

        public WindowWidthToTabItemWidthConverterTests()
        {
            _converter = new WindowWidthToTabItemWidthConverter();
        }

        [Fact]
        public void Convert_Should_ReturnHalfWindowWidthMinusDPI_When_WindowWidthIsValid()
        {
            // Arrange
            double windowWidth = 100;
            double expectedWidth = (windowWidth / 2) - 10; // 10 is UNDISPLAYABLE_DPI

            // Act
            var result = _converter.Convert(windowWidth, null, null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(expectedWidth, result);
        }

        [Fact]
        public void Convert_Should_ReturnZero_When_ValueIsNotDouble()
        {
            // Arrange
            object nonDoubleValue = "invalid";

            // Act
            var result = _converter.Convert(nonDoubleValue, null, null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Convert_Should_ReturnZero_When_ValueIsNull()
        {
            // Arrange
            object nullValue = null;

            // Act
            var result = _converter.Convert(nullValue, null, null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void ConvertBack_Should_ThrowNotImplementedException()
        {
            // Arrange
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _converter.ConvertBack(0, null, null, CultureInfo.InvariantCulture));
        }
    }
}
