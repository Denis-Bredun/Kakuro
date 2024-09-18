using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access;
using Kakuro.Models;
using System.IO;

namespace Kakuro.Data_Access
{
    public class RatingRecordRepository : IReadAllRepository<RatingRecord, DifficultyLevels>
    {
        private IJsonFileHandler<RatingRecord> _jsonEnumerableFileHandler;
        private readonly string _directoryPath;
        private const string PART_OF_FILEPATH = ". Rating Table.json";

        public RatingRecordRepository(IJsonFileHandler<RatingRecord> jsonEnumerableFileHandler, string directoryPath = "")
        {
            _jsonEnumerableFileHandler = jsonEnumerableFileHandler;
            _directoryPath = string.IsNullOrWhiteSpace(directoryPath) ? "Rating Tables\\" : directoryPath;
        }

        public void Add(RatingRecord entity, DifficultyLevels key)
        {
            if (entity == null)
                return;

            string filepath = FormFilepath(key);
            var ratingTableConcreteDifficulty = GetAll(key);
            ratingTableConcreteDifficulty = ratingTableConcreteDifficulty.Append(entity);
            _jsonEnumerableFileHandler.Save(ratingTableConcreteDifficulty, filepath);
        }

        public IEnumerable<RatingRecord> GetAll(DifficultyLevels key)
        {
            string filepath = FormFilepath(key);
            return _jsonEnumerableFileHandler.Load(filepath);
        }

        private string FormFilepath(DifficultyLevels level) => Path.Combine(_directoryPath, level.ToString() + PART_OF_FILEPATH);
    }
}
