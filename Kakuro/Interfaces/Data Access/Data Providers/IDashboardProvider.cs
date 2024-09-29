using Kakuro.Enums;

namespace Kakuro.Interfaces.Data_Access.Data_Providers
{
    public interface IDashboardProvider
    {
        void GenerateDashboard(DifficultyLevels difficultyLevel);
    }
}
