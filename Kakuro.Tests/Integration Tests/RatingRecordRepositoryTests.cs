using Kakuro.Data_Access;
using Kakuro.Models;

namespace Kakuro.Tests.Integration_Tests
{
    public class RatingRecordRepositoryTests
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Integration Tests\\Files\\RatingRecordRepositoryTests\\";
        private JsonEnumerableFileHandler<RatingRecord> _jsonEnumerableFileHandler;
        private RatingRecordRepository _ratingRecordRepository;

        public RatingRecordRepositoryTests()
        {
            _jsonEnumerableFileHandler = new JsonEnumerableFileHandler<RatingRecord>();
            _ratingRecordRepository = new RatingRecordRepository(_jsonEnumerableFileHandler);
        }


    }
}
