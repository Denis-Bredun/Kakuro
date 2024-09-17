using Kakuro.Interfaces.Data_Access;
using Kakuro.Models;
using System.IO;

namespace Kakuro.Data_Access
{
    public class SavepointRepository : IRepository<Savepoint>
    {
        private IJsonEnumerableFileHandler<Savepoint> _jsonEnumerableFileHandler;
        private readonly string _directoryPath;
        private readonly string _filename = "Savepoints.json";
        private readonly string _filepath;

        public SavepointRepository(IJsonEnumerableFileHandler<Savepoint> jsonEnumerableFileHandler, string directoryPath = "")
        {
            _jsonEnumerableFileHandler = jsonEnumerableFileHandler;
            _directoryPath = directoryPath;
            _filepath = Path.Combine(_directoryPath, _filename);
        }

        public void Add(Savepoint entity)
        {
            if (entity == null)
                return;

            var savepoints = _jsonEnumerableFileHandler.Load(_filepath);
            savepoints = savepoints.Append(entity);
            _jsonEnumerableFileHandler.Save(savepoints, _filepath);
        }

        public void Delete(int id)
        {
            var savepoints = _jsonEnumerableFileHandler.Load(_filepath);

            if (id > savepoints.Count() - 1 || id < 0)    // id starts from 0
                return;

            savepoints = savepoints.Where(el => el.Id != id); // "el" stands for "element"

            _jsonEnumerableFileHandler.Save(savepoints, _filepath);
        }

        public Savepoint? GetById(int id)
        {
            var savepoints = _jsonEnumerableFileHandler.Load(_filepath);

            if (id > savepoints.Count() - 1 || id < 0)
                return null;

            return savepoints.ElementAt(id);
        }

        public void Update(Savepoint entity)
        {
            var savepoints = _jsonEnumerableFileHandler.Load(_filepath);

            if (entity == null || savepoints.Count() == 0)
                return;

            savepoints = savepoints.Select((el, id) => entity.Id == id ? entity : el);

            _jsonEnumerableFileHandler.Save(savepoints, _filepath);
        }
    }
}
