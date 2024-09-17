﻿using Kakuro.Data_Access;
using Kakuro.Models;

namespace Kakuro.Tests.Integration_Tests
{
    public class SavepointRepositoryTests : IDisposable
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Integration Tests\\Files\\SavepointRepositoryTests\\";
        private readonly string _filename = "Savepoints.json";
        private readonly string _filepath;
        private JsonEnumerableFileHandler<Savepoint> _jsonEnumerableFileHandler;
        private SavepointRepository _savepointRepository;

        public SavepointRepositoryTests()
        {
            _jsonEnumerableFileHandler = new JsonEnumerableFileHandler<Savepoint>();
            _savepointRepository = new SavepointRepository(_jsonEnumerableFileHandler, DIRECTORY_PATH);
            _filepath = Path.Combine(DIRECTORY_PATH, _filename);
        }

        public void Dispose()
        {
            if (Directory.Exists(DIRECTORY_PATH))
                Directory.Delete(DIRECTORY_PATH, true);
        }

        [Fact]
        public void Should_AddSavepoint_When_ValidSavepointProvided()
        {
            // Arrange
            var savepoint = new Savepoint
            {
                Id = 0,
                DashboardItems = new List<DashboardItem>
            {
                new DashboardItem { Value = 5 },
                new DashboardItem { Value = 3 }
            }
            };

            // Act
            _savepointRepository.Add(savepoint);

            // Assert
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();
            Assert.Single(savedSavepoints);
            Assert.Equal(savepoint.Id, savedSavepoints[0].Id);
            Assert.Equal(savepoint.DashboardItems[0].Value, savedSavepoints[0].DashboardItems[0].Value);
            Assert.Equal(savepoint.DashboardItems[1].Value, savedSavepoints[0].DashboardItems[1].Value);
        }

        [Fact]
        public void Should_NotAddSavepoint_When_NullSavepointProvided()
        {
            // Arrange
            var initialSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();

            // Act
            _savepointRepository.Add(null);

            // Assert
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();
            Assert.Equal(initialSavepoints.Count, savedSavepoints.Count);
        }

        [Fact]
        public void Should_AddMultipleSavepoints_When_MultipleSavepointsWithMultipleDashboardItems()
        {
            // Arrange
            var savepoint1 = new Savepoint
            {
                Id = 0,
                DashboardItems = new List<DashboardItem>
                {
                    new DashboardItem { Value = 2, Notes = new[] { 1, 2, 3, 0, 0, 0, 0, 0, 0 } },
                    new DashboardItem { Value = 4, Notes = new[] { 7, 8, 9, 0, 0, 0, 0, 0, 0 } }
                }
            };

            var savepoint2 = new Savepoint
            {
                Id = 1,
                DashboardItems = new List<DashboardItem>
                {
                    new DashboardItem { Value = 8, Notes = new[] { 4, 5, 6, 0, 0, 0, 0, 0, 0 } },
                    new DashboardItem { Value = 10, Notes = new[] { 3, 2, 1, 0, 0, 0, 0, 0, 0 } }
                }
            };

            // Act
            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);

            // Assert
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();
            Assert.Equal(2, savedSavepoints.Count);

            Assert.Equal(savepoint1.Id, savedSavepoints[0].Id);
            Assert.Equal(savepoint1.DashboardItems[0].Value, savedSavepoints[0].DashboardItems[0].Value);
            Assert.Equal(savepoint1.DashboardItems[0].Notes, savedSavepoints[0].DashboardItems[0].Notes);
            Assert.Equal(savepoint1.DashboardItems[1].Value, savedSavepoints[0].DashboardItems[1].Value);
            Assert.Equal(savepoint1.DashboardItems[1].Notes, savedSavepoints[0].DashboardItems[1].Notes);

            Assert.Equal(savepoint2.Id, savedSavepoints[1].Id);
            Assert.Equal(savepoint2.DashboardItems[0].Value, savedSavepoints[1].DashboardItems[0].Value);
            Assert.Equal(savepoint2.DashboardItems[0].Notes, savedSavepoints[1].DashboardItems[0].Notes);
            Assert.Equal(savepoint2.DashboardItems[1].Value, savedSavepoints[1].DashboardItems[1].Value);
            Assert.Equal(savepoint2.DashboardItems[1].Notes, savedSavepoints[1].DashboardItems[1].Notes);
        }

        [Fact]
        public void Should_AddSavepoint_When_EmptyDashboardItemsAreProvided()
        {
            // Arrange
            var savepoint = new Savepoint
            {
                Id = 0,
                DashboardItems = new List<DashboardItem>()
            };

            // Act
            _savepointRepository.Add(savepoint);

            // Assert
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();
            Assert.Single(savedSavepoints);
            Assert.Empty(savedSavepoints[0].DashboardItems);
        }

        [Fact]
        public void Should_AddSavepoint_When_PartialNotesAreProvided()
        {
            // Arrange
            var savepoint = new Savepoint
            {
                Id = 0,
                DashboardItems = new List<DashboardItem>
                {
                    new DashboardItem { Value = 2, Notes = new[] { 1, 2, 3, 0, 0, 0, 0, 0, 0 } }
                }
            };

            // Act
            _savepointRepository.Add(savepoint);

            // Assert
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();
            Assert.Single(savedSavepoints);
            Assert.Equal(savepoint.DashboardItems[0].Notes, savedSavepoints[0].DashboardItems[0].Notes);
        }

        [Fact]
        public void Should_AddSavepoint_When_DashboardItemValueIsNull()
        {
            // Arrange
            var savepoint = new Savepoint
            {
                Id = 0,
                DashboardItems = new List<DashboardItem>
                {
                    new DashboardItem { Value = null, Notes = new[] { 1, 2, 3, 0, 0, 0, 0, 0, 0 } }
                }
            };

            // Act
            _savepointRepository.Add(savepoint);

            // Assert
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();
            Assert.Single(savedSavepoints);
            Assert.Null(savedSavepoints[0].DashboardItems[0].Value);
        }

        [Fact]
        public void Should_NotAddSavepoint_When_SavepointWithDuplicateIdIsGiven()
        {
            // Arrange
            var savepoint1 = new Savepoint
            {
                Id = 0,
                DashboardItems = new List<DashboardItem>
                {
                    new DashboardItem { Value = 2, Notes = new[] { 1, 2, 3, 0, 0, 0, 0, 0, 0 } }
                }
            };

            var savepoint2 = new Savepoint
            {
                Id = 0,
                DashboardItems = new List<DashboardItem>
                {
                    new DashboardItem { Value = 8, Notes = new[] { 4, 5, 6, 0, 0, 0, 0, 0, 0 } }
                }
            };

            // Act
            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);

            // Assert
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();
            Assert.Single(savedSavepoints);
            var savedSavepoint = savedSavepoints.First();
            Assert.Equal(savepoint1.Id, savedSavepoint.Id);
            Assert.Equal(savepoint1.DashboardItems.Count, savedSavepoint.DashboardItems.Count);
            Assert.Equal(savepoint1.DashboardItems[0].Value, savedSavepoint.DashboardItems[0].Value);
            Assert.Equal(savepoint1.DashboardItems[0].Notes, savedSavepoint.DashboardItems[0].Notes);
        }

        [Fact]
        public void Should_ReturnZero_When_NoSavepointsExist()
        {
            // Arrange
            // Repository is empty

            // Act
            int count = _savepointRepository.Count;

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void Should_ReturnCorrectCount_When_AddingSavepoints()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);

            // Act
            int count = _savepointRepository.Count;

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void Should_ReturnCorrectCount_When_DeletedSavepoints()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);
            _savepointRepository.Delete(0);

            // Act
            int count = _savepointRepository.Count;

            // Assert
            Assert.Equal(1, count);
        }

        [Fact]
        public void Should_ReturnCorrectCount_When_UpdatedSavepoints()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);
            var updatedSavepoint = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem> { new DashboardItem { Value = 5 } } };
            _savepointRepository.Update(updatedSavepoint);

            // Act
            int count = _savepointRepository.Count;

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void Should_ReturnZero_When_DeletedAllSavepoints()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);
            _savepointRepository.Delete(0);
            _savepointRepository.Delete(1);

            // Act
            int count = _savepointRepository.Count;

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void Should_DeleteExistingSavepoint_When_ValidIdIsProvided()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);
            _savepointRepository.Delete(0);

            // Act
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();

            // Assert
            Assert.Single(savedSavepoints);
            Assert.Equal(savepoint2.Id, savedSavepoints[0].Id);
        }

        [Fact]
        public void Should_NotDelete_When_IdIsOutOfRange()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);
            _savepointRepository.Delete(999);

            // Act
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();

            // Assert
            Assert.Equal(2, savedSavepoints.Count);
            Assert.True(savedSavepoints.Count(el => el.Id == savepoint1.Id) == 1);
            Assert.True(savedSavepoints.Count(el => el.Id == savepoint2.Id) == 1);
        }

        [Fact]
        public void Should_NotDelete_When_IdIsNegative()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);
            _savepointRepository.Delete(-1);

            // Act
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();

            // Assert
            Assert.Equal(2, savedSavepoints.Count);
            Assert.True(savedSavepoints.Count(el => el.Id == savepoint1.Id) == 1);
            Assert.True(savedSavepoints.Count(el => el.Id == savepoint2.Id) == 1);
        }

        [Fact]
        public void Should_DeleteOnlySpecifiedSavepoint_When_ValidIdIsProvided()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };
            var savepoint3 = new Savepoint { Id = 2, DashboardItems = new List<DashboardItem>() };

            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);
            _savepointRepository.Add(savepoint3);
            _savepointRepository.Delete(1);

            // Act
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();

            // Assert
            Assert.Equal(2, savedSavepoints.Count);
            Assert.True(savedSavepoints.Count(el => el.Id == savepoint1.Id) == 1);
            Assert.True(savedSavepoints.Count(el => el.Id == savepoint3.Id) == 1);
            Assert.False(savedSavepoints.Count(el => el.Id == savepoint2.Id) == 1);
        }

        [Fact]
        public void Should_HandleEmptyRepository_When_DeleteIsCalled()
        {
            // Arrange
            // Our reposiitory is empty

            // Act
            _savepointRepository.Delete(0);

            // Assert
            Assert.Equal(0, _savepointRepository.Count);
        }

        [Fact]
        public void Should_HandleDelete_When_DeleteAlreadyDeletedSavepoint()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);
            _savepointRepository.Delete(1);
            _savepointRepository.Delete(1);

            // Act
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();

            // Assert
            Assert.Single(savedSavepoints);
            Assert.True(savedSavepoints.Count(el => el.Id == savepoint1.Id) == 1);
        }

        [Fact]
        public void Should_SaveChangesToFile_When_Delete()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 0, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            _savepointRepository.Add(savepoint1);
            _savepointRepository.Add(savepoint2);
            _savepointRepository.Delete(0);

            // Act
            var savedSavepoints = _jsonEnumerableFileHandler.Load(_filepath).ToList();

            // Assert
            Assert.Single(savedSavepoints);
            Assert.Equal(savepoint2.Id, savedSavepoints[0].Id);
        }
    }
}
