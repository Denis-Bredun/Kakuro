using Kakuro.Interfaces.Data_Access;

namespace Kakuro.Data_Access
{
    public class JsonEnumerableFileHandler<T> : IJsonEnumerableFileHandler<T>
    {
        public IEnumerable<T> Load(string filepath)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<T> data, string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
