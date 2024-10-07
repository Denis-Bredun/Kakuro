using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.ViewModels;

namespace Kakuro.Commands
{
    // #BAD: tests shall be REwritten
    public class ApplyDifficultyCommand : RelayCommand
    {
        private readonly IDashboardProvider _dashboardProvider;
        private readonly DashboardViewModel _dashboardViewModel;

        public ApplyDifficultyCommand(IDashboardProvider dashboardProvider, DashboardViewModel dashboardViewModel)
        {
            _dashboardProvider ??= dashboardProvider;
            _dashboardViewModel ??= dashboardViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                throw new NullReferenceException("Parameter for ApplyDifficultyCommand is null!");

            if (!Enum.TryParse<DifficultyLevels>(parameter.ToString(), out var difficultyLevel))
                throw new ArgumentException("Parameter for ApplyDifficultyCommand is of incorrect type!");

            _dashboardViewModel.ChoosenDifficulty = difficultyLevel;

            _dashboardProvider.GenerateDashboard(difficultyLevel);
        }
    }
}
