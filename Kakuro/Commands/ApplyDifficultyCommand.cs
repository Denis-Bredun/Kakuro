using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;

namespace Kakuro.Commands
{
    public class ApplyDifficultyCommand : RelayCommand
    {
        private IDashboardProvider _dashboardProvider;

        public ApplyDifficultyCommand(IDashboardProvider dashboardProvider)
        {
            _dashboardProvider ??= dashboardProvider;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                throw new NullReferenceException("Parameter for ApplyDifficultyCommand is null!");

            if (!Enum.TryParse<DifficultyLevels>(parameter.ToString(), out var difficultyLevel))
                throw new ArgumentException("Parameter for ApplyDifficultyCommand is of incorrect type!");

            if (DoesNotDashboardExistForDifficulty(difficultyLevel))
                _dashboardProvider.GenerateDashboard(difficultyLevel);
        }

        private bool DoesNotDashboardExistForDifficulty(DifficultyLevels difficultyLevel)
        {
            const int TOTAL_BORDER_SIZE_FROM_TWO_SIDES = 2;
            int onlyDashboardSize = (int)difficultyLevel;
            int fullDashboardSize = onlyDashboardSize + TOTAL_BORDER_SIZE_FROM_TWO_SIDES;

            int currentDashboardSize = _dashboardProvider.GetDashboardCount();

            return fullDashboardSize != currentDashboardSize;
        }
    }
}
