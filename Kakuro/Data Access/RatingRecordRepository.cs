using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access;
using Kakuro.Models;
using System.IO;

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
            string filepath = FormFilepath(key);
            var ratingTableConcreteDifficulty = GetAll(key);
            ratingTableConcreteDifficulty.Append(entity);
            _jsonEnumerableFileHandler.Save(ratingTableConcreteDifficulty, filepath);
        }

        public IEnumerable<RatingRecord> GetAll(DifficultyLevels key)
        {
            string filepath = FormFilepath(key);
            return _jsonEnumerableFileHandler.Load(filepath);
        }

        private string FormFilepath(DifficultyLevels level) => Path.Combine(DIRECTORY_PATH, level.ToString() + PART_OF_FILEPATH);
    }
}
