namespace Kakuro.Interfaces.Data_Access.Tools
{
    public interface IFilesHandler<T, F> // #BAD: we should have the general interface for all types of communication
                                         // with data: though JSON, through CSV, through DB, e.t.c. Now it's only for files
                                         // So we shouldn't put filepath straightfully in parameters.
    {
        void Save(T data, string filepath);
        T Load(string filepath);
    }
}
