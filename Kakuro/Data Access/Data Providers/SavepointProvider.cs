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

            Cache.Add(entity);

            if (Cache.Count > MAX_CACHE_COUNT)
                Cache.RemoveAt(0);

            return isSaved;
        }

        public void Delete(int id)
        {
            _dataService.Delete(id);
            Cache.Dequeue(id);
        }

        public Savepoint? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Savepoint entity)
        {
            throw new NotImplementedException();
        }
    }
}
