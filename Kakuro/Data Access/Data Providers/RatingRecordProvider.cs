using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Models;

namespace Kakuro.Data_Access.Data_Providers
{
    public class RatingRecordProvider : IRatingRecordProvider
    {
        private bool _isCacheSynchronizedWithFiles = true;
        private IReadAllRepository<RatingRecord, DifficultyLevels> _dataService;
        public Dictionary<DifficultyLevels, IEnumerable<RatingRecord>> Cache { private set; get; }
        // #BAD: Only for tests I made cache public for reading. But it shouldn't be public any possible way.
        // It should be available only inside the class.
        // #BAD: it's a temporary solution for the cache mechanism. There's an IMemoryCache and I should use it instead.
        // As an idea to make it create inside of this Data Provider, but to check it with mocks, we can
        // implement "Fabric method" pattern, in theory. Answer was also given here: https://qna.habr.com/q/1371574

        public RatingRecordProvider(IReadAllRepository<RatingRecord, DifficultyLevels> dataService)
        {
            _dataService = dataService;
            Cache = new Dictionary<DifficultyLevels, IEnumerable<RatingRecord>>();
        }

        public void Add(RatingRecord entity, DifficultyLevels key)
        {
            _dataService.Add(entity, key);
            _isCacheSynchronizedWithFiles = false;
        }

        public IEnumerable<RatingRecord> GetAll(DifficultyLevels key)
        {
            IEnumerable<RatingRecord>? ratingRecords;

            if (_isCacheSynchronizedWithFiles)             // We update cache only when needed: when we added new data last time
                                                           // AND when we try to read the data.
            {
                Cache.TryGetValue(key, out ratingRecords);

                ratingRecords ??= _dataService.GetAll(key);
            }
            else
            {
                ratingRecords = _dataService.GetAll(key);

                Cache[key] = ratingRecords;

                _isCacheSynchronizedWithFiles = true;
            }

            return ratingRecords;
        }
    }
}
