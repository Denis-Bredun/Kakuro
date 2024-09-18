using Kakuro.Models;

namespace Kakuro.Tests.Unit_Tests
{
    public class RatingRecordTests : IDisposable
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Unit Tests\\Files\\RatingRecordTests\\";

        public void Dispose()
        {
            if (Directory.Exists(DIRECTORY_PATH))
                Directory.Delete(DIRECTORY_PATH, true);
        }

        [Fact]
        public void CompareTo_ShouldThrowArgumentNullException_When_OtherIsNull()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(10, 0, 0),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };

            RatingRecord? nullRatingRecord = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => ratingRecord.CompareTo(nullRatingRecord));

            // Assert
            Assert.Equal("While sorting rating records, one was passed as null.", exception.Message);
        }
    }
}
