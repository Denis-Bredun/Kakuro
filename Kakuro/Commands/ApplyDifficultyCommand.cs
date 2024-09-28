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
            var difficultyLevel = (DifficultyLevels)parameter;

            _dashboardProvider.GenerateDashboard(difficultyLevel);
        }
    }
}
