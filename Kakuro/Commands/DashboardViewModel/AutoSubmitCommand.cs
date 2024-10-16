using Kakuro.Base_Classes;
using Kakuro.Enums;
using System.Windows.Input;

namespace Kakuro.Commands.DashboardViewModel
{
    public class AutoSubmitCommand : RelayCommand
    {
        private ViewModels.DashboardViewModel _dashboardViewModel;
        private DashboardItemCollection _dashboard;
        private ICommand _validateSolutionCommand;

        public AutoSubmitCommand(ViewModels.DashboardViewModel dashboardViewModel, ICommand validateSolutionCommand)
        {
            _dashboardViewModel = dashboardViewModel;
            _dashboard = _dashboardViewModel.Dashboard;
            _validateSolutionCommand = validateSolutionCommand;
        }
        public override void Execute(object? parameter)
        {
            if (!_dashboardViewModel.AutoSubmit)
                return;

            var cellType = (CellType)parameter;

            if (cellType != CellType.ValueCell)
                return;

            bool allElementsAreFilled = true;
            int dashboardSize = _dashboard.Count;
            ViewModels.DashboardItemViewModel currentElement;

            for (int i = 1; i < dashboardSize - 1; i++)
            {
                for (int j = 1; j < dashboardSize - 1; j++)
                {
                    currentElement = _dashboard[i][j];

                    if (currentElement.CellType == CellType.ValueCell && currentElement.DisplayValue == "")
                    {
                        allElementsAreFilled = false;
                        break;
                    }
                }
                if (!allElementsAreFilled)
                    break;
            }

            if (allElementsAreFilled)
                _validateSolutionCommand.Execute(parameter);
        }
    }
}
