using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access;
using Kakuro.Models;

namespace Kakuro.Data_Access
{
    public class RatingRecordRepository : IReadAllRepository<RatingRecord, DifficultyLevels>
    {
        private IJsonEnumerableFileHandler<RatingRecord> _jsonEnumerableFileHandler;
        private const string DIRECTORY_PATH = "Rating Tables\\";
        private const string PART_OF_FILEPATH = ". Rating Table.json";

        public RatingRecordRepository(IJsonEnumerableFileHandler<RatingRecord> jsonEnumerableFileHandler)
        {
            _jsonEnumerableFileHandler = jsonEnumerableFileHandler;
        }

        public void Add(RatingRecord entity, DifficultyLevels key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RatingRecord> GetAll(DifficultyLevels key)
        {
            throw new NotImplementedException();
        }
    }
}
