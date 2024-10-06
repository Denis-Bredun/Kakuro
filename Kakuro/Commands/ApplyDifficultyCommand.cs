using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.ViewModels;

namespace Kakuro.Commands
{
    public class ApplyDifficultyCommand : RelayCommand
    {
        private IDashboardProvider _dashboardProvider;
        public IDashboardViewModel DashboardViewModel { get; set; } // #BAD: shall we pass it as parameter in constructor?

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

            if (DashboardViewModel == null)
                throw new NullReferenceException("DashboardViewModel in ApplyDifficultyCommand is null!");

            DashboardViewModel.ChoosenDifficulty = difficultyLevel;

            _dashboardProvider.GenerateDashboard(difficultyLevel);
        }
    }
}
