using Kakuro.Interfaces.Data_Access;
using Kakuro.Models;
using System.IO;

namespace Kakuro.Data_Access
{
    public class SavepointRepository : IRepository<Savepoint>
    {
        private const int MAX_COUNT = 10;
        private const string FILENAME = "Savepoints.json";
        private readonly string _directoryPath;
        private readonly string _filepath;

        private IJsonFileHandler<Savepoint> _jsonEnumerableFileHandler;

        public int Count { private set; get; } = 0;

        public SavepointRepository(IJsonFileHandler<Savepoint> jsonEnumerableFileHandler, string directoryPath = "")
        {
            _jsonEnumerableFileHandler = jsonEnumerableFileHandler;
            _directoryPath = directoryPath;
            _filepath = Path.Combine(_directoryPath, FILENAME);
        }

        public bool Add(Savepoint entity)
        {
            if (entity == null)
                return false;

            var savepoints = _jsonEnumerableFileHandler.Load(_filepath);

            if (IsInvalidState(savepoints, entity.Id))
                return false;

            savepoints = savepoints.Append(entity);
            Count++;

            _jsonEnumerableFileHandler.Save(savepoints, _filepath);
            return true;
        }

        // "el" stands for "element"
        public void Delete(int id)
        {
            var savepoints = _jsonEnumerableFileHandler.Load(_filepath);

            if (!IsDuplicateId(savepoints, id))
                return;

            savepoints = savepoints.Where(GetRemoveByIdSelector(id));
            Count--;

            _jsonEnumerableFileHandler.Save(savepoints, _filepath);
        }

        public Savepoint? GetById(int id)
        {
            var savepoints = _jsonEnumerableFileHandler.Load(_filepath);

            return savepoints.FirstOrDefault(IsIdEqual(id));
        }

        public void Update(Savepoint entity)
        {
            var savepoints = _jsonEnumerableFileHandler.Load(_filepath);

            if (IsUnableToUpdate(savepoints, entity))
                return;

            var existingSavepoint = savepoints.FirstOrDefault(IsIdEqual(entity.Id));
            if (existingSavepoint.Equals(entity))
                return;

            savepoints = savepoints.Select(GetUpdatedSavepointSelector(entity.Id, entity));

            _jsonEnumerableFileHandler.Save(savepoints, _filepath);
        }

        private bool IsUnableToUpdate(IEnumerable<Savepoint> savepoints, Savepoint entity) => entity == null || !IsDuplicateId(savepoints, entity.Id);

        private bool IsInvalidState(IEnumerable<Savepoint> savepoints, int id) => IsMaxCountReached(savepoints) || IsDuplicateId(savepoints, id);

        private bool IsMaxCountReached(IEnumerable<Savepoint> savepoints) => savepoints.Count() == MAX_COUNT;

        private bool IsDuplicateId(IEnumerable<Savepoint> savepoints, int id) => savepoints.Any(IsIdEqual(id));

        private Func<Savepoint, bool> IsIdEqual(int id)
        {
            return el => el.Id == id; // "el" stands for "element"
        }

        private Func<Savepoint, bool> GetRemoveByIdSelector(int id)
        {
            return el => !IsIdEqual(id)(el);
        }

        private Func<Savepoint, Savepoint> GetUpdatedSavepointSelector(int id, Savepoint newEntity)
        {
            return el => IsIdEqual(id)(el) ? newEntity : el;
        }

    }
}
