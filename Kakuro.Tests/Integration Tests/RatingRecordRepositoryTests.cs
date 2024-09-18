using Kakuro.Data_Access;
using Kakuro.Enums;
using Kakuro.Models;

namespace Kakuro.Tests.Integration_Tests
{
    public class RatingRecordRepositoryTests : IDisposable
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Integration Tests\\Files\\RatingRecordRepositoryTests\\";
        private JsosFileHandler<RatingRecord> _jsonEnumerableFileHandler;
        private RatingRecordRepository _ratingRecordRepository;

        public RatingRecordRepositoryTests()
        {
            _jsonEnumerableFileHandler = new JsosFileHandler<RatingRecord>();
            _ratingRecordRepository = new RatingRecordRepository(_jsonEnumerableFileHandler, DIRECTORY_PATH);
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

        [Fact]
        public void Should_ReturnEmptyCollection_When_FileExistsButHasNoData()
        {
            // Arrange
            if (!Directory.Exists(DIRECTORY_PATH))
                Directory.CreateDirectory(DIRECTORY_PATH);

            var difficultyLevel = DifficultyLevels.Normal;
            var filepath = Path.Combine(DIRECTORY_PATH, difficultyLevel.ToString() + ". Rating Table.json");
            File.WriteAllText(filepath, string.Empty);

            // Act
            var records = _ratingRecordRepository.GetAll(difficultyLevel);

            // Assert
            Assert.Empty(records);
        }

        [Fact]
        public void Should_SaveAndRetrieveDuplicateRecords_When_AddingSameRecordTwice()
        {
            // Arrange
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(10, 0),
                GameCompletionDate = new DateOnly(2024, 9, 15)
            };
            DifficultyLevels difficultyLevel = DifficultyLevels.Normal;

            // Act
            _ratingRecordRepository.Add(ratingRecord, difficultyLevel);
            _ratingRecordRepository.Add(ratingRecord, difficultyLevel);

            // Assert
            var savedRecords = _ratingRecordRepository.GetAll(difficultyLevel).ToList();
            Assert.Equal(2, savedRecords.Count);
            Assert.Equal(new TimeOnly(10, 0), savedRecords[0].GameCompletionTime);
            Assert.Equal(new DateOnly(2024, 9, 15), savedRecords[0].GameCompletionDate);
        }

        [Fact]
        public void Should_SaveAndRetrieveEmptyRecord_When_AddingRecordWithDefaultValues()
        {
            // Arrange
            var emptyRecord = new RatingRecord();
            DifficultyLevels difficultyLevel = DifficultyLevels.Easy;

            // Act
            _ratingRecordRepository.Add(emptyRecord, difficultyLevel);

            // Assert
            var savedRecords = _ratingRecordRepository.GetAll(difficultyLevel).ToList();
            Assert.Single(savedRecords);
            Assert.Equal(default, savedRecords[0].GameCompletionTime);
            Assert.Equal(default, savedRecords[0].GameCompletionDate);
        }

        [Fact]
        public void Should_NotSaveRecord_When_AddingNullRecord()
        {
            // Arrange
            DifficultyLevels difficultyLevel = DifficultyLevels.Normal;
            var initialRecords = _ratingRecordRepository.GetAll(difficultyLevel).ToList();

            // Act
            _ratingRecordRepository.Add(null, difficultyLevel);

            // Assert
            var savedRecords = _ratingRecordRepository.GetAll(difficultyLevel).ToList();
            Assert.Equal(initialRecords.Count, savedRecords.Count);
        }

        [Fact]
        public void Should_NotSaveRecord_When_SaveFailsDueToPermissions()
        {
            // Arrange
            var readOnlyDirectoryPath = "C:\\Windows";
            var readOnlyRepository = new RatingRecordRepository(_jsonEnumerableFileHandler, readOnlyDirectoryPath);
            var ratingRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(10, 0),
                GameCompletionDate = new DateOnly(2024, 9, 15)
            };
            var initialRecords = _ratingRecordRepository.GetAll(DifficultyLevels.Easy).ToList();

            // Act
            try
            {
                readOnlyRepository.Add(ratingRecord, DifficultyLevels.Easy);
            }
            catch (UnauthorizedAccessException)
            {
                // Ignoring exception, because we just want to check the file after catching it
            }

            // Assert
            var savedRecords = _ratingRecordRepository.GetAll(DifficultyLevels.Easy).ToList();
            Assert.Equal(initialRecords.Count, savedRecords.Count);
        }

        [Fact]
        public void Should_NotAddMoreThanTenRecords_When_AddingMoreThanLimit()
        {
            // Arrange
            var random = new Random();
            var records = Enumerable.Range(1, 10).Select(i => new RatingRecord
            {
                GameCompletionTime = new TimeOnly(random.Next(0, 24), random.Next(0, 60)),
                GameCompletionDate = new DateOnly(2024, 1, 1)
            }).ToList();

            foreach (var record in records)
                _ratingRecordRepository.Add(record, DifficultyLevels.Easy);

            var newRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(23, 59),
                GameCompletionDate = new DateOnly(2024, 1, 2)
            };

            // Act
            _ratingRecordRepository.Add(newRecord, DifficultyLevels.Easy);

            // Assert
            var savedRecords = _ratingRecordRepository.GetAll(DifficultyLevels.Easy).ToList();
            Assert.Equal(10, savedRecords.Count);
            Assert.DoesNotContain(newRecord, savedRecords);
        }

        [Fact]
        public void Should_SortRecords_When_AddingMultipleRandomRecords()
        {
            // Arrange
            var random = new Random();
            var records = Enumerable.Range(1, 8).Select(i => new RatingRecord
            {
                GameCompletionTime = new TimeOnly(random.Next(0, 24), random.Next(0, 60)),
                GameCompletionDate = new DateOnly(2024, 1, 1)
            }).ToList();

            foreach (var record in records)
                _ratingRecordRepository.Add(record, DifficultyLevels.Easy);

            // Act
            var sortedRecords = _ratingRecordRepository.GetAll(DifficultyLevels.Easy).ToList();

            // Assert
            Assert.Equal(records.Count, sortedRecords.Count);

            for (int i = 0; i < sortedRecords.Count - 1; i++)
                Assert.True(sortedRecords[i].GameCompletionTime < sortedRecords[i + 1].GameCompletionTime);
        }

        [Fact]
        public void Should_SortRecordsAndNotAddMoreThanTen_When_AddingNewRecord()
        {
            // Arrange
            var random = new Random();

            var initialRecords = Enumerable.Range(1, 10).Select(i => new RatingRecord
            {
                GameCompletionTime = new TimeOnly(random.Next(0, 24), random.Next(0, 60)),
                GameCompletionDate = new DateOnly(2024, 1, 1)
            }).ToList();

            foreach (var record in initialRecords)
            {
                _ratingRecordRepository.Add(record, DifficultyLevels.Easy);
            }

            var newRecord = new RatingRecord
            {
                GameCompletionTime = new TimeOnly(23, 59),
                GameCompletionDate = new DateOnly(2024, 1, 2)
            };

            // Act
            _ratingRecordRepository.Add(newRecord, DifficultyLevels.Easy);

            // Assert
            var savedRecords = _ratingRecordRepository.GetAll(DifficultyLevels.Easy).ToList();

            Assert.Equal(10, savedRecords.Count);

            Assert.DoesNotContain(newRecord, savedRecords);

            for (int i = 0; i < savedRecords.Count - 1; i++)
                Assert.True(savedRecords[i].GameCompletionTime < savedRecords[i + 1].GameCompletionTime);
        }

    }
}
