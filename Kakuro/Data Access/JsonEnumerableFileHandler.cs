using Kakuro.Interfaces.Data_Access;
using System.IO;
using System.Text.Json;

namespace Kakuro.Data_Access
{
    public class JsonEnumerableFileHandler<T> : IJsonEnumerableFileHandler<T>
    {
        public IEnumerable<T> Load(string filepath)
        {
            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
                return new List<T>();

            var jsonData = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
        }

        public void Save(IEnumerable<T> data, string filepath)
        {
            if (data == null || string.IsNullOrEmpty(filepath))
                return;

            var directory = Path.GetDirectoryName(filepath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var jsonData = JsonSerializer.Serialize(data);
            File.WriteAllText(filepath, jsonData);
        }
    }
}
