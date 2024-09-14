namespace Kakuro.Interfaces.Data_Access
{
    public interface IReadAllRepository<T, F>
    {
        IEnumerable<T> GetAll(F key);
    }
}
