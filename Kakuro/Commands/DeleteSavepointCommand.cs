using Kakuro.Base_Classes;
using Kakuro.ViewModels;
using System.ComponentModel;

namespace Kakuro.Commands
{
    public class DeleteSavepointCommand : RelayCommand
    {
        private SavepointsViewModel _savepointsViewModel;

        public DeleteSavepointCommand(SavepointsViewModel savepointsViewModel)
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
    }
}
