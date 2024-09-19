using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Models;

namespace Kakuro.Data_Access.Data_Providers
{
    public class SavepointProvider : ISavepointProvider
    {
        private IRepository<Savepoint> _dataService;

        public SavepointProvider(IRepository<Savepoint> dataService)
        {
            _dataService = dataService;
        }

        public bool Add(Savepoint entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
