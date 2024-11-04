namespace Kakuro.Interfaces.Data_Access.Tools
{

    // #BAD: unnecessary interface. As for "Fabric method" pattern we could implement IDataHandler interface straightfully.
    // This note is connected with the note in IFilesHandler.cs.

    public interface IJsonFileHandler<T> : IFilesHandler<IEnumerable<T>, string>
    {
        new void Save(IEnumerable<T> data, string filepath);
        new IEnumerable<T> Load(string filepath);
        void DeleteFile(string filepath);
    }
}
