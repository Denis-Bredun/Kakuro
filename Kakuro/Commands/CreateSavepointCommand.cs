using Kakuro.Base_Classes;
using Kakuro.Models;
using Kakuro.ViewModels;
using Microsoft.VisualBasic;

namespace Kakuro.Commands
{
    public class CreateSavepointCommand : RelayCommand
    {
        private readonly SavepointsViewModel _savepointsViewModel;
        private readonly DashboardViewModel _dashboardViewModel;

        public CreateSavepointCommand(SavepointsViewModel savepointsViewModel, DashboardViewModel dashboardViewModel)
        {
            _savepointsViewModel = savepointsViewModel;
            _dashboardViewModel = dashboardViewModel;
        }

        public override void Execute(object? parameter)
        {
            var dashboard = _dashboardViewModel.Dashboard;

            var newId = _savepointsViewModel.Savepoints.Count + 1;

            string defaultName = $"Savepoint #{newId}";

            string inputName = Interaction.InputBox("Enter a name for the Savepoint:", "Create Savepoint", defaultName);

            if (string.IsNullOrEmpty(inputName))
                inputName = defaultName;

            var newSavepoint = new Savepoint(
                newId,
                inputName,
                dashboard);

            _savepointsViewModel.Savepoints.Add(new SavepointViewModel(newSavepoint));
        }
    }
}
