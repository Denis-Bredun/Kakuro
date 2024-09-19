using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access;
using Kakuro.Models;

namespace Kakuro.Data_Access
{
    public class RatingRecordProvider : IRatingRecordProvider
    {
        private IReadAllRepository<RatingRecord, DifficultyLevels> _dataService;

        public RatingRecordProvider(IReadAllRepository<RatingRecord, DifficultyLevels> dataService)
        {
            _dataService = dataService;
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
