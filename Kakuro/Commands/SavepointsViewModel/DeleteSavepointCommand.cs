using Kakuro.Base_Classes;
using System.ComponentModel;

namespace Kakuro.Commands.SavepointsViewModel
{
    public class DeleteSavepointCommand : RelayCommand, IDisposable
    {
        private ViewModels.SavepointsViewModel _savepointsViewModel;
        private bool _disposed = false;

        public DeleteSavepointCommand(ViewModels.SavepointsViewModel savepointsViewModel)
        {
            _savepointsViewModel = savepointsViewModel;

            _savepointsViewModel.PropertyChanged += OnSelectedSavepointPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _savepointsViewModel.Savepoints.Remove(_savepointsViewModel.SelectedSavepoint);
            _savepointsViewModel.SelectedSavepoint = null;
        }

        public override bool CanExecute(object? parameter)
        {
            return _savepointsViewModel.SelectedSavepoint != null && base.CanExecute(parameter);
        }

        private void OnSelectedSavepointPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_savepointsViewModel.SelectedSavepoint))
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
