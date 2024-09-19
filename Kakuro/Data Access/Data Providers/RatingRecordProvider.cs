using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Models;

namespace Kakuro.Data_Access.Data_Providers
{
    public class RatingRecordProvider : IRatingRecordProvider, IDisposable
    {
        private IReadAllRepository<RatingRecord, DifficultyLevels> _dataService;
        private Dictionary<DifficultyLevels, IEnumerable<RatingRecord>> _cache; // temporary improvisation of cache, lmao
                                                                                // in the future, it's planned that cache here is cleaned every 5 min.

        public RatingRecordProvider(IReadAllRepository<RatingRecord, DifficultyLevels> dataService)
        {
            _dataService = dataService;
            _cache = new Dictionary<DifficultyLevels, IEnumerable<RatingRecord>>();
        }

        // Too inefficient an operation. This is a temporary solution until a proper cache service is implemented.
        // MVP, anyway.
        public void Add(RatingRecord entity, DifficultyLevels key)
        {
            _dataService.Add(entity, key);
            var newRatingRecords = _dataService.GetAll(key);

            if (IsKeyInCache(key))
                _cache.Remove(key);

            _cache.Add(key, newRatingRecords);
        }

        public IEnumerable<RatingRecord> GetAll(DifficultyLevels key)
        {
            var ratingRecords = _cache[key];
            if (ratingRecords == null)
            {
                ratingRecords = _dataService.GetAll(key);
                _cache.Add(key, ratingRecords);
            }
            return ratingRecords;
        }

        public void Dispose()
        {
            _cache.Clear(); // Honestly, I'm not sure if it's needed or not. Let it be here for some time.
        }

        private bool IsKeyInCache(DifficultyLevels key) => _cache.Count(pair => pair.Key == key) != 0;
    }
}
