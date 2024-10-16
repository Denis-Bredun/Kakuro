using Kakuro.Base_Classes;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using System.ComponentModel;

namespace Kakuro.Commands.SavepointsViewModel
{
    public class RewriteSavepointCommand : RelayCommand, IDisposable
    {
        private ViewModels.SavepointsViewModel _savepointsViewModel;
        private readonly ViewModels.DashboardViewModel _dashboardViewModel;
        private readonly ISavepointProvider _savepointProvider;
        private bool _disposed = false;

        public RewriteSavepointCommand(
            ViewModels.SavepointsViewModel savepointsViewModel,
            ViewModels.DashboardViewModel dashboardViewModel,
            ISavepointProvider savepointProvider)
        {
            _savepointsViewModel = savepointsViewModel;
            _dashboardViewModel = dashboardViewModel;
            _savepointProvider = savepointProvider;

            _savepointsViewModel.PropertyChanged += OnSelectedSavepointPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            var savepointToRewrite = _savepointProvider.GetById(_savepointsViewModel.SelectedSavepoint.Id);
            savepointToRewrite.Dashboard = _dashboardViewModel.CreateDashboardCopy();
            _savepointProvider.Update(savepointToRewrite);
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

        ~RewriteSavepointCommand()
        {
            Dispose(false);
        }
    }
}
