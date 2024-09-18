namespace Kakuro.Interfaces.Data_Access
{
    public interface IRepository<T>
    {
        T? GetById(int id);
        bool Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
