namespace Kakuro.Interfaces.Data_Access
{
    public interface IFilesHandler<T, F>
    {
        void Save(T data, string filepath);
        T Load(string filepath);
    }
}
