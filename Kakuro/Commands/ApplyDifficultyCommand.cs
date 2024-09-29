using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using DashboardItemCollection = System.Collections.ObjectModel.ObservableCollection<System.Collections.ObjectModel.ObservableCollection<Kakuro.ViewModels.DashboardItemViewModel>>;

namespace Kakuro.Commands
{
    public class ApplyDifficultyCommand : RelayCommand
    {
        private IDashboardProvider _dashboardProvider;
        private DashboardItemCollection _dashboard;

        public ApplyDifficultyCommand(IDashboardProvider dashboardProvider, DashboardItemCollection dashboard)
        {
            _dashboardProvider ??= dashboardProvider;
            _dashboard ??= dashboard;
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

            int dashboardSizeOnDifficulty = (int)difficultyLevel;
            int fullDashboardSize = dashboardSizeOnDifficulty + TOTAL_BORDER_SIZE_FROM_TWO_SIDES;

            int currentDashboardSize = _dashboard.Count;

            return fullDashboardSize != currentDashboardSize;
        }
    }
}
