using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Interfaces.Data_Access.Tools;
using Kakuro.Models;
using System.IO;

namespace Kakuro.Data_Access.Repositories
{
    public class SavepointRepository : IRepository<Savepoint>
    {
        private const int MAX_COUNT = 10;
        private const string FILENAME = "Savepoints.json";
        private readonly string _directoryPath;         // #BAD: we shall not store data about filepathes, directories and
                                                        // count of max files in repositories. Some sort of File Handler is
                                                        // responsible for that
        private readonly string _filepath;

        private IJsonFileHandler<Savepoint> _jsonFileHandler;

        public int Count { private set; get; } = 0;

        public SavepointRepository(IJsonFileHandler<Savepoint> jsonEnumerableFileHandler, string directoryPath = "")
        {
            _jsonFileHandler = jsonEnumerableFileHandler;
            _directoryPath = directoryPath;
            _filepath = Path.Combine(_directoryPath, FILENAME);
        }

        public bool Add(Savepoint entity)
        {
            if (entity == null)
                throw new NullReferenceException("Entity equals null.");

            var savepoints = _jsonFileHandler.Load(_filepath);

            if (IsInvalidState(savepoints, entity.Id))
                return false;

            savepoints = savepoints.Append(entity);
            Count++;

            _jsonFileHandler.Save(savepoints, _filepath);
            return true;
        }

        // "el" stands for "element"
        public Savepoint Delete(int id)
        {
            var savepoints = _jsonFileHandler.Load(_filepath);

            if (!IsDuplicateId(savepoints, id))
                throw new IndexOutOfRangeException("Entity with such ID wasn't found.");

            var deletedSavepoint = savepoints.FirstOrDefault(IsIdEqual(id));

            savepoints = savepoints.Where(GetRemoveByIdSelector(id));
            Count--;

            _jsonFileHandler.Save(savepoints, _filepath);

            return deletedSavepoint;
        }

        public Savepoint? GetById(int id)
        {
            var savepoints = _jsonFileHandler.Load(_filepath);

            var foundSavepoint = savepoints.FirstOrDefault(IsIdEqual(id));

            if (foundSavepoint == null)
                throw new IndexOutOfRangeException("Entity with such ID doesn't exist.");
            else
                return foundSavepoint;
        }

        public void Update(Savepoint entity)
        {
            var savepoints = _jsonFileHandler.Load(_filepath);

            CheckIfUnableToUpdate(savepoints, entity);

            var existingSavepoint = savepoints.FirstOrDefault(IsIdEqual(entity.Id));
            if (existingSavepoint.Equals(entity))
                return;         // i don't throw exception, because in user's opinion everything is okay and an update is done. Business logic, lmao

            savepoints = savepoints.Select(GetUpdatedSavepointSelector(entity.Id, entity));

            _jsonFileHandler.Save(savepoints, _filepath);
        }

        private void CheckIfUnableToUpdate(IEnumerable<Savepoint> savepoints, Savepoint entity)
        {
            if (entity == null)
                throw new NullReferenceException("Entity equals null.");
            if (!IsDuplicateId(savepoints, entity.Id))
                throw new IndexOutOfRangeException("Entity with such ID wasn't found.");
        }

        private bool IsInvalidState(IEnumerable<Savepoint> savepoints, int id)
        {
            if (IsMaxCountReached(savepoints))
                return true;
            if (IsDuplicateId(savepoints, id))
                throw new ArgumentException("Entity with such ID already exists!");
            return false;
        }

        private bool IsMaxCountReached(IEnumerable<Savepoint> savepoints) => savepoints.Count() == MAX_COUNT;

        private bool IsDuplicateId(IEnumerable<Savepoint> savepoints, int id) => savepoints.Any(IsIdEqual(id));

        private Func<Savepoint, bool> IsIdEqual(int id) => el => el.Id == id; // "el" stands for "element"

        private Func<Savepoint, bool> GetRemoveByIdSelector(int id) => el => !IsIdEqual(id)(el);

        private Func<Savepoint, Savepoint> GetUpdatedSavepointSelector(int id, Savepoint newEntity) => el => IsIdEqual(id)(el) ? newEntity : el;

        public void CleanData()
        {
            _jsonFileHandler.DeleteFile(_filepath);
        }
    }
}
