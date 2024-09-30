using Kakuro.Converters;
using System.Globalization;
using System.Windows.Data;

namespace Kakuro.Tests.Unit_Tests
{
    public class SumConverterTests
    {
        private readonly IMultiValueConverter _converter;

        public SumConverterTests()
        {
            _converter = new SumConverter();
        }

        [Fact]
        public void Should_ReturnEmptyString_When_ValuesAreNull()
        {
            // Arrange
            object[] values = { null, null };

            // Act
            var result = _converter.Convert(values, null, null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Should_ReturnEmptyString_When_OneValueIsNull()
        {
            // Arrange
            object[] values = { "5", null };

            // Act
            var result = _converter.Convert(values, null, null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Should_ReturnFormattedString_When_ValidValuesProvided()
        {
            // Arrange
            object[] values = { "5", "10" };

            // Act
            var result = _converter.Convert(values, null, null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal("╲  5\n10  ╲", result);
        }

        [Fact]
        public void Should_ThrowNotImplementedException_When_ConvertBackIsCalled()
        {
            // Arrange
            var value = "some value";
            var targetTypes = new Type[0];

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _converter.ConvertBack(value, targetTypes, null, CultureInfo.InvariantCulture));
        }

        [Fact]
        public void Should_HandleNonStringValues_When_ValidNumericValuesProvided()
        {
            // Arrange
            object[] values = { 5, 10 };

            // Act
            var result = _converter.Convert(values, null, null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal("╲  5\n10  ╲", result);
        }

        [Fact]
        public void Should_HandleMixedDataTypes_When_MixedStringAndNumericValuesProvided()
        {
            // Arrange
            object[] values = { 5, "10" };

            // Act
            var result = _converter.Convert(values, null, null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal("╲  5\n10  ╲", result);
        }
    }
}
