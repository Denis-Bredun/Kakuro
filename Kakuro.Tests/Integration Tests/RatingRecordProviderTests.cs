using Kakuro.Data_Access.Data_Providers;
using Kakuro.Data_Access.Repositories;
using Kakuro.Data_Access.Tools;
using Kakuro.Models;

namespace Kakuro.Tests.Integration_Tests
{
    public class RatingRecordProviderTests
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Integration Tests\\Files\\RatingRecordProviderTests\\";
        private JsonFileHandler<RatingRecord> _jsonFileHandler;
        private RatingRecordRepository _ratingRecordRepository;
        private RatingRecordProvider _ratingRecordProvider;

        public RatingRecordProviderTests()
        {
            _jsonFileHandler = new JsonFileHandler<RatingRecord>();
            _ratingRecordRepository = new RatingRecordRepository(_jsonFileHandler, DIRECTORY_PATH);
            _ratingRecordProvider = new RatingRecordProvider(_ratingRecordRepository);
        }

        public void Dispose()
        {
            if (Directory.Exists(DIRECTORY_PATH))
                Directory.Delete(DIRECTORY_PATH, true);
        }


    }
}
