using Kakuro.Data_Access;
using Kakuro.Enums;
using Kakuro.Models;

namespace Kakuro.Tests.Integration_Tests
{
    public class RatingRecordRepositoryTests : IDisposable
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Integration Tests\\Files\\RatingRecordRepositoryTests\\";
        private JsonEnumerableFileHandler<RatingRecord> _jsonEnumerableFileHandler;
        private RatingRecordRepository _ratingRecordRepository;

        public RatingRecordRepositoryTests()
        {
            _jsonEnumerableFileHandler = new JsonEnumerableFileHandler<RatingRecord>();
            _ratingRecordRepository = new RatingRecordRepository(_jsonEnumerableFileHandler);
        }

        public void Dispose()
        {
            if (Directory.Exists(DIRECTORY_PATH))
                Directory.Delete(DIRECTORY_PATH, true);
        }

        [Fact]
        public void Should_SaveAndRetrieveRecord_When_AddingOneRecord()
        {
            // Arrange
            DifficultyLevels difficultyLevel = DifficultyLevels.Normal;
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(10, 30),
                GameCompletionDate = new DateOnly(2024, 9, 15)
            };

            // Act
            _ratingRecordRepository.Add(ratingRecord, difficultyLevel);

            // Assert
            var savedRecords = _ratingRecordRepository.GetAll(difficultyLevel).ToList();
            Assert.Single(savedRecords);
            Assert.Equal(new TimeOnly(10, 30), savedRecords[0].GameCompletionTime);
            Assert.Equal(new DateOnly(2024, 9, 15), savedRecords[0].GameCompletionDate);
        }

        [Fact]
        public void Should_ReturnEmptyCollection_When_NoRecordsExist()
        {
            // Arrange
            DifficultyLevels difficultyLevel = DifficultyLevels.Hard;

            // Act
            var records = _ratingRecordRepository.GetAll(difficultyLevel);

            // Assert
            Assert.Empty(records);
        }

        [Fact]
        public void Should_SaveAndRetrieveMultipleRecords_When_AddingMultipleRecords()
        {
            // Arrange
            DifficultyLevels difficultyLevel = DifficultyLevels.Easy;
            var ratingRecord1 = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(8, 15),
                GameCompletionDate = new DateOnly(2024, 7, 1)
            };
            var ratingRecord2 = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(9, 45),
                GameCompletionDate = new DateOnly(2024, 7, 2)
            };

            // Act
            _ratingRecordRepository.Add(ratingRecord1, difficultyLevel);
            _ratingRecordRepository.Add(ratingRecord2, difficultyLevel);

            // Assert
            var savedRecords = _ratingRecordRepository.GetAll(difficultyLevel).ToList();
            Assert.Equal(2, savedRecords.Count);

            Assert.Equal(new TimeOnly(8, 15), savedRecords[0].GameCompletionTime);
            Assert.Equal(new DateOnly(2024, 7, 1), savedRecords[0].GameCompletionDate);

            Assert.Equal(new TimeOnly(9, 45), savedRecords[1].GameCompletionTime);
            Assert.Equal(new DateOnly(2024, 7, 2), savedRecords[1].GameCompletionDate);
        }

        [Fact]
        public void Should_SaveRecordsForDifferentDifficulties_When_AddingRecordsToDifferentLevels()
        {
            // Arrange
            var easyRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(7, 30),
                GameCompletionDate = new DateOnly(2024, 8, 5)
            };
            var hardRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(10, 0),
                GameCompletionDate = new DateOnly(2024, 8, 10)
            };

            // Act
            _ratingRecordRepository.Add(easyRecord, DifficultyLevels.Easy);
            _ratingRecordRepository.Add(hardRecord, DifficultyLevels.Hard);

            // Assert
            var easyRecords = _ratingRecordRepository.GetAll(DifficultyLevels.Easy).ToList();
            var hardRecords = _ratingRecordRepository.GetAll(DifficultyLevels.Hard).ToList();

            Assert.Single(easyRecords);
            Assert.Equal(new TimeOnly(7, 30), easyRecords[0].GameCompletionTime);
            Assert.Equal(new DateOnly(2024, 8, 5), easyRecords[0].GameCompletionDate);

            Assert.Single(hardRecords);
            Assert.Equal(new TimeOnly(10, 0), hardRecords[0].GameCompletionTime);
            Assert.Equal(new DateOnly(2024, 8, 10), hardRecords[0].GameCompletionDate);
        }
    }
}
