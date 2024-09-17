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
            if (entity == null)
                return;

            var savepoints = _jsonEnumerableFileHandler.Load(FILEPATH);
            savepoints.Append(entity);
            _jsonEnumerableFileHandler.Save(savepoints, FILEPATH);
        }

        public void Delete(int id)
        {
            var savepoints = _jsonEnumerableFileHandler.Load(FILEPATH);

            if (id > savepoints.Count() - 1 || id < 0)    // id starts from 0
                return;

            savepoints = savepoints.Where(el => el.Id != id); // el stands for "element"

            _jsonEnumerableFileHandler.Save(savepoints, FILEPATH);
        }

        public Savepoint? GetById(int id)
        {
            var savepoints = _jsonEnumerableFileHandler.Load(FILEPATH);

            if (id > savepoints.Count() - 1 || id < 0)
                return null;

            return savepoints.ElementAt(id);
        }

        public void Update(Savepoint entity)
        {
            var savepoints = _jsonEnumerableFileHandler.Load(FILEPATH);

            if (entity == null || savepoints.Count() == 0)
                return;

            savepoints = savepoints.Select((el, id) => entity.Id == id ? entity : el);

            _jsonEnumerableFileHandler.Save(savepoints, FILEPATH);
        }
    }
}
