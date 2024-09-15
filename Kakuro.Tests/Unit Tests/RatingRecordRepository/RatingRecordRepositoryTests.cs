using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access;
using Kakuro.Models;
using Kakuro.Tests.Unit_Tests.RatingRecordRepository;
using Moq;

namespace Kakuro.Tests.Unit_Tests
{
    public class RatingRecordRepositoryTests
    {
        private static Mock<IReadAllRepository<RatingRecord, DifficultyLevels>> _ratingRecordRepositoryMock;

        public RatingRecordRepositoryTests()
        {
            _ratingRecordRepositoryMock ??= new Mock<IReadAllRepository<RatingRecord, DifficultyLevels>>();
        }

        // The first idea for test was to transmit 3 sets of data consisting of Difficulty level and expected collection.
        // So my test run 3 times. Then i wanted my Mock object to return 3 different data (in accordance with three different
        // expected collections) on these 3 times. So I tried SetupSequence. But it didn't work because it works if we 
        // use Mock several times for 1 run of method, and I have 3 runs of method and every time it's used 1 time.

        // Another solution for it is to put all test data and all test cases in the only one run of method.

        // But I thought that my method'd be overwhelmed in this case. That's why I decided to transmit also 
        // List<RatingRecord> actualData in parameters. I thought that i might not change anything if we actually have fake data.

        [Theory]
        [MemberData(nameof(RatingRecordRepositoryTestData.GetAllMethod_TestData), MemberType = typeof(RatingRecordRepositoryTestData))]
        public void Should_ReturnConcreteRatingRecords_When_ConveyingConcreteDifficulty
            (DifficultyLevels difficultyLevel, List<RatingRecord> expectedData, List<RatingRecord> actualData)
        {
            _ratingRecordRepositoryMock.Setup(obj => obj.GetAll(It.IsAny<DifficultyLevels>())).Returns(actualData);
            var realRecordRepository = _ratingRecordRepositoryMock.Object;

            var actualDataFromMethod = realRecordRepository.GetAll(difficultyLevel);

            Assert.Equivalent(expectedData, actualDataFromMethod);
        }
    }
}
