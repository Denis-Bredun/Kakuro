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
            Assert.False(_ratingRecordProvider.IsCacheSynchronizedWithFiles[difficultyLevel]);
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
            Assert.True(ratingRecordProvider.IsCacheSynchronizedWithFiles[difficultyLevel]);

            mockDataService.Verify(m => m.GetAll(It.IsAny<DifficultyLevels>()), Times.Once); // Once - I mean the first time we call GetAll() just to get Cache synchronized
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
            Assert.False(ratingRecordProvider.IsCacheSynchronizedWithFiles[difficultyLevel]);

            // Act
            var records = ratingRecordProvider.GetAll(difficultyLevel).ToList();

            // Assert
            Assert.Contains(ratingRecord, records);
            Assert.True(ratingRecordProvider.Cache.ContainsKey(difficultyLevel));
            Assert.True(ratingRecordProvider.IsCacheSynchronizedWithFiles[difficultyLevel]);

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

            // Assert: we check that cache still contains old records cuz' we didn't call GetAll(), so it's not updated
            Assert.True(recordsBeforeInvalidation.SequenceEqual(_ratingRecordProvider.Cache[difficultyLevel]));

            // Act: cache is being updated here
            var recordsAfterInvalidation = _ratingRecordProvider.GetAll(difficultyLevel).ToList();

            // Assert: now our cache equals recordsAfterInvalidation
            Assert.True(recordsAfterInvalidation.SequenceEqual(_ratingRecordProvider.Cache[difficultyLevel]));
            Assert.NotEqual(recordsBeforeInvalidation, recordsAfterInvalidation);
        }

        [Fact]
        public void Should_ReturnEmptyCollection_When_NoRecordsExistForDifficultyLevel()
        {
            // Arrange
            var difficultyLevel = DifficultyLevels.Easy;

            // Act
            var records = _ratingRecordProvider.GetAll(difficultyLevel);

            // Assert
            Assert.Empty(records);
            Assert.True(_ratingRecordProvider.IsCacheSynchronizedWithFiles[difficultyLevel]);
        }

        [Fact]
        public void Should_ReturnRecords_When_MultipleRecordsExistForDifficultyLevel()
        {
            // Arrange
            var difficultyLevel = DifficultyLevels.Easy;
            var ratingRecord1 = new RatingRecord { GameCompletionTime = new TimeOnly(0, 30), GameCompletionDate = new DateOnly(2024, 9, 23) };
            var ratingRecord2 = new RatingRecord { GameCompletionTime = new TimeOnly(1, 15), GameCompletionDate = new DateOnly(2024, 9, 24) };

            _ratingRecordProvider.Add(ratingRecord1, difficultyLevel);
            _ratingRecordProvider.Add(ratingRecord2, difficultyLevel);

            // Act
            var records = _ratingRecordProvider.GetAll(difficultyLevel).ToList();

            // Assert
            Assert.Contains(ratingRecord1, records);
            Assert.Contains(ratingRecord2, records);
            Assert.Equal(2, records.Count);
        }

        [Fact]
        public void Should_UpdateCacheCorrectly_When_AddingRecordsWithDifferentDifficultyLevels()
        {
            // Arrange
            var difficultyLevels = new[] { DifficultyLevels.Easy, DifficultyLevels.Normal, DifficultyLevels.Hard };
            var ratingRecords = new Dictionary<DifficultyLevels, List<RatingRecord>>();

            foreach (var level in difficultyLevels)
                ratingRecords[level] = GenerateRandomRatingRecords(level, 3);

            // Act
            foreach (var difficultyLevel in difficultyLevels)
                foreach (var record in ratingRecords[difficultyLevel])
                    _ratingRecordProvider.Add(record, difficultyLevel);

            // Assert
            foreach (var difficultyLevel in difficultyLevels)
            {
                var records = _ratingRecordProvider.GetAll(difficultyLevel).ToList();

                Assert.Equal(3, records.Count);
                Assert.Contains(ratingRecords[difficultyLevel][0], records);
                Assert.Contains(ratingRecords[difficultyLevel][1], records);
                Assert.True(_ratingRecordProvider.Cache.ContainsKey(difficultyLevel));
            }
        }

        [Fact]
        public void Should_MaintainCacheState_When_ConsecutiveCallsToGetAllAreMade()
        {
            // Arrange
            var difficultyLevel = DifficultyLevels.Normal;
            var ratingRecord = new RatingRecord { GameCompletionTime = new TimeOnly(0, 45), GameCompletionDate = new DateOnly(2024, 9, 25) };

            _ratingRecordProvider.Add(ratingRecord, difficultyLevel);
            _ratingRecordProvider.GetAll(difficultyLevel); // First call to synchronize cache

            // Act
            var recordsFirstCall = _ratingRecordProvider.GetAll(difficultyLevel).ToList();
            var recordsSecondCall = _ratingRecordProvider.GetAll(difficultyLevel).ToList();

            // Assert
            Assert.True(recordsFirstCall.SequenceEqual(recordsSecondCall));
            Assert.True(_ratingRecordProvider.IsCacheSynchronizedWithFiles[difficultyLevel]);
        }

        [Fact]
        public void Should_MaintainCacheState_When_AddingMultipleRecordsForDifferentDifficultyLevels()
        {
            // Arrange
            var difficultyLevels = new[] { DifficultyLevels.Easy, DifficultyLevels.Normal, DifficultyLevels.Hard };
            var ratingRecords = new Dictionary<DifficultyLevels, List<RatingRecord>>();

            foreach (var level in difficultyLevels)
                ratingRecords[level] = GenerateRandomRatingRecords(level, 3);

            // Act and Assert for each difficulty level
            foreach (var difficultyLevel in difficultyLevels)
                AddRecordsAndValidateCache(difficultyLevel, ratingRecords[difficultyLevel]);
        }

        private List<RatingRecord> GenerateRandomRatingRecords(DifficultyLevels difficultyLevel, int count)
        {
            var random = new Random();
            var records = new List<RatingRecord>();

            for (int i = 0; i < count; i++)
            {
                var gameCompletionTime = new TimeOnly(random.Next(1, 24), random.Next(1, 60), random.Next(1, 60));
                var gameCompletionDate = DateOnly.FromDateTime(DateTime.Now.AddDays(i));

                records.Add(new RatingRecord
                {
                    GameCompletionTime = gameCompletionTime,
                    GameCompletionDate = gameCompletionDate
                });
            }

            return records;
        }

        private void AddRecordsAndValidateCache(DifficultyLevels difficultyLevel, List<RatingRecord> records)
        {
            for (int i = 0; i < records.Count; i++)
            {
                // Check cache state before adding
                if (i == 0)
                    Assert.False(_ratingRecordProvider.Cache.ContainsKey(difficultyLevel));

                // Act
                _ratingRecordProvider.Add(records[i], difficultyLevel);

                // Check cache state after adding
                Assert.False(_ratingRecordProvider.IsCacheSynchronizedWithFiles[difficultyLevel]);

                // Act
                var retrievedRecords = _ratingRecordProvider.GetAll(difficultyLevel).ToList();

                // Assert
                Assert.Contains(records[i], retrievedRecords);
                Assert.True(_ratingRecordProvider.Cache.ContainsKey(difficultyLevel));
                Assert.True(_ratingRecordProvider.IsCacheSynchronizedWithFiles[difficultyLevel]);
            }
        }
    }
}
