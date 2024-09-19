namespace Kakuro.Interfaces.Data_Access.Tools
{
    public interface IJsonFileHandler<T> : IFilesHandler<IEnumerable<T>, string>
    {
        new void Save(IEnumerable<T> data, string filepath);
        new IEnumerable<T> Load(string filepath);
    }
}
