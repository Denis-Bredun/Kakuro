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
        public void Should_ThrowArgumentNullException_When_OtherIsNull()
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

        [Fact]
        public void Should_SortRecordsByGameCompletionTime_When_RecordsHaveDifferentTimes()
        {
            // Arrange
            var random = new Random();
            var records = new List<RatingRecord>();

            for (int i = 0; i < 50; i++)
            {
                records.Add(new RatingRecord
                {
                    GameCompletionTime = new TimeOnly(random.Next(0, 24), random.Next(0, 60), random.Next(0, 60)),
                    GameCompletionDate = new DateOnly(2024, random.Next(1, 12), random.Next(1, 28))
                });
            }

            // Act
            var sortedRecords = records.OrderBy(r => r).ToList();

            // Assert
            for (int i = 1; i < sortedRecords.Count; i++)
            {
                Assert.True(sortedRecords[i - 1].GameCompletionTime <= sortedRecords[i].GameCompletionTime);
            }
        }

        [Fact]
        public void Should_ReturnNegative_When_CurrentRecordIsEarlierThanOther()
        {
            // Arrange
            var earlierRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(9, 0, 0),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };
            var laterRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(10, 0, 0),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };

            // Act
            var result = earlierRecord.CompareTo(laterRecord);

            // Assert
            Assert.True(result < 0);
        }

        [Fact]
        public void Should_ReturnPositive_When_CurrentRecordIsLaterThanOther()
        {
            // Arrange
            var earlierRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(10, 0, 0),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };
            var laterRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(9, 0, 0),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };

            // Act
            var result = earlierRecord.CompareTo(laterRecord);

            // Assert
            Assert.True(result > 0);
        }

        [Fact]
        public void Should_ReturnZero_When_BothRecordsHaveSameTime()
        {
            // Arrange
            var record1 = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(10, 0, 0),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };
            var record2 = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(10, 0, 0),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };

            // Act
            var result = record1.CompareTo(record2);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Should_HandleBoundaryTimes_When_RecordsAtStartAndEndOfDay()
        {
            // Arrange
            var startOfDayRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(0, 0, 0),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };
            var endOfDayRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(23, 59, 59),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };

            // Act
            var result = startOfDayRecord.CompareTo(endOfDayRecord);

            // Assert
            Assert.True(result < 0);
        }

        [Fact]
        public void Should_MaintainOrderOfEqualRecords_When_RecordsAreEqual()
        {
            // Arrange
            var records = new List<RatingRecord>
            {
                new RatingRecord { GameCompletionTime = new TimeOnly(10, 0, 0), GameCompletionDate = new DateOnly(2024, 9, 19) },
                new RatingRecord { GameCompletionTime = new TimeOnly(10, 0, 0), GameCompletionDate = new DateOnly(2024, 9, 19) },
                new RatingRecord { GameCompletionTime = new TimeOnly(10, 0, 0), GameCompletionDate = new DateOnly(2024, 9, 19) }
            };

            // Act
            var sortedRecords = records.OrderBy(record => record).ToList();

            // Assert
            Assert.Equal(records, sortedRecords);
        }

        [Fact]
        public void Should_NotSortByDate_When_RecordsHaveDifferentDates()
        {
            // Arrange
            var records = new List<RatingRecord>
            {
                new RatingRecord { GameCompletionTime = new TimeOnly(10, 0, 0), GameCompletionDate = new DateOnly(2024, 9, 19) },
                new RatingRecord { GameCompletionTime = new TimeOnly(9, 0, 0), GameCompletionDate = new DateOnly(2024, 9, 20) },
                new RatingRecord { GameCompletionTime = new TimeOnly(10, 0, 0), GameCompletionDate = new DateOnly(2024, 9, 18) }
            };

            // Act
            var sortedRecords = records.OrderBy(record => record).ToList();

            // Assert
            Assert.Equal(records[1], sortedRecords[0]);
            Assert.Equal(records[2], sortedRecords[1]);
            Assert.Equal(records[0], sortedRecords[2]);
        }
    }
}
