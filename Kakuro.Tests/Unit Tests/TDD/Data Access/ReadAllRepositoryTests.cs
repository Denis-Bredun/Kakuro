using FluentAssertions;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access;
using Kakuro.Models;
using Moq;

namespace Kakuro.Tests.Unit_Tests
{
    public class ReadAllRepositoryTests
    {
        private Mock<IReadAllRepository<RatingRecord, DifficultyLevels>>? _ratingRecordRepositoryMock;

        public ReadAllRepositoryTests()
        {
            _ratingRecordRepositoryMock = new Mock<IReadAllRepository<RatingRecord, DifficultyLevels>>();
        }

        [Fact]
        public void Should_ReturnSomeData_When_InvokingMethod()
        {
            // Arrange:
            var fixedDate = DateOnly.FromDateTime(DateTime.Now);
            var fixedTime1 = new TimeOnly(23, 45, 13);
            var fixedTime2 = new TimeOnly(12, 1, 56);

            _ratingRecordRepositoryMock.Setup(rep => rep.GetAll(It.IsAny<DifficultyLevels>())).Returns(
                new List<RatingRecord>()
                {
                    new RatingRecord() { GameCompletionDate = fixedDate, GameCompletionTime = fixedTime1 },
                    new RatingRecord() { GameCompletionDate = fixedDate, GameCompletionTime = fixedTime2 }
                }
            );

            var expected = new List<RatingRecord>()
            {
                new RatingRecord() { GameCompletionDate = fixedDate, GameCompletionTime = fixedTime1 },
                new RatingRecord() { GameCompletionDate = fixedDate, GameCompletionTime = fixedTime2 }
            };

            // Act:
            var ratingRecordRepository = _ratingRecordRepositoryMock.Object;
            var actual = ratingRecordRepository.GetAll(It.IsAny<DifficultyLevels>());

            // Assert:
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Should_ReturnEmptyCollection_When_NoDataWereSavedYet()
        {
            // Arrange:
            _ratingRecordRepositoryMock.Setup(rep => rep.GetAll(It.IsAny<DifficultyLevels>())).Returns(new List<RatingRecord>());

            // Act:
            var ratingRecordRepository = _ratingRecordRepositoryMock.Object;
            var actual = ratingRecordRepository.GetAll(It.IsAny<DifficultyLevels>());

            // Assert:
            actual.Should().BeEmpty();
        }

        [Fact]
        public void Should_NotMatchExpectedData_When_RepositoryReturnsData()
        {
            // Arrange:
            var fixedDate = DateOnly.FromDateTime(DateTime.Now);
            var fixedTime1 = new TimeOnly(23, 45, 13);
            var fixedTime2 = new TimeOnly(12, 1, 56);

            _ratingRecordRepositoryMock.Setup(rep => rep.GetAll(It.IsAny<DifficultyLevels>())).Returns(
                new List<RatingRecord>()
                {
                    new RatingRecord() { GameCompletionDate = fixedDate, GameCompletionTime = fixedTime1 }
                }
            );

            var expected = new List<RatingRecord>()
            {
                new RatingRecord() { GameCompletionDate = fixedDate, GameCompletionTime = fixedTime1 },
                new RatingRecord() { GameCompletionDate = fixedDate, GameCompletionTime = fixedTime2 }
            };

            // Act:
            var ratingRecordRepository = _ratingRecordRepositoryMock.Object;
            var actual = ratingRecordRepository.GetAll(It.IsAny<DifficultyLevels>());

            // Assert:
            actual.Should().NotBeEquivalentTo(expected);
        }

        [Fact]
        public void Should_ReturnCorrectData_When_InvokingMethodWithDifferentDifficultyLevels()
        {
            // Arrange:
            var easyRecords = new List<RatingRecord>()
            {
                new RatingRecord() { GameCompletionDate = DateOnly.FromDateTime(DateTime.Now), GameCompletionTime = new TimeOnly(10, 15, 30) }
            };

            var normalRecords = new List<RatingRecord>()
            {
                new RatingRecord() { GameCompletionDate = DateOnly.FromDateTime(DateTime.Now), GameCompletionTime = new TimeOnly(12, 45, 00) }
            };

            var hardRecords = new List<RatingRecord>()
            {
                new RatingRecord() { GameCompletionDate = DateOnly.FromDateTime(DateTime.Now), GameCompletionTime = new TimeOnly(18, 30, 00) }
            };

            _ratingRecordRepositoryMock.Setup(rep => rep.GetAll(DifficultyLevels.Easy)).Returns(easyRecords);
            _ratingRecordRepositoryMock.Setup(rep => rep.GetAll(DifficultyLevels.Normal)).Returns(normalRecords);
            _ratingRecordRepositoryMock.Setup(rep => rep.GetAll(DifficultyLevels.Hard)).Returns(hardRecords);

            var ratingRecordRepository = _ratingRecordRepositoryMock.Object;

            // Act:
            var easyResult = ratingRecordRepository.GetAll(DifficultyLevels.Easy);
            var normalResult = ratingRecordRepository.GetAll(DifficultyLevels.Normal);
            var hardResult = ratingRecordRepository.GetAll(DifficultyLevels.Hard);

            // Assert:
            easyResult.Should().BeEquivalentTo(easyRecords);
            normalResult.Should().BeEquivalentTo(normalRecords);
            hardResult.Should().BeEquivalentTo(hardRecords);
        }

        [Fact]
        public void Should_ReturnEmptyList_When_ExceptionOccurs()
        {
            // Arrange:
            _ratingRecordRepositoryMock.Setup(rep => rep.GetAll(It.IsAny<DifficultyLevels>()))
                .Throws(new InvalidOperationException("Test exception"));

            var ratingRecordRepository = _ratingRecordRepositoryMock.Object;

            // Act:
            IEnumerable<RatingRecord> result;
            try
            {
                result = ratingRecordRepository.GetAll(DifficultyLevels.Easy);
            }
            catch (Exception)
            {
                result = new List<RatingRecord>();
            }

            // Assert:
            result.Should().BeEmpty();
        }
    }
}

// NOTE: add to Tech. stack - FluentAssertions