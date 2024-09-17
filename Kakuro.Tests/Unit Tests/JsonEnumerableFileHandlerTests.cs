﻿using Kakuro.Data_Access;
using Kakuro.Models;
using System.Text.Json;

namespace Kakuro.Tests.Unit_Tests.Functionality_tests.Data_Access
{
    public class JsonEnumerableFileHandlerTests : IDisposable
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Unit Tests\\Files\\JsonEnumerableFileHandlerTests\\";

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
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<int>();
            var dataToSave = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            jsonEnumerableFileHandler.Save(dataToSave, filepath);
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
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<int>();
            var dataToSave = new List<int> { 1, 2, 3, 4, 5 };
            string expectedData = JsonSerializer.Serialize(dataToSave), actualData = "Smth";

            // Act
            try
            {
                jsonEnumerableFileHandler.Save(dataToSave, filepath);
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
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<int>();
            var dataToSave = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            jsonEnumerableFileHandler.Save(dataToSave, filepath);
            var loadedData = jsonEnumerableFileHandler.Load(filepath);

            // Assert
            Assert.Equivalent(dataToSave, loadedData);
        }

        [Fact]
        public void Should_CorrectlyDeserialize_When_FileContainsValidJson()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_ValidData.json");
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<TestPerson>();

            var peopleToSave = new List<TestPerson>
            {
            new TestPerson { Id = 1, Name = "Alice", Money = 1000.0, Description = "Description A", Time = DateTime.Now },
            new TestPerson { Id = 2, Name = "Bob", Money = 1500.0, Description = "Description B", Time = DateTime.Now }
            };

            jsonEnumerableFileHandler.Save(peopleToSave, filepath);

            // Act
            var loadedPeople = jsonEnumerableFileHandler.Load(filepath);

            // Assert
            Assert.Equivalent(peopleToSave, loadedPeople);
        }

        [Fact]
        public void Should_ReturnEmpty_When_FileDoesNotExist()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_NonExistingFile.json");
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<TestPerson>();

            // Act
            var loadedPeople = jsonEnumerableFileHandler.Load(filepath);

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
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<TestPerson>();

            // Act
            var loadedPeople = jsonEnumerableFileHandler.Load(filepath);

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
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<TestPerson>();

            // Act
            var loadedPeople = jsonEnumerableFileHandler.Load(filepath);

            // Assert
            Assert.Empty(loadedPeople);
        }

        [Fact]
        public void Should_SaveEmptyData_When_EmptyCollectionIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_EmptyData.json");
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<TestPerson>();

            var emptyList = new List<TestPerson>();

            // Act
            jsonEnumerableFileHandler.Save(emptyList, filepath);

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
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<TestPerson>();

            // Act
            var loadedPeople = jsonEnumerableFileHandler.Load(filepath);

            // Assert
            Assert.Empty(loadedPeople);
        }

        [Fact]
        public void Should_SaveLargeData_When_LargeCollectionIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_LargeData.json");
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<TestPerson>();

            var largeList = new List<TestPerson>();
            for (int i = 0; i < 10000; i++)
            {
                largeList.Add(new TestPerson { Id = i, Name = $"Person {i}", Money = i * 10.0, Description = $"Description {i}", Time = DateTime.Now });
            }

            // Act
            jsonEnumerableFileHandler.Save(largeList, filepath);

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
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<TestPerson>();

            var dataWithLargeNumbersAndDates = new List<TestPerson>
            {
                new TestPerson { Id = int.MaxValue, Name = "Max Int", Money = double.MaxValue, Description = "Max values", Time = DateTime.MaxValue },
                new TestPerson { Id = int.MinValue, Name = "Min Int", Money = double.MinValue, Description = "Min values", Time = DateTime.MinValue }
            };

            // Act
            jsonEnumerableFileHandler.Save(dataWithLargeNumbersAndDates, filepath);

            var savedContent = File.ReadAllText(filepath);
            var deserializedPeople = JsonSerializer.Deserialize<List<TestPerson>>(savedContent);

            // Assert
            Assert.Equivalent(dataWithLargeNumbersAndDates, deserializedPeople);
        }

        [Fact]
        public void Should_NotSaveData_When_NullDataIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_NullData.json");
            Directory.CreateDirectory(DIRECTORY_PATH);
            File.WriteAllText(filepath, string.Empty);

            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<TestPerson>();

            // Act
            try
            {
                jsonEnumerableFileHandler.Save(null, filepath);
            }
            catch (ArgumentNullException)
            {
                // Ignoring exception, because we just want to check the file after catching it
            }

            // Assert
            var fileContent = File.ReadAllText(filepath);
            Assert.Empty(fileContent);
        }

        [Fact]
        public void Should_SaveDataWithSpecialCharacters_When_SpecialCharactersArePresent()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_SpecialCharsData.json");
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<TestPerson>();

            var specialCharsList = new List<TestPerson>
            {
                new TestPerson { Id = 1, Name = "Alice", Money = 1000.0, Description = "Special Characters: !@#$%^&*()", Time = DateTime.Now },
                new TestPerson { Id = 2, Name = "Bob", Money = 1500.0, Description = "New Test Person with Special Characters", Time = DateTime.Now }
            };

            // Act
            jsonEnumerableFileHandler.Save(specialCharsList, filepath);
            var loadedData = jsonEnumerableFileHandler.Load(filepath);

            // Assert
            Assert.Equivalent(specialCharsList, loadedData);
        }

        [Fact]
        public void Should_CorrectlyDeserialize_When_FileContainsValidRatingRecordJson()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_ValidRatingRecordData.json");
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<RatingRecord>();

            var ratingRecordsToSave = new List<RatingRecord>
            {
                new RatingRecord { GameCompletionTime = new TimeOnly(10, 30, 45), GameCompletionDate = new DateOnly(2023, 9, 15) },
                new RatingRecord { GameCompletionTime = new TimeOnly(14, 20, 30), GameCompletionDate = new DateOnly(2024, 1, 10) }
            };

            jsonEnumerableFileHandler.Save(ratingRecordsToSave, filepath);

            // Act
            var loadedRatingRecords = jsonEnumerableFileHandler.Load(filepath);

            // Assert
            Assert.Equivalent(ratingRecordsToSave, loadedRatingRecords);
        }

        [Fact]
        public void Should_CorrectlyDeserialize_When_FileContainsValidSavepointJson()
        {
            // Arrange
            var filepath = Path.Combine(DIRECTORY_PATH, "Test_ValidSavepointData.json");
            var jsonEnumerableFileHandler = new JsonEnumerableFileHandler<Savepoint>();

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

            jsonEnumerableFileHandler.Save(savepointsToSave, filepath);

            // Act
            var loadedSavepoints = jsonEnumerableFileHandler.Load(filepath);

            // Assert
            Assert.Equivalent(savepointsToSave, loadedSavepoints);
        }
    }
}