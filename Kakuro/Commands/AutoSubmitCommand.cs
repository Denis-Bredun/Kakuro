using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.ViewModels;
using System.Windows.Input;

namespace Kakuro.Commands
{
    public class AutoSubmitCommand : RelayCommand
    {
        private DashboardViewModel _dashboardViewModel;
        private DashboardItemCollection _dashboard;
        private ICommand _validateSolutionCommand;

        public AutoSubmitCommand(DashboardViewModel dashboardViewModel, ICommand validateSolutionCommand)
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
            DashboardItemViewModel currentElement;

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
