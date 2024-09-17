using Kakuro.Data_Access;
using Kakuro.Models;

namespace Kakuro.Tests.Integration_Tests
{
    public class SavepointRepositoryTests
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Integration Tests\\Files\\SavepointRepositoryTests\\";
        private JsonEnumerableFileHandler<Savepoint> _jsonEnumerableFileHandler;
        private SavepointRepository _savepointRepository;

        public SavepointRepositoryTests()
        {
            _jsonEnumerableFileHandler = new JsonEnumerableFileHandler<Savepoint>();
            _savepointRepository = new SavepointRepository(_jsonEnumerableFileHandler);
        }


    }
}
