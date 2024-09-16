using FluentAssertions;
using Kakuro.Interfaces.Data_Access;
using Moq;
using System.Text.Json;

namespace Kakuro.Tests.Unit_Tests.TDD.Data_Access
{
    public class FilesHandlerTests
    {
        private static string _directorypath = "..\\..\\..\\TDD Tests Files\\";

        [Fact]
        public void Should_SaveDataToFile_When_InvokingMethod()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_Saving_Data.txt");
            Directory.CreateDirectory(_directorypath);
            var filesHandler = new Mock<IFilesHandler<string, string>>();
            var dataToSave = "Test Data #1\n Test Data #2 =)";

            filesHandler.Setup(h => h.Save(It.IsAny<string>(), It.IsAny<string>()))
                   .Callback<string, string>((data, filepath) =>
                   {
                       File.WriteAllText(filepath, data);
                   });

            // Act
            filesHandler.Object.Save(dataToSave, filepath);
            var savedContent = File.ReadAllText(filepath);

            // Assert
            savedContent.Should().Be(dataToSave);
        }

        [Fact]
        public void Should_ThrowException_When_DirectoryDoesNotExist()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "NonExistingDirectory", "Test_Directory_Doesnt_Exist.txt");
            var filesHandler = new Mock<IFilesHandler<string, string>>();
            var dataToSave = "Test Data #1\n Test Data #2 =)";

            filesHandler.Setup(h => h.Save(It.IsAny<string>(), It.IsAny<string>()))
                   .Callback<string, string>((data, path) =>
                   {
                       File.WriteAllText(path, data);
                   });

            string expectedData = "", actualData = "Smth";

            // Act
            try
            {
                filesHandler.Object.Save(dataToSave, filepath);
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
            var filesHandler = new Mock<IFilesHandler<IEnumerable<int>, string>>();
            var dataToSave = new List<int> { 1, 2, 3, 4, 5 };

            filesHandler.Setup(h => h.Save(It.IsAny<IEnumerable<int>>(), It.IsAny<string>()))
                   .Callback<IEnumerable<int>, string>((data, path) =>
                   {
                       var jsonData = JsonSerializer.Serialize(data);
                       File.WriteAllText(path, jsonData);
                   });

            // Act
            filesHandler.Object.Save(dataToSave, filepath);
            var savedContent = File.ReadAllText(filepath);

            // Assert
            var expectedJsonData = JsonSerializer.Serialize(dataToSave);
            savedContent.Should().Be(expectedJsonData);
        }

        [Fact]
        public void Should_CorrectDeserialize_When_InvokingMethodWithListOfObjects()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_Person_List.json");
            Directory.CreateDirectory(_directorypath);
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

            var peopleToSave = new List<TestPerson>
            {
                new TestPerson
                {
                    Id = 1,
                    Name = "Alice",
                    Money = 1234.56,
                    Description = "Experienced software engineer with a knack for problem-solving.",
                    Time = new DateTime(2023, 5, 1, 10, 30, 0)
                },
                new TestPerson
                {
                    Id = 2,
                    Name = "Bob",
                    Money = 7890.12,
                    Description = "Project manager with a background in agile methodologies and team leadership.",
                    Time = new DateTime(2022, 11, 23, 14, 45, 0)
                },
                new TestPerson
                {
                    Id = 3,
                    Name = "Charlie",
                    Money = 567.89,
                    Description = "Junior developer with a passion for learning new technologies and coding best practices.",
                    Time = new DateTime(2024, 1, 10, 9, 15, 0)
                },
                new TestPerson
                {
                    Id = 4,
                    Name = "Diana",
                    Money = 3456.78,
                    Description = "UI/UX designer with expertise in user-centered design and wireframing.",
                    Time = new DateTime(2023, 7, 25, 16, 0, 0)
                },
                new TestPerson
                {
                    Id = 5,
                    Name = "Eve",
                    Money = 91011.12,
                    Description = "Data scientist specializing in machine learning and statistical analysis.",
                    Time = new DateTime(2022, 12, 5, 11, 45, 0)
                }
            };

            filesHandler.Setup(h => h.Save(peopleToSave, It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            // Act
            filesHandler.Object.Save(peopleToSave, filepath);

            var savedContent = File.ReadAllText(filepath);
            var deserializedPeople = JsonSerializer.Deserialize<List<TestPerson>>(savedContent);

            // Assert
            deserializedPeople.Should().BeEquivalentTo(peopleToSave);
        }

        [Fact]
        public void Should_SaveEmptyData_When_EmptyCollectionIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_EmptyData.json");
            Directory.CreateDirectory(_directorypath);
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

            var emptyList = new List<TestPerson>();

            filesHandler.Setup(h => h.Save(It.IsAny<IEnumerable<TestPerson>>(), It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            // Act
            filesHandler.Object.Save(emptyList, filepath);

            var savedContent = File.ReadAllText(filepath);
            var deserializedPeople = JsonSerializer.Deserialize<List<TestPerson>>(savedContent);

            // Assert
            deserializedPeople.Should().BeEmpty();
        }

        [Fact]
        public void Should_ThrowArgumentNullException_When_NullDataIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_NullData.json");
            Directory.CreateDirectory(_directorypath);
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

            filesHandler.Setup(h => h.Save(It.IsAny<IEnumerable<TestPerson>>(), It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    if (data == null)
                        throw new ArgumentNullException(nameof(data));

                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            IEnumerable<TestPerson> actualData = null, expectedData = new List<TestPerson>();

            // Act
            try
            {
                filesHandler.Object.Save(null, filepath);
            }
            catch (Exception)
            {
                actualData = new List<TestPerson>();
            }

            // Assert
            actualData.Should().BeEquivalentTo(expectedData);
        }

        [Fact]
        public void Should_SaveDataWithSpecialCharacters_When_SpecialCharactersArePresent()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_SpecialCharsData.json");
            Directory.CreateDirectory(_directorypath);
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

            var specialCharsList = new List<TestPerson>
            {
                new TestPerson { Id = 1, Name = "Alice", Money = 1000.0, Description = "Special Characters: !@#$%^&*()", Time = DateTime.Now },
                new TestPerson { Id = 2, Name = "Bob", Money = 1500.0, Description = "New Line: \nTab: \t", Time = DateTime.Now }
            };

            filesHandler.Setup(h => h.Save(It.IsAny<IEnumerable<TestPerson>>(), It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            // Act
            filesHandler.Object.Save(specialCharsList, filepath);

            var savedContent = File.ReadAllText(filepath);
            var deserializedPeople = JsonSerializer.Deserialize<List<TestPerson>>(savedContent);

            // Assert
            deserializedPeople.Should().BeEquivalentTo(specialCharsList);
        }

        [Fact]
        public void Should_SaveDataWithLargeNumbersAndDates_When_LargeNumbersAndDatesArePresent()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_LargeNumbersAndDates.json");
            Directory.CreateDirectory(_directorypath);
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

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
        public void Should_SaveLargeData_When_LargeCollectionIsPassed()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_LargeData.json");
            Directory.CreateDirectory(_directorypath);
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

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
        public void Should_LoadData_When_FileContainsValidJson()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_Loading_ValidData.json");
            Directory.CreateDirectory(_directorypath);
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

            var peopleToSave = new List<TestPerson>
            {
                new TestPerson { Id = 1, Name = "Alice", Money = 1000.0, Description = "Description A", Time = DateTime.Now },
                new TestPerson { Id = 2, Name = "Bob", Money = 1500.0, Description = "Description B", Time = DateTime.Now }
            };

            filesHandler.Setup(h => h.Save(It.IsAny<IEnumerable<TestPerson>>(), It.IsAny<string>()))
                .Callback<IEnumerable<TestPerson>, string>((data, path) =>
                {
                    var jsonData = JsonSerializer.Serialize(data);
                    File.WriteAllText(path, jsonData);
                });

            filesHandler.Setup(h => h.Load(It.IsAny<string>()))
                .Returns<string>(path =>
                {
                    var jsonData = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<IEnumerable<TestPerson>>(jsonData);
                });

            filesHandler.Object.Save(peopleToSave, filepath);

            // Act
            var loadedPeople = filesHandler.Object.Load(filepath);

            // Assert
            loadedPeople.Should().BeEquivalentTo(peopleToSave);
        }

        [Fact]
        public void Should_ReturnEmpty_When_FileDoesNotExist()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_NonExistingFile.json");
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

            filesHandler.Setup(h => h.Load(It.IsAny<string>()))
                .Returns<string>(path =>
                {
                    if (!File.Exists(path))
                    {
                        return new List<TestPerson>();
                    }

                    var jsonData = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<IEnumerable<TestPerson>>(jsonData);
                });

            // Act
            var loadedPeople = filesHandler.Object.Load(filepath);

            // Assert
            loadedPeople.Should().BeEmpty(); // Ensure that an empty list is returned
        }

        [Fact]
        public void Should_ReturnEmpty_When_FileIsEmpty()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "Test_EmptyFile.json");
            Directory.CreateDirectory(_directorypath);
            File.WriteAllText(filepath, string.Empty); // Create an empty file
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

            filesHandler.Setup(h => h.Load(It.IsAny<string>()))
                .Returns<string>(path =>
                {
                    var jsonData = File.ReadAllText(path);
                    if (string.IsNullOrWhiteSpace(jsonData))
                    {
                        return new List<TestPerson>();
                    }
                    return JsonSerializer.Deserialize<IEnumerable<TestPerson>>(jsonData);
                });

            // Act
            var loadedPeople = filesHandler.Object.Load(filepath);

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
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

            filesHandler.Setup(h => h.Load(It.IsAny<string>()))
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
            var loadedPeople = filesHandler.Object.Load(filepath);

            // Assert
            loadedPeople.Should().BeEmpty();
        }

        [Fact]
        public void Should_ReturnEmptyList_When_DirectoryDoesNotExist()
        {
            // Arrange
            var filepath = Path.Combine(_directorypath, "NonExistingDirectory", "Test_File.json");
            var filesHandler = new Mock<IFilesHandler<IEnumerable<TestPerson>, string>>();

            filesHandler.Setup(h => h.Load(It.IsAny<string>()))
                .Returns<string>(path =>
                {
                    // Check if the directory exists
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

        public class TestPerson
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Money { get; set; }
            public string Description { get; set; }
            public DateTime Time { get; set; }
        }
    }
}
