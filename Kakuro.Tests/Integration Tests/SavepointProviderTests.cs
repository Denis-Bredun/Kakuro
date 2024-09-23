using Kakuro.Data_Access.Data_Providers;
using Kakuro.Data_Access.Repositories;
using Kakuro.Data_Access.Tools;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Models;
using Moq;

namespace Kakuro.Tests.Integration_Tests
{
    public class SavepointProviderTests : IDisposable
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Integration Tests\\Files\\SavepointProviderTests\\";
        private JsonFileHandler<Savepoint> _jsonFileHandler;
        private SavepointRepository _savepointRepository;
        private SavepointProvider _savepointProvider;

        public SavepointProviderTests()
        {
            _jsonFileHandler = new JsonFileHandler<Savepoint>();
            _savepointRepository = new SavepointRepository(_jsonFileHandler, DIRECTORY_PATH);
            _savepointProvider = new SavepointProvider(_savepointRepository);
        }

        public void Dispose()
        {
            if (Directory.Exists(DIRECTORY_PATH))
                Directory.Delete(DIRECTORY_PATH, true);
        }

        [Fact]
        public void Add_Should_ReturnTrue_And_SaveToCache_When_EntityIsValid()
        {
            // Arrange
            var savepoint = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            // Act
            var result = _savepointProvider.Add(savepoint);

            // Assert
            Assert.True(result);
            Assert.Contains(savepoint, _savepointProvider.Cache);
            Assert.Single(_savepointProvider.Cache);
        }

        [Fact]
        public void Add_Should_ThrowArgumentException_If_SavepointAlreadyExists()
        {
            // Arrange
            var savepoint = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };
            _savepointProvider.Add(savepoint);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _savepointProvider.Add(savepoint));
            Assert.Equal("Entity with such ID already exists!", exception.Message);
            Assert.Single(_savepointProvider.Cache);
        }

        [Fact]
        public void Add_Should_ThrowNullReferenceException_If_SavepointIsNull()
        {
            // Arrange
            Savepoint? savepoint = null;

            // Act & Assert
            var exception = Assert.Throws<NullReferenceException>(() => _savepointProvider.Add(savepoint!));
            Assert.Equal("Entity equals null.", exception.Message);
        }

        [Fact]
        public void Add_Should_ReturnFalse_If_MaxCacheCountReached()
        {
            // Arrange
            for (int i = 1; i <= 10; i++)
            {
                var savepoint = new Savepoint { Id = i, DashboardItems = new List<DashboardItem>() };
                _savepointProvider.Add(savepoint);
            }

            var extraSavepoint = new Savepoint { Id = 11, DashboardItems = new List<DashboardItem>() };

            // Act
            var result = _savepointProvider.Add(extraSavepoint);

            // Assert
            Assert.False(result);
            Assert.Equal(3, _savepointProvider.Cache.Count);
            Assert.DoesNotContain(extraSavepoint, _savepointProvider.Cache);
        }

        [Fact]
        public void Add_Should_AddToCache_When_SavepointIsValid()
        {
            // Arrange
            var savepoint = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            // Act
            var result = _savepointProvider.Add(savepoint);

            // Assert
            Assert.True(result);
            Assert.Contains(savepoint, _savepointProvider.Cache);
        }

        [Fact]
        public void Add_Should_RemoveOldestSavepoint_When_CacheLimitIsExceeded()
        {
            // Arrange
            for (int i = 1; i <= 3; i++) // 3 is MAX_CACHE_COUNT in SavepointProvider
            {
                _savepointProvider.Add(new Savepoint { Id = i, DashboardItems = new List<DashboardItem>() });
            }

            var newSavepoint = new Savepoint { Id = 4, DashboardItems = new List<DashboardItem>() };

            // Act
            _savepointProvider.Add(newSavepoint);

            // Assert
            Assert.Equal(3, _savepointProvider.Cache.Count);
            Assert.DoesNotContain(_savepointProvider.Cache, s => s.Id == 1); // Oldest element removed
            Assert.Contains(newSavepoint, _savepointProvider.Cache); // New element added
        }

        [Fact]
        public void Add_Should_NotAddToCache_If_SavepointIsNotSaved()
        {
            // Arrange
            var savepoint = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };
            var mockRepository = new Mock<IRepository<Savepoint>>();
            mockRepository.Setup(repo => repo.Add(savepoint)).Returns(false); // Simulate save failure
            var provider = new SavepointProvider(mockRepository.Object);

            // Act
            var result = provider.Add(savepoint);

            // Assert
            Assert.False(result);
            Assert.Empty(provider.Cache);
        }

        [Fact]
        public void Should_AddMultipleUniqueSavepoints_When_ValidSavepointsProvided()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 2, DashboardItems = new List<DashboardItem>() };

            // Act
            _savepointProvider.Add(savepoint1);
            _savepointProvider.Add(savepoint2);

            // Assert
            Assert.Contains(savepoint1, _savepointProvider.Cache);
            Assert.Contains(savepoint2, _savepointProvider.Cache);
            Assert.Equal(2, _savepointProvider.Cache.Count);
        }

        [Fact]
        public void Should_AddSavepointWithEmptyDashboardItems_When_EmptyDashboardItemsProvided()
        {
            // Arrange
            var savepoint = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };

            // Act
            var result = _savepointProvider.Add(savepoint);

            // Assert
            Assert.True(result);
            Assert.Contains(savepoint, _savepointProvider.Cache);
        }

        [Fact]
        public void Should_AddSavepointsInCorrectOrder_When_MultipleSavepointsAdded()
        {
            // Arrange
            var savepoint1 = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };
            var savepoint2 = new Savepoint { Id = 2, DashboardItems = new List<DashboardItem>() };

            // Act
            _savepointProvider.Add(savepoint1);
            _savepointProvider.Add(savepoint2);

            // Assert
            Assert.Equal(2, _savepointProvider.Cache.Count);
            Assert.Equal(savepoint1, _savepointProvider.Cache.First());
            Assert.Equal(savepoint2, _savepointProvider.Cache.Last());
        }

        [Fact]
        public void Should_SaveAllSavepointFieldsCorrectly_When_DashboardItemsProvided()
        {
            // Arrange
            var dashboardItems = new List<DashboardItem>
            {
                new DashboardItem { Value = 1, Notes = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 } },
                new DashboardItem { Value = 2, Notes = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 } }
            };

            var savepoint = new Savepoint { Id = 1, DashboardItems = dashboardItems };

            // Act
            _savepointProvider.Add(savepoint);

            // Assert
            var savedSavepoint = _savepointProvider.Cache.First();
            Assert.Equal(1, savedSavepoint.Id);

            Assert.Equal(2, savedSavepoint.DashboardItems.Count);

            var savedItem1 = savedSavepoint.DashboardItems[0];
            Assert.Equal(1, savedItem1.Value);
            Assert.True(savedItem1.Notes.SequenceEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));

            var savedItem2 = savedSavepoint.DashboardItems[1];
            Assert.Equal(2, savedItem2.Value);
            Assert.True(savedItem2.Notes.SequenceEqual(new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 }));
        }

        [Fact]
        public void Delete_Should_RemoveSavepointFromCache_When_ExistsInCache()
        {
            // Arrange
            var savepoint = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };
            _savepointProvider.Add(savepoint);

            // Act
            _savepointProvider.Delete(1);

            // Assert
            Assert.Empty(_savepointProvider.Cache);
        }

        [Fact]
        public void Delete_Should_ThrowArgumentException_When_SavepointNotFound_And_CheckCache()
        {
            // Arrange
            var savepoint = new Savepoint { Id = 1, DashboardItems = new List<DashboardItem>() };
            _savepointProvider.Add(savepoint);
            _savepointProvider.Delete(1);

            // Act & Assert
            var exception = Assert.Throws<NullReferenceException>(() => _savepointProvider.Delete(2));
            Assert.Equal("Entity with such ID doesn't exist.", exception.Message);

            // Assert
            Assert.Empty(_savepointProvider.Cache);
        }

        [Fact]
        public void Delete_Should_ThrowArgumentException_When_DeletingNonExistentID_And_CheckCache()
        {
            // Arrange

            // Act & Assert
            var exception = Assert.Throws<NullReferenceException>(() => _savepointProvider.Delete(99));
            Assert.Equal("Entity with such ID doesn't exist.", exception.Message);

            // Assert
            Assert.Empty(_savepointProvider.Cache);
        }

        [Fact]
        public void Delete_Should_ThrowArgumentException_When_DeletingNegativeID_And_CheckCache()
        {
            // Arrange
            for (int i = 1; i <= 5; i++)
                _savepointProvider.Add(new Savepoint { Id = i, DashboardItems = new List<DashboardItem>() });

            // Act & Assert
            var exception = Assert.Throws<NullReferenceException>(() => _savepointProvider.Delete(-1));
            Assert.Equal("Entity with such ID doesn't exist.", exception.Message);

            // Assert
            Assert.Equal(3, _savepointProvider.Cache.Count);
        }

        [Fact]
        public void Delete_Should_ThrowArgumentException_When_DeletingVeryLargeID_And_CheckCache()
        {
            // Arrange
            for (int i = 1; i <= 5; i++)
                _savepointProvider.Add(new Savepoint { Id = i, DashboardItems = new List<DashboardItem>() });

            // Act & Assert
            var exception = Assert.Throws<NullReferenceException>(() => _savepointProvider.Delete(9999));
            Assert.Equal("Entity with such ID doesn't exist.", exception.Message);

            // Assert
            Assert.Equal(3, _savepointProvider.Cache.Count);
        }

        [Fact]
        public void Delete_Should_RemoveLastElement_And_CheckCache()
        {
            // Arrange
            for (int i = 1; i <= 10; i++)
                _savepointProvider.Add(new Savepoint { Id = i, DashboardItems = new List<DashboardItem>() });

            // Act
            _savepointProvider.Delete(10);

            // Assert
            Assert.Equal(2, _savepointProvider.Cache.Count);
            var exception = Assert.Throws<NullReferenceException>(() => _savepointProvider.GetById(10));
            Assert.Equal("Entity with such ID doesn't exist.", exception.Message);
            Assert.DoesNotContain(_savepointProvider.Cache, sp => sp.Id == 10);
        }

        [Fact]
        public void Delete_Should_RemoveFirstElement_And_CheckCache()
        {
            // Arrange
            for (int i = 1; i <= 10; i++)
                _savepointProvider.Add(new Savepoint { Id = i, DashboardItems = new List<DashboardItem>() });

            // Act
            _savepointProvider.Delete(1);

            // Assert
            Assert.Equal(3, _savepointProvider.Cache.Count);
            var exception = Assert.Throws<NullReferenceException>(() => _savepointProvider.GetById(1));
            Assert.Equal("Entity with such ID doesn't exist.", exception.Message);
            Assert.DoesNotContain(_savepointProvider.Cache, sp => sp.Id == 1);
        }
    }
}
