using Kakuro.Data_Access;
using Kakuro.Models;

namespace Kakuro.Tests.Integration_Tests
{
    public class SavepointRepositoryTests : IDisposable
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Integration Tests\\Files\\SavepointRepositoryTests\\";
        private JsonEnumerableFileHandler<Savepoint> _jsonEnumerableFileHandler;
        private SavepointRepository _savepointRepository;

        public SavepointRepositoryTests()
        {
            _jsonEnumerableFileHandler = new JsonEnumerableFileHandler<Savepoint>();
            _savepointRepository = new SavepointRepository(_jsonEnumerableFileHandler);
        }

        public void Dispose()
        {
            if (Directory.Exists(DIRECTORY_PATH))
                Directory.Delete(DIRECTORY_PATH, true);
        }
    }
}
