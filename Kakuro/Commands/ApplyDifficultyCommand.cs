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
            var difficultyLevel = (DifficultyLevels)parameter;

            _dashboardProvider.GenerateDashboard(difficultyLevel);
        }
    }
}
