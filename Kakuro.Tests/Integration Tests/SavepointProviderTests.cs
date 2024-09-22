using Kakuro.Data_Access.Data_Providers;
using Kakuro.Data_Access.Repositories;
using Kakuro.Data_Access.Tools;
using Kakuro.Models;

namespace Kakuro.Tests.Integration_Tests
{
    public class SavepointProviderTests : IDisposable
    {
        private const string DIRECTORY_PATH = "..\\..\\..\\Integration Tests\\Files\\SavepointProviderTests\\";
        private JsonFileHandler<Savepoint> _jsonFileHandler;
        private SavepointRepository _savepointRepository;
        private SavepointProvider _savepointProvider;

        public SavepointProviderTests()
        {
            _jsonFileHandler = new JsonFileHandler<Savepoint>();
            _savepointRepository = new SavepointRepository(_jsonFileHandler, DIRECTORY_PATH);
            _savepointProvider = new SavepointProvider(_savepointRepository);
        }

        public void Dispose()
        {
            if (Directory.Exists(DIRECTORY_PATH))
                Directory.Delete(DIRECTORY_PATH, true);
        }

        // Tests...
    }
}
