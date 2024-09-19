using Kakuro.Enums;
using Kakuro.Models;

namespace Kakuro.Interfaces.Data_Access
{
    public interface IRatingRecordProvider : IReadAllRepository<RatingRecord, DifficultyLevels> { }
}
