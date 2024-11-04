using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Models;
using System.Collections.ObjectModel;

namespace Kakuro.Data_Access.Data_Providers
{
    public class SavepointProvider : ISavepointProvider
    {
        private IRepository<Savepoint> _dataService;
        private const int MAX_CACHE_COUNT = 3;
        public ObservableCollection<Savepoint> Cache { get; private set; }

        // #BAD: Same as in class RatingRecordProvider, Cache is made public for reading, so as for testing and checking.

        public SavepointProvider(IRepository<Savepoint> dataService)
        {
            _dataService = dataService;
            Cache = new ObservableCollection<Savepoint>();
        }

        public bool Add(Savepoint entity)
        {
            var isSaved = _dataService.Add(entity);

            if (isSaved == false)
                return isSaved;

            AddSavepointToCache(entity);

            return isSaved;
        }

        public Savepoint Delete(int id)
        {
            var deletedEntity = _dataService.Delete(id);

            var cachedEntity = Cache.FirstOrDefault(el => el.Id == id);
            if (cachedEntity != null)
            {
                Cache.Remove(cachedEntity);
            }

            return deletedEntity;
        }

        public Savepoint? GetById(int id)
        {
            var foundSavepoint = Cache.FirstOrDefault(IsIdEqual(id));

            if (foundSavepoint == null)
            {
                foundSavepoint = _dataService.GetById(id);
                AddSavepointToCache(foundSavepoint);
            }

            return foundSavepoint;
        }

        public void Update(Savepoint entity)
        {
            _dataService.Update(entity);

            var cachedSavepoint = Cache.FirstOrDefault(IsIdEqual(entity.Id));

            if (cachedSavepoint != null)
            {
                var index = Cache.IndexOf(cachedSavepoint);
                Cache[index] = entity;
            }
            else
            {
                var updatedSavepoint = _dataService.GetById(entity.Id);
                AddSavepointToCache(updatedSavepoint);
            }
        }

        private void AddSavepointToCache(Savepoint entity)
        {
            Cache.Add(entity);

            if (Cache.Count > MAX_CACHE_COUNT)
                Cache.RemoveAt(0);
        }

        private Func<Savepoint, bool> IsIdEqual(int id) => el => el.Id == id; // "el" stands for "element"

        public void CleanData()
        {
            _dataService.CleanData();
        }
    }
}
