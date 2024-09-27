using Kakuro.Enums;

namespace Kakuro.Interfaces.Data_Access.Data_Providers
{
    public interface IDashboardTemplateProvider
    {
        string[,] GenerateTemplate(DifficultyLevels difficultyLevel);
    }
}
