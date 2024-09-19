using Kakuro.Data_Access.Tools;
using Kakuro.Models;
using System.Text.Json;

namespace Kakuro.Tests.Unit_Tests
{
    public class JsonFileHandlerTests : IDisposable
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Unit Tests\\Files\\JsonFileHandlerTests\\";

        public void Dispose()
        {
            if (Directory.Exists(DIRECTORY_PATH))
                Directory.Delete(DIRECTORY_PATH, true);
        }

        [Fact]
        public void Should_SaveDataToFile_When_InvokingMethod()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_Save_Data.json");
            var jsonFileHandler = new JsonFileHandler<int>();
            var dataToSave = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            jsonFileHandler.Save(dataToSave, filepath);
            var savedContent = File.ReadAllText(filepath);

            // Assert
            var expectedJsonData = JsonSerializer.Serialize(dataToSave);
            Assert.Equal(expectedJsonData, savedContent);
        }

        [Fact]
        public void Should_CreateDirectory_When_DirectoryDoesNotExist()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "NonExistingDirectory", "Test_Directory_Doesnt_Exist.json");
            var jsonFileHandler = new JsonFileHandler<int>();
            var dataToSave = new List<int> { 1, 2, 3, 4, 5 };
            string expectedData = JsonSerializer.Serialize(dataToSave), actualData = "Smth";

            // Act
            try
            {
                jsonFileHandler.Save(dataToSave, filepath);
                actualData = JsonSerializer.Serialize(dataToSave);
            }
            catch (Exception)
            {
                actualData = "";
            }

            // Assert
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public void Should_SaveAndReadJsonData_When_InvokingMethodWithIEnumerable()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_Json_Data.json");
            var jsonFileHandler = new JsonFileHandler<int>();
            var dataToSave = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            jsonFileHandler.Save(dataToSave, filepath);
            var loadedData = jsonFileHandler.Load(filepath);

            // Assert
            Assert.Equivalent(dataToSave, loadedData);
        }

        [Fact]
        public void Should_CorrectlyDeserialize_When_FileContainsValidJson()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_ValidData.json");
            var jsonFileHandler = new JsonFileHandler<TestPerson>();

            var peopleToSave = new List<TestPerson>
            {
            new TestPerson { Id = 1, Name = "Alice", Money = 1000.0, Description = "Description A", Time = DateTime.Now },
            new TestPerson { Id = 2, Name = "Bob", Money = 1500.0, Description = "Description B", Time = DateTime.Now }
            };

            jsonFileHandler.Save(peopleToSave, filepath);

            // Act
            var loadedPeople = jsonFileHandler.Load(filepath);

            // Assert
            Assert.Equivalent(peopleToSave, loadedPeople);
        }

        [Fact]
        public void Should_ReturnEmpty_When_FileDoesNotExist()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_NonExistingFile.json");
            var jsonFileHandler = new JsonFileHandler<TestPerson>();

            // Act
            var loadedPeople = jsonFileHandler.Load(filepath);

            // Assert
            Assert.Empty(loadedPeople);
        }

        [Fact]
        public void Should_ReturnEmpty_When_FileIsEmpty()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_EmptyFile.json");
            Directory.CreateDirectory(DIRECTORY_PATH);
            File.WriteAllText(filepath, string.Empty);
            var jsonFileHandler = new JsonFileHandler<TestPerson>();

            // Act
            var loadedPeople = jsonFileHandler.Load(filepath);

            // Assert
            Assert.Empty(loadedPeople);
        }

        [Fact]
        public void Should_ReturnEmpty_When_FileContainsInvalidJson()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_InvalidJson.json");
            Directory.CreateDirectory(DIRECTORY_PATH);
            File.WriteAllText(filepath, "{ invalid json }");
            var jsonFileHandler = new JsonFileHandler<TestPerson>();

            // Act
            var loadedPeople = jsonFileHandler.Load(filepath);

            // Assert
            Assert.Empty(loadedPeople);
        }

        [Fact]
        public void Should_SaveEmptyData_When_EmptyCollectionIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_EmptyData.json");
            var jsonFileHandler = new JsonFileHandler<TestPerson>();

            var emptyList = new List<TestPerson>();

            // Act
            jsonFileHandler.Save(emptyList, filepath);

            var savedContent = File.ReadAllText(filepath);
            var deserializedPeople = JsonSerializer.Deserialize<List<TestPerson>>(savedContent);

            // Assert
            Assert.Empty(deserializedPeople);
        }

        [Fact]
        public void Should_ReturnEmptyList_When_DirectoryDoesNotExist()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "NonExistingDirectory", "Test_File.json");
            var jsonFileHandler = new JsonFileHandler<TestPerson>();

            // Act
            var loadedPeople = jsonFileHandler.Load(filepath);

            // Assert
            Assert.Empty(loadedPeople);
        }

        [Fact]
        public void Should_SaveLargeData_When_LargeCollectionIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_LargeData.json");
            var jsonFileHandler = new JsonFileHandler<TestPerson>();

            var largeList = new List<TestPerson>();
            for (int i = 0; i < 10000; i++)
            {
                largeList.Add(new TestPerson { Id = i, Name = $"Person {i}", Money = i * 10.0, Description = $"Description {i}", Time = DateTime.Now });
            }

            // Act
            jsonFileHandler.Save(largeList, filepath);

            var savedContent = File.ReadAllText(filepath);
            var deserializedPeople = JsonSerializer.Deserialize<List<TestPerson>>(savedContent);

            // Assert
            Assert.Equivalent(largeList, deserializedPeople);
        }

        [Fact]
        public void Should_SaveDataWithLargeNumbersAndDates_When_LargeNumbersAndDatesArePresent()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_LargeNumbersAndDates.json");
            var jsonFileHandler = new JsonFileHandler<TestPerson>();

            var dataWithLargeNumbersAndDates = new List<TestPerson>
            {
                new TestPerson { Id = int.MaxValue, Name = "Max Int", Money = double.MaxValue, Description = "Max values", Time = DateTime.MaxValue },
                new TestPerson { Id = int.MinValue, Name = "Min Int", Money = double.MinValue, Description = "Min values", Time = DateTime.MinValue }
            };

            // Act
            jsonFileHandler.Save(dataWithLargeNumbersAndDates, filepath);

            var savedContent = File.ReadAllText(filepath);
            var deserializedPeople = JsonSerializer.Deserialize<List<TestPerson>>(savedContent);

            // Assert
            Assert.Equivalent(dataWithLargeNumbersAndDates, deserializedPeople);
        }

        [Fact]
        public void Should_ThrowArgumentException_When_NullDataIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_NullData.json");
            Directory.CreateDirectory(DIRECTORY_PATH);
            File.WriteAllText(filepath, string.Empty);

            var jsonFileHandler = new JsonFileHandler<TestPerson>();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => jsonFileHandler.Save(null, filepath));
            Assert.Equal("Invalid data or filepath", exception.Message);
        }

        [Fact]
        public void Should_SaveDataWithSpecialCharacters_When_SpecialCharactersArePresent()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_SpecialCharsData.json");
            var jsonFileHandler = new JsonFileHandler<TestPerson>();

            var specialCharsList = new List<TestPerson>
            {
                new TestPerson { Id = 1, Name = "Alice", Money = 1000.0, Description = "Special Characters: !@#$%^&*()", Time = DateTime.Now },
                new TestPerson { Id = 2, Name = "Bob", Money = 1500.0, Description = "New Test Person with Special Characters", Time = DateTime.Now }
            };

            // Act
            jsonFileHandler.Save(specialCharsList, filepath);
            var loadedData = jsonFileHandler.Load(filepath);

            // Assert
            Assert.Equivalent(specialCharsList, loadedData);
        }

        [Fact]
        public void Should_CorrectlyDeserialize_When_FileContainsValidRatingRecordJson()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_ValidRatingRecordData.json");
            var jsonFileHandler = new JsonFileHandler<RatingRecord>();

            var ratingRecordsToSave = new List<RatingRecord>
            {
                new RatingRecord { GameCompletionTime = new TimeOnly(10, 30, 45), GameCompletionDate = new DateOnly(2023, 9, 15) },
                new RatingRecord { GameCompletionTime = new TimeOnly(14, 20, 30), GameCompletionDate = new DateOnly(2024, 1, 10) }
            };

            jsonFileHandler.Save(ratingRecordsToSave, filepath);

            // Act
            var loadedRatingRecords = jsonFileHandler.Load(filepath);

            // Assert
            Assert.Equivalent(ratingRecordsToSave, loadedRatingRecords);
        }

        [Fact]
        public void Should_CorrectlyDeserialize_When_FileContainsValidSavepointJson()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_ValidSavepointData.json");
            var jsonFileHandler = new JsonFileHandler<Savepoint>();

            var savepointsToSave = new List<Savepoint>
            {
                new Savepoint
                {
                    Id = 1,
                    DashboardItems = new List<DashboardItem>
                    {
                        new DashboardItem { Value = 100, Notes = [1, 2, 3] },
                        new DashboardItem { Value = 200, Notes = [4, 5, 6] }
                    }
                },
                new Savepoint
                {
                    Id = 2,
                    DashboardItems = new List<DashboardItem>
                    {
                        new DashboardItem { Value = null, Notes = [7, 8, 9] },
                        new DashboardItem { Value = 300, Notes = null }
                    }
                }
            };

            jsonFileHandler.Save(savepointsToSave, filepath);

            // Act
            var loadedSavepoints = jsonFileHandler.Load(filepath);

            // Assert
            Assert.Equivalent(savepointsToSave, loadedSavepoints);
        }

        [Fact]
        public void Should_ThrowArgumentNullException_When_FilePathIsNull()
        {
            // Arrange
            var jsonFileHandler = new JsonFileHandler<Savepoint>();
            var savepoints = new List<Savepoint>
            {
                new Savepoint
                {
                    Id = 0,
                    DashboardItems = new List<DashboardItem>
                    {
                        new DashboardItem { Value = 2, Notes = new[] { 1, 2, 3, 0, 0, 0, 0, 0, 0 } }
                    }
                }
            };

            string nullFilePath = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => jsonFileHandler.Save(savepoints, nullFilePath));

            // Assert
            Assert.Equal("Invalid data or filepath", exception.Message);
        }
    }
}
