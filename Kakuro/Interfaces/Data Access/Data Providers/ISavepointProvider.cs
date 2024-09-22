using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Models;

namespace Kakuro.Interfaces.Data_Access.Data_Providers
{
    // #BAD: the idea for creating interfaces ISavepointProvider and IRatingRecordProvider was not to have same interfaces for
    // Repositories and Data Providers, because they are in different system layouts and because I didn't think that it'd be
    // good for DI. I dunno if it's a good practice. 
    // I should remember about it
    public interface ISavepointProvider : IRepository<Savepoint> { }
}
