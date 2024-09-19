using Kakuro.Data_Access.Data_Providers;
using Kakuro.Data_Access.Repositories;
using Kakuro.Data_Access.Tools;
using Kakuro.Enums;
using Kakuro.Models;

namespace Kakuro.Tests.Integration_Tests
{
    public class RatingRecordProviderTests : IDisposable
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Integration Tests\\Files\\RatingRecordProviderTests\\";
        private JsonFileHandler<RatingRecord> _jsonFileHandler;
        private RatingRecordRepository _ratingRecordRepository;
        private RatingRecordProvider _ratingRecordProvider;

        public RatingRecordProviderTests()
        {
            _jsonFileHandler = new JsonFileHandler<RatingRecord>();
            _ratingRecordRepository = new RatingRecordRepository(_jsonFileHandler, DIRECTORY_PATH);
            _ratingRecordProvider = new RatingRecordProvider(_ratingRecordRepository);
        }

        public void Dispose()
        {
            if (Directory.Exists(DIRECTORY_PATH))
                Directory.Delete(DIRECTORY_PATH, true);
        }

        [Fact]
        public void Should_AddAndRetrieveRatingRecord_When_ValidDataIsProvided()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(1, 30),
                GameCompletionDate = new DateOnly(2024, 9, 18)
            };
            var difficulty = DifficultyLevels.Normal;

            // Act
            _ratingRecordProvider.Add(ratingRecord, difficulty);
            var records = _ratingRecordProvider.GetAll(difficulty).ToList();

            // Assert
            Assert.Single(records);
            Assert.Equal(ratingRecord.GameCompletionTime, records[0].GameCompletionTime);
            Assert.Equal(ratingRecord.GameCompletionDate, records[0].GameCompletionDate);
        }

        [Fact]
        public void Should_CacheRatingRecords_When_RatingRecordsAreFetched()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(1, 30),
                GameCompletionDate = new DateOnly(2024, 9, 18)
            };
            var difficulty = DifficultyLevels.Hard;

            // Act
            _ratingRecordProvider.Add(ratingRecord, difficulty);

            // First retrieval - should come from the repository and cache it
            var recordsFromRepo = _ratingRecordProvider.GetAll(difficulty).ToList();

            // Simulate cache hit - second retrieval should come from the cache
            var cachedRecords = _ratingRecordProvider.GetAll(difficulty).ToList();

            // Assert
            Assert.Single(recordsFromRepo);
            Assert.Single(cachedRecords);
            Assert.Equal(recordsFromRepo.First().GameCompletionTime, cachedRecords.First().GameCompletionTime);
        }

        [Fact]
        public void Should_ClearCache_When_NewRecordIsAdded()
        {
            // Arrange
            var initialRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(1, 30),
                GameCompletionDate = new DateOnly(2024, 9, 18)
            };
            var newRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(2, 15),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };
            var difficulty = DifficultyLevels.Easy;

            _ratingRecordProvider.Add(initialRecord, difficulty);

            // Act
            var initialCache = _ratingRecordProvider.GetAll(difficulty).ToList();

            // Adding a new record should reset cache
            _ratingRecordProvider.Add(newRecord, difficulty);
            var updatedCache = _ratingRecordProvider.GetAll(difficulty).ToList();

            // Assert
            Assert.Single(initialCache); // Only 1 in initial cache
            Assert.Equal(2, updatedCache.Count); // Cache updated with both records
            Assert.Contains(updatedCache, r => r.GameCompletionTime == newRecord.GameCompletionTime);
        }

        [Fact]
        public void Should_ThrowException_When_NullRecordAdded()
        {
            // Arrange
            var difficulty = DifficultyLevels.Normal;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _ratingRecordProvider.Add(null, difficulty));
        }

        [Fact]
        public void Should_ReturnEmptyCollection_When_NoRecordsExistForDifficulty()
        {
            // Arrange
            var difficulty = DifficultyLevels.Hard;

            // Act
            var records = _ratingRecordProvider.GetAll(difficulty);

            // Assert
            Assert.Empty(records);
        }

        [Fact]
        public void Should_NotDuplicateCache_When_SameKeyIsUsed()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(1, 30),
                GameCompletionDate = new DateOnly(2024, 9, 18)
            };
            var difficulty = DifficultyLevels.Normal;

            // Act
            _ratingRecordProvider.Add(ratingRecord, difficulty);
            var recordsFirstFetch = _ratingRecordProvider.GetAll(difficulty).ToList();

            // Fetching the same difficulty should not duplicate cache
            var recordsSecondFetch = _ratingRecordProvider.GetAll(difficulty).ToList();

            // Assert
            Assert.Single(recordsFirstFetch);
            Assert.Single(recordsSecondFetch);
        }

    }
}
