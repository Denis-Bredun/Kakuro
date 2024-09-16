namespace Kakuro.Interfaces.Data_Access
{
    public interface IJsonEnumerableFileHandler<T> : IFilesHandler<IEnumerable<T>, string>
    {
        new void Save(IEnumerable<T> data, string filepath);
        new IEnumerable<T> Load(string filepath);
    }
}
