using Kakuro.Interfaces.Data_Access;
using Kakuro.Models;

namespace Kakuro.Data_Access
{
    public class SavepointRepository : IRepository<Savepoint>
    {
        private IJsonEnumerableFileHandler<Savepoint> _jsonEnumerableFileHandler;
        private const string FILEPATH = "Savepoints.json";

        public SavepointRepository(IJsonEnumerableFileHandler<Savepoint> jsonEnumerableFileHandler)
        {
            _jsonEnumerableFileHandler = jsonEnumerableFileHandler;
        }

        public void Add(Savepoint entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Savepoint GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Savepoint entity)
        {
            throw new NotImplementedException();
        }
    }
}
