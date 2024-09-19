using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Models;

namespace Kakuro.Data_Access.Data_Providers
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
