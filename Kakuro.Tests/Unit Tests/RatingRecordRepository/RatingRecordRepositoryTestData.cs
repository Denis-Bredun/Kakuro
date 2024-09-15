using Kakuro.Enums;
using Kakuro.Models;

namespace Kakuro.Tests.Unit_Tests.RatingRecordRepository
{
    public class RatingRecordRepositoryTestData
    {
        public static IEnumerable<object[]> GetAllMethod_TestData()
        {
            var expectedRatingRecordsOnEasyLevel = new List<RatingRecord>();
            var expectedRatingRecordsOnNormalLevel = new List<RatingRecord>();
            var expectedRatingRecordsOnHardLevel = new List<RatingRecord>();

            var actualRatingRecordsOnEasyLevel = new List<RatingRecord>();
            var actualRatingRecordsOnNormalLevel = new List<RatingRecord>();
            var actualRatingRecordsOnHardLevel = new List<RatingRecord>();

            expectedRatingRecordsOnEasyLevel.Add(new RatingRecord() { GameCompletionTime = new TimeOnly(13, 45, 23), GameCompletionDate = DateOnly.FromDateTime(DateTime.Now) });
            expectedRatingRecordsOnNormalLevel.Add(new RatingRecord() { GameCompletionTime = new TimeOnly(1, 59, 59), GameCompletionDate = DateOnly.FromDateTime(DateTime.Now) });
            expectedRatingRecordsOnHardLevel.Add(new RatingRecord() { GameCompletionTime = new TimeOnly(21, 31, 2), GameCompletionDate = DateOnly.FromDateTime(DateTime.Now) });

            actualRatingRecordsOnEasyLevel.Add(new RatingRecord() { GameCompletionTime = new TimeOnly(13, 45, 23), GameCompletionDate = DateOnly.FromDateTime(DateTime.Now) });
            actualRatingRecordsOnNormalLevel.Add(new RatingRecord() { GameCompletionTime = new TimeOnly(1, 59, 59), GameCompletionDate = DateOnly.FromDateTime(DateTime.Now) });
            actualRatingRecordsOnHardLevel.Add(new RatingRecord() { GameCompletionTime = new TimeOnly(21, 31, 2), GameCompletionDate = DateOnly.FromDateTime(DateTime.Now) });

            var allData = new List<object[]>
            {
                new object[] { DifficultyLevels.Easy, expectedRatingRecordsOnEasyLevel, actualRatingRecordsOnEasyLevel },
                new object[] { DifficultyLevels.Normal, expectedRatingRecordsOnNormalLevel, actualRatingRecordsOnNormalLevel },
                new object[] { DifficultyLevels.Hard, expectedRatingRecordsOnHardLevel, actualRatingRecordsOnHardLevel }
            };

            return allData;
        }
    }
}
