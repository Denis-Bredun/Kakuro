using Kakuro.Base_Classes;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Models;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace Kakuro.Commands.SavepointsViewModel
{
    public class CreateSavepointCommand : RelayCommand, IDisposable
    {
        private readonly ViewModels.SavepointsViewModel _savepointsViewModel;
        private readonly ViewModels.DashboardViewModel _dashboardViewModel;
        private readonly ISavepointProvider _savepointProvider;
        private bool _disposed = false;

        public CreateSavepointCommand(
            ViewModels.SavepointsViewModel savepointsViewModel,
            ViewModels.DashboardViewModel dashboardViewModel,
            ISavepointProvider savepointProvider)
        {
            _savepointsViewModel = savepointsViewModel;
            _dashboardViewModel = dashboardViewModel;
            _savepointProvider = savepointProvider;

            _savepointsViewModel.PropertyChanged += OnSavepointsCountPropertyChanged;
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

            var newSavepoint = new Savepoint(newId, dashboard);

            _savepointProvider.Add(newSavepoint);

            _savepointsViewModel.Savepoints.Add(new ViewModels.SavepointViewModel(newId, inputName));

            _savepointsViewModel.IsCreatingAllowed = _savepointsViewModel.Savepoints.Count < 10;
        }

        public override bool CanExecute(object? parameter)
        {
            return NotMaxSavepointsCreated() && !AreCorrectAnswersShown() && base.CanExecute(parameter);
        }

        private bool NotMaxSavepointsCreated() => _savepointsViewModel.Savepoints.Count < 10;

        private bool AreCorrectAnswersShown() => _savepointsViewModel.CorrectAnswersAreShown;

        private void OnSavepointsCountPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_savepointsViewModel.Savepoints.Count) ||
                e.PropertyName == nameof(_savepointsViewModel.CorrectAnswersAreShown))
                OnCanExecutedChanged();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _savepointsViewModel.PropertyChanged -= OnSavepointsCountPropertyChanged;
                }
                _disposed = true;
            }
        }

        ~CreateSavepointCommand()
        {
            Dispose(false);
        }
    }
}
