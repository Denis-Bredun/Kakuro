using Kakuro.Base_Classes;
using Kakuro.Models;
using Kakuro.ViewModels;
using Microsoft.VisualBasic;
using System.ComponentModel;

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

            _savepointsViewModel.PropertyChanged += OnSavepointsCollectionPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            var dashboard = _dashboardViewModel.CreateDashboardCopy();

            var newId = _savepointsViewModel.Savepoints.Count + 1;

            while (_savepointsViewModel.Savepoints.Any(sp => sp.Id == newId))
            {
                newId++;
            }

            string defaultName = $"Savepoint #{newId}";
            string inputName = Interaction.InputBox("Enter a name for the Savepoint:", "Create Savepoint", defaultName);

            if (string.IsNullOrEmpty(inputName))
            {
                inputName = defaultName;
            }

            var newSavepoint = new Savepoint(
                newId,
                inputName,
                dashboard);

            _savepointsViewModel.Savepoints.Add(new SavepointViewModel(newSavepoint));
        }


        public override bool CanExecute(object? parameter)
        {
            return _savepointsViewModel.Savepoints.Count < 10 && base.CanExecute(parameter);
        }

        private void OnSavepointsCollectionPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_savepointsViewModel.Savepoints.Count))
                OnCanExecutedChanged();
        }
    }
}
