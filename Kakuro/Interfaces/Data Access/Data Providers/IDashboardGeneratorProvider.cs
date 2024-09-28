using Kakuro.Enums;

namespace Kakuro.Interfaces.Data_Access.Data_Providers
{
    public interface IDashboardGeneratorProvider
    {
        void FillDashboardWithValuesAndSums(DifficultyLevels difficultyLevel);
    }
}
