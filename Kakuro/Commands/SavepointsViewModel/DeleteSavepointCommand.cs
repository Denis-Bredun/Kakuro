using Kakuro.Base_Classes;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using System.ComponentModel;

namespace Kakuro.Commands.SavepointsViewModel
{
    public class DeleteSavepointCommand : RelayCommand, IDisposable
    {
        private ViewModels.SavepointsViewModel _savepointsViewModel;
        private ISavepointProvider _savepointProvider;
        private bool _disposed = false;

        public DeleteSavepointCommand(ViewModels.SavepointsViewModel savepointsViewModel, ISavepointProvider savepointProvider)
        {
            _savepointsViewModel = savepointsViewModel;
            _savepointProvider = savepointProvider;

            _savepointsViewModel.PropertyChanged += OnSelectedSavepointPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _savepointProvider.Delete(_savepointsViewModel.SelectedSavepoint.Id);

            _savepointsViewModel.Savepoints.Remove(_savepointsViewModel.SelectedSavepoint);
            _savepointsViewModel.SelectedSavepoint = null;
        }

        public override bool CanExecute(object? parameter)
        {
            return AnySavepointSelected() && !AreCorrectAnswersShown() && base.CanExecute(parameter);
        }

        private bool AnySavepointSelected() => _savepointsViewModel.SelectedSavepoint != null;

        private bool AreCorrectAnswersShown() => _savepointsViewModel.CorrectAnswersAreShown;

        private void OnSelectedSavepointPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_savepointsViewModel.SelectedSavepoint) ||
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
                    _savepointsViewModel.PropertyChanged -= OnSelectedSavepointPropertyChanged;
                }
                _disposed = true;
            }
        }

        ~DeleteSavepointCommand()
        {
            Dispose(false);
        }
    }
}
