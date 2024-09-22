using Kakuro.Data_Access.Data_Providers;
using Kakuro.Data_Access.Repositories;
using Kakuro.Data_Access.Tools;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Models;
using Moq;

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


        // #BAD: tests below are made in a hurry. I should tests here much more things using Moq. I'll refactor these tests in the future

        [Fact]
        public void Should_ReturnRecordsFromCache_When_CacheIsSynchronized()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(0, 45),
                GameCompletionDate = new DateOnly(2024, 9, 19)
            };
            var difficultyLevel = DifficultyLevels.Easy;

            var mockDataService = new Mock<IReadAllRepository<RatingRecord, DifficultyLevels>>();
            mockDataService.Setup(m => m.GetAll(difficultyLevel)).Returns(new List<RatingRecord> { ratingRecord });

            var ratingRecordProvider = new RatingRecordProvider(mockDataService.Object);
            ratingRecordProvider.Add(ratingRecord, difficultyLevel);
            ratingRecordProvider.GetAll(difficultyLevel);

            // Act
            var records = ratingRecordProvider.GetAll(difficultyLevel).ToList();

            // Assert
            Assert.Contains(ratingRecord, records);
            Assert.True(ratingRecordProvider.Cache.ContainsKey(difficultyLevel));
            Assert.True(ratingRecordProvider.IsCacheSynchronizedWithFiles);

            mockDataService.Verify(m => m.GetAll(It.IsAny<DifficultyLevels>()), Times.Never);
        }

        [Fact]
        public void Should_ReturnRecordsFromDataService_When_CacheIsNotSynchronized()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(0, 50),
                GameCompletionDate = new DateOnly(2024, 9, 20)
            };
            var difficultyLevel = DifficultyLevels.Normal;

            var mockDataService = new Mock<IReadAllRepository<RatingRecord, DifficultyLevels>>();
            mockDataService.Setup(m => m.GetAll(difficultyLevel)).Returns(new List<RatingRecord> { ratingRecord });

            var ratingRecordProvider = new RatingRecordProvider(mockDataService.Object);
            ratingRecordProvider.Add(ratingRecord, difficultyLevel);

            // Assert
            Assert.False(ratingRecordProvider.Cache.ContainsKey(difficultyLevel));
            Assert.False(ratingRecordProvider.IsCacheSynchronizedWithFiles);

            // Act
            var records = ratingRecordProvider.GetAll(difficultyLevel).ToList();

            // Assert
            Assert.Contains(ratingRecord, records);
            Assert.True(ratingRecordProvider.Cache.ContainsKey(difficultyLevel));
            Assert.True(ratingRecordProvider.IsCacheSynchronizedWithFiles);

            mockDataService.Verify(m => m.GetAll(difficultyLevel), Times.Once);
        }

        [Fact]
        public void Should_UpdateCache_When_GetAllIsCalledAfterCacheInvalidation()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(0, 55),
                GameCompletionDate = new DateOnly(2024, 9, 21)
            };
            var difficultyLevel = DifficultyLevels.Hard;

            _ratingRecordProvider.Add(ratingRecord, difficultyLevel);

            // Act
            var recordsBeforeInvalidation = _ratingRecordProvider.GetAll(difficultyLevel).ToList();
            _ratingRecordProvider.Add(new RatingRecord { GameCompletionTime = new TimeOnly(1, 10), GameCompletionDate = new DateOnly(2024, 9, 22) }, difficultyLevel);
            var recordsAfterInvalidation = _ratingRecordProvider.GetAll(difficultyLevel).ToList();

            // Assert
            Assert.True(recordsBeforeInvalidation.SequenceEqual(_ratingRecordProvider.Cache[difficultyLevel]));
            Assert.NotEqual(recordsBeforeInvalidation, recordsAfterInvalidation);
        }
    }
}
