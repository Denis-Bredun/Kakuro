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
        public void Should_AddRatingRecordToRepository_When_ValidEntityIsProvided()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(1, 30),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };
            var difficultyLevel = DifficultyLevels.Hard;

            // Act
            _ratingRecordProvider.Add(ratingRecord, difficultyLevel);

            // Assert
            var recordsFromRepository = _ratingRecordRepository.GetAll(difficultyLevel).ToList();
            Assert.Contains(ratingRecord, recordsFromRepository);
        }

        [Fact]
        public void Should_SetIsCacheSynchronizedWithFilesToFalse_When_AddIsCalled()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(2, 15),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };
            var difficultyLevel = DifficultyLevels.Normal;

            // Act
            _ratingRecordProvider.Add(ratingRecord, difficultyLevel);

            // Assert
            var cache = _ratingRecordProvider.Cache;
            Assert.False(_ratingRecordProvider.IsCacheSynchronizedWithFiles);
            Assert.False(cache.ContainsKey(difficultyLevel));
        }

        [Fact]
        public void Should_UpdateCache_When_GetAllIsCalledAfterAdd()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(0, 45),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };
            var difficultyLevel = DifficultyLevels.Easy;

            // Act
            _ratingRecordProvider.Add(ratingRecord, difficultyLevel);
            var records = _ratingRecordProvider.GetAll(difficultyLevel).ToList();

            // Assert
            Assert.Contains(ratingRecord, records);
            Assert.True(_ratingRecordProvider.IsCacheSynchronizedWithFiles);
            Assert.True(_ratingRecordProvider.Cache.ContainsKey(difficultyLevel));
            var cachedRecords = _ratingRecordProvider.Cache[difficultyLevel].ToList();
            Assert.Equal(records.Count, cachedRecords.Count);
            for (int i = 0; i < records.Count; i++)
                Assert.Equal(records[i], cachedRecords[i]);
        }

        [Fact]
        public void Should_ThrowNullReferenceException_When_AddingNullEntity()
        {
            // Arrange
            RatingRecord nullRatingRecord = null;
            var difficultyLevel = DifficultyLevels.Hard;

            // Act & Assert
            var exception = Assert.Throws<NullReferenceException>(() =>
                _ratingRecordProvider.Add(nullRatingRecord, difficultyLevel));

            Assert.Equal("Entity equals null", exception.Message);
        }

        [Fact]
        public void Should_ThrowArgumentException_When_AddingEntityWithDefaultValues()
        {
            // Arrange
            RatingRecord nullRatingRecord = new RatingRecord() { GameCompletionDate = default, GameCompletionTime = default };
            var difficultyLevel = DifficultyLevels.Hard;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _ratingRecordProvider.Add(nullRatingRecord, difficultyLevel));

            Assert.Equal("Entity's properties cannot be null or default.", exception.Message);
        }
    }
}
