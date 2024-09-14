namespace Kakuro.Interfaces.Data_Access
{
    public interface IReadAllRepository<T, F>
    {
        void Add(T entity);
        IEnumerable<T> GetAll(F key);
    }
}
