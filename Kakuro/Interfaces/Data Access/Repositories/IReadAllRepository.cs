namespace Kakuro.Interfaces.Data_Access.Repositories
{
    public interface IReadAllRepository<T, F>
    {
        void Add(T entity, F key);
        IEnumerable<T> GetAll(F key);
    }
}
