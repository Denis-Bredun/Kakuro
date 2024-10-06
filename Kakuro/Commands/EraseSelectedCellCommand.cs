using Kakuro.Base_Classes;
using Kakuro.ViewModels;

namespace Kakuro.Commands
{
    public class EraseSelectedCellCommand : RelayCommand
    {
        private readonly DashboardViewModel _dashboardViewModel;

        public EraseSelectedCellCommand(DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_dashboardViewModel.SelectedCell != null)
                _dashboardViewModel.SelectedCell.HiddenValue = "";
        }
    }
}
