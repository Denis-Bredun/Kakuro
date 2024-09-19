namespace Kakuro.Interfaces.Data_Access
{
    public interface IFilesHandler<T, F> : IDisposable
    {
        void Save(T data, string filepath);
        T Load(string filepath);
    }
}
