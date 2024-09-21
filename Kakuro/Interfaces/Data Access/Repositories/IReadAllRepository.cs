namespace Kakuro.Interfaces.Data_Access.Repositories
{
    // #BAD: we have two 2 interfaces for, yep, 2 repositories with not very different roles.. maybe...
    // I dunno. It causes some worries. I should remember about this place.
    public interface IReadAllRepository<T, F>
    {
        void Add(T entity, F key);
        IEnumerable<T> GetAll(F key);
    }
}
