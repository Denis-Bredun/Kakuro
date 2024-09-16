using FluentAssertions;
using Kakuro.Interfaces.Data_Access;
using Moq;
using System.Text.Json;

namespace Kakuro.Tests.Unit_Tests.TDD.Data_Access
{
    public class JsonEnumerableFileHandlerTests
    {
        private static string _directorypath = "..\\..\\..\\TDD Tests Files\\JsonEnumerableFileHandlerTests\\";

        [Fact]
        public void Should_SaveDataToFile_When_InvokingMethod()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_Save_Data.json");
            Directory.CreateDirectory(_directorypath);
            var handler = new Mock<IJsonEnumerableFileHandler<int>>();
            var dataToSave = new List<int> { 1, 2, 3, 4, 5 };

            handler.Setup(h => h.Save(It.IsAny<IEnumerable<int>>(), It.IsAny<string>()))
                   .Callback<IEnumerable<int>, string>((data, path) =>
                   {
                       var jsonData = JsonSerializer.Serialize(data);
                       File.WriteAllText(path, jsonData);
                   });

            // Act
            handler.Object.Save(dataToSave, filepath);
            var savedContent = File.ReadAllText(filepath);

            // Assert
            var expectedJsonData = JsonSerializer.Serialize(dataToSave);
            savedContent.Should().Be(expectedJsonData);
        }

        [Fact]
        public void Should_NotSaveData_When_DirectoryDoesNotExist()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "NonExistingDirectory", "Test_Directory_Doesnt_Exist.json");
            var handler = new Mock<IJsonEnumerableFileHandler<int>>();
            var dataToSave = new List<int> { 1, 2, 3, 4, 5 };

            handler.Setup(h => h.Save(It.IsAny<IEnumerable<int>>(), It.IsAny<string>()))
                   .Callback<IEnumerable<int>, string>((data, path) =>
                   {
                       var jsonData = JsonSerializer.Serialize(data);
                       File.WriteAllText(path, jsonData);
                   });

            string expectedData = "", actualData = "Smth";

            // Act
            try
            {
                handler.Object.Save(dataToSave, filepath);
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
            var filepath = Path.Combine(_directorypath, "Test_Json_Data.json");
            Directory.CreateDirectory(_directorypath);
            var handler = new Mock<IJsonEnumerableFileHandler<int>>();
            var dataToSave = new List<int> { 1, 2, 3, 4, 5 };

            handler.Setup(h => h.Save(It.IsAny<IEnumerable<int>>(), It.IsAny<string>()))
                   .Callback<IEnumerable<int>, string>((data, path) =>
                   {
                       var jsonData = JsonSerializer.Serialize(data);
                       File.WriteAllText(path, jsonData);
                   });

            handler.Setup(h => h.Load(It.IsAny<string>()))
                   .Returns<string>(path =>
                   {
                       if (!File.Exists(path)) return new List<int>();
                       var jsonData = File.ReadAllText(path);
                       return JsonSerializer.Deserialize<IEnumerable<int>>(jsonData) ?? new List<int>();
                   });

            // Act
            handler.Object.Save(dataToSave, filepath);
            var loadedData = handler.Object.Load(filepath);

            // Assert
            loadedData.Should().BeEquivalentTo(dataToSave);
        }

        [Fact]
        public void Should_CorrectlyDeserialize_When_FileContainsValidJson()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_ValidData.json");
            Directory.CreateDirectory(_directorypath);
            var handler = new Mock<IJsonEnumerableFileHandler<TestPerson>>();

            var peopleToSave = new List<TestPerson>
        {
            new TestPerson { Id = 1, Name = "Alice", Money = 1000.0, Description = "Description A", Time = DateTime.Now },
            new TestPerson { Id = 2, Name = "Bob", Money = 1500.0, Description = "Description B", Time = DateTime.Now }
        };

            handler.Setup(h => h.Save(It.IsAny<IEnumerable<TestPerson>>(), It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            handler.Setup(h => h.Load(It.IsAny<string>()))
                .Returns<string>(path =>
                {
                    var jsonData = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<IEnumerable<TestPerson>>(jsonData) ?? new List<TestPerson>();
                });

            handler.Object.Save(peopleToSave, filepath);

            // Act
            var loadedPeople = handler.Object.Load(filepath);

            // Assert
            loadedPeople.Should().BeEquivalentTo(peopleToSave);
        }

        [Fact]
        public void Should_ReturnEmpty_When_FileDoesNotExist()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_NonExistingFile.json");
            var handler = new Mock<IJsonEnumerableFileHandler<TestPerson>>();

            handler.Setup(h => h.Load(It.IsAny<string>()))
                .Returns<string>(path =>
                {
                    if (!File.Exists(path))
                    {
                        return new List<TestPerson>();
                    }

                    var jsonData = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<IEnumerable<TestPerson>>(jsonData) ?? new List<TestPerson>();
                });

            // Act
            var loadedPeople = handler.Object.Load(filepath);

            // Assert
            loadedPeople.Should().BeEmpty();
        }

        [Fact]
        public void Should_ReturnEmpty_When_FileIsEmpty()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_EmptyFile.json");
            Directory.CreateDirectory(_directorypath);
            File.WriteAllText(filepath, string.Empty);
            var handler = new Mock<IJsonEnumerableFileHandler<TestPerson>>();

            handler.Setup(h => h.Load(It.IsAny<string>()))
                .Returns<string>(path =>
                {
                    var jsonData = File.ReadAllText(path);
                    return string.IsNullOrWhiteSpace(jsonData) ? new List<TestPerson>() : JsonSerializer.Deserialize<IEnumerable<TestPerson>>(jsonData);
                });

            // Act
            var loadedPeople = handler.Object.Load(filepath);

            // Assert
            loadedPeople.Should().BeEmpty();
        }

        [Fact]
        public void Should_ReturnEmpty_When_FileContainsInvalidJson()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_InvalidJson.json");
            Directory.CreateDirectory(_directorypath);
            File.WriteAllText(filepath, "{ invalid json }");
            var handler = new Mock<IJsonEnumerableFileHandler<TestPerson>>();

            handler.Setup(h => h.Load(It.IsAny<string>()))
                .Returns<string>(path =>
                {
                    try
                    {
                        var jsonData = File.ReadAllText(path);
                        return JsonSerializer.Deserialize<IEnumerable<TestPerson>>(jsonData) ?? new List<TestPerson>();
                    }
                    catch
                    {
                        return new List<TestPerson>();
                    }
                });

            // Act
            var loadedPeople = handler.Object.Load(filepath);

            // Assert
            loadedPeople.Should().BeEmpty();
        }

        [Fact]
        public void Should_SaveEmptyData_When_EmptyCollectionIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_EmptyData.json");
            Directory.CreateDirectory(_directorypath);
            var handler = new Mock<IJsonEnumerableFileHandler<TestPerson>>();

            var emptyList = new List<TestPerson>();

            handler.Setup(h => h.Save(It.IsAny<IEnumerable<TestPerson>>(), It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            // Act
            handler.Object.Save(emptyList, filepath);

            var savedContent = File.ReadAllText(filepath);
            var deserializedPeople = JsonSerializer.Deserialize<List<TestPerson>>(savedContent);

            // Assert
            deserializedPeople.Should().BeEmpty();
        }

        [Fact]
        public void Should_ReturnEmptyList_When_DirectoryDoesNotExist()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "NonExistingDirectory", "Test_File.json");
            var filesHandler = new Mock<IJsonEnumerableFileHandler<TestPerson>>();

            filesHandler.Setup(h => h.Load(It.IsAny<string>()))
                .Returns<string>(path =>
                {
                    var directory = Path.GetDirectoryName(path);
                    if (!Directory.Exists(directory))
                        return new List<TestPerson>();

                    var jsonData = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<IEnumerable<TestPerson>>(jsonData);
                });

            // Act
            var loadedPeople = filesHandler.Object.Load(filepath);

            // Assert
            loadedPeople.Should().BeEmpty();
        }

        [Fact]
        public void Should_SaveLargeData_When_LargeCollectionIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_LargeData.json");
            Directory.CreateDirectory(_directorypath);
            var filesHandler = new Mock<IJsonEnumerableFileHandler<TestPerson>>();

            var largeList = new List<TestPerson>();
            for (int i = 0; i < 10000; i++)
            {
                largeList.Add(new TestPerson { Id = i, Name = $"Person {i}", Money = i * 10.0, Description = $"Description {i}", Time = DateTime.Now });
            }

            filesHandler.Setup(h => h.Save(It.IsAny<IEnumerable<TestPerson>>(), It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            // Act
            filesHandler.Object.Save(largeList, filepath);

            var savedContent = File.ReadAllText(filepath);
            var deserializedPeople = JsonSerializer.Deserialize<List<TestPerson>>(savedContent);

            // Assert
            deserializedPeople.Should().BeEquivalentTo(largeList);
        }

        [Fact]
        public void Should_SaveDataWithLargeNumbersAndDates_When_LargeNumbersAndDatesArePresent()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_LargeNumbersAndDates.json");
            Directory.CreateDirectory(_directorypath);
            var filesHandler = new Mock<IJsonEnumerableFileHandler<TestPerson>>();

            var dataWithLargeNumbersAndDates = new List<TestPerson>
        {
            new TestPerson { Id = int.MaxValue, Name = "Max Int", Money = double.MaxValue, Description = "Max values", Time = DateTime.MaxValue },
            new TestPerson { Id = int.MinValue, Name = "Min Int", Money = double.MinValue, Description = "Min values", Time = DateTime.MinValue }
        };

            filesHandler.Setup(h => h.Save(It.IsAny<IEnumerable<TestPerson>>(), It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            // Act
            filesHandler.Object.Save(dataWithLargeNumbersAndDates, filepath);

            var savedContent = File.ReadAllText(filepath);
            var deserializedPeople = JsonSerializer.Deserialize<List<TestPerson>>(savedContent);

            // Assert
            deserializedPeople.Should().BeEquivalentTo(dataWithLargeNumbersAndDates);
        }

        [Fact]
        public void Should_NotSaveData_When_NullDataIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_NullData.json");
            Directory.CreateDirectory(_directorypath);
            File.WriteAllText(filepath, string.Empty);

            var filesHandler = new Mock<IJsonEnumerableFileHandler<TestPerson>>();

            filesHandler.Setup(h => h.Save(It.IsAny<IEnumerable<TestPerson>>(), It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    if (data == null)
                        throw new ArgumentNullException(nameof(data));

                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            // Act
            try
            {
                filesHandler.Object.Save(null, filepath);
            }
            catch (ArgumentNullException)
            {
                // Ignoring exception, because we just want to check the file after catching it
            }

            // Assert
            var fileContent = File.ReadAllText(filepath);
            fileContent.Should().Be(string.Empty);
        }

        [Fact]
        public void Should_SaveDataWithSpecialCharacters_When_SpecialCharactersArePresent()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_SpecialCharsData.json");
            Directory.CreateDirectory(_directorypath);
            var handler = new Mock<IJsonEnumerableFileHandler<TestPerson>>();

            var specialCharsList = new List<TestPerson>
        {
            new TestPerson { Id = 1, Name = "Alice", Money = 1000.0, Description = "Special Characters: !@#$%^&*()", Time = DateTime.Now },
            new TestPerson { Id = 2, Name = "Bob", Money = 1500.0, Description = "New Test Person with Special Characters", Time = DateTime.Now }
        };

            handler.Setup(h => h.Save(It.IsAny<IEnumerable<TestPerson>>(), It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            handler.Setup(h => h.Load(It.IsAny<string>()))
                .Returns<string>(path =>
                {
                    var jsonData = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<IEnumerable<TestPerson>>(jsonData) ?? new List<TestPerson>();
                });

            // Act
            handler.Object.Save(specialCharsList, filepath);
            var loadedData = handler.Object.Load(filepath);

            // Assert
            loadedData.Should().BeEquivalentTo(specialCharsList);
        }
    }
}
