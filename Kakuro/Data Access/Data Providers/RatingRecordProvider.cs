using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Models;

namespace Kakuro.Data_Access.Data_Providers
{
    public class RatingRecordProvider : IRatingRecordProvider
    {
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

        // #BAD: too inefficient algorithm. We add record straightfully to files just so entities with properties of game 
        // completion time could sort, then we read ALL the data and then we save it to the cache.
        public void Add(RatingRecord entity, DifficultyLevels key)
        {
            _dataService.Add(entity, key);
            var newRatingRecords = _dataService.GetAll(key);

            if (IsKeyInCache(key))
                Cache.Remove(key);

            Cache.Add(key, newRatingRecords);
        }

        public IEnumerable<RatingRecord> GetAll(DifficultyLevels key)
        {
            var ratingRecords = Cache[key];
            if (ratingRecords == null)
            {
                ratingRecords = _dataService.GetAll(key);
                Cache.Add(key, ratingRecords);
            }
            return ratingRecords;
        }

        private bool IsKeyInCache(DifficultyLevels key) => Cache.Count(pair => pair.Key == key) != 0;
    }
}
