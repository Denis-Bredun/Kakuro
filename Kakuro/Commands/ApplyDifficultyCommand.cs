using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;

using DashboardItemCollection = System.Collections.ObjectModel.ObservableCollection<System.Collections.ObjectModel.ObservableCollection<Kakuro.ViewModels.DashboardItemViewModel>>;

namespace Kakuro.Commands
{
    public class ApplyDifficultyCommand : RelayCommand
    {
        private IDashboardGeneratorProvider _dashboardGeneratorProvider;
        private DashboardItemCollection _dashboard;

        public ApplyDifficultyCommand(IDashboardGeneratorProvider dashboardGeneratorProvider, DashboardItemCollection dashboard)
        {
            _dashboardGeneratorProvider ??= dashboardGeneratorProvider;
            _dashboard ??= dashboard;
        }

        public override void Execute(object? parameter)
        {
            var difficultyLevel = (DifficultyLevels)parameter;

            _dashboard.Clear();

            _dashboardGeneratorProvider.FillDashboardWithValuesAndSums(difficultyLevel);
        }
    }
}
