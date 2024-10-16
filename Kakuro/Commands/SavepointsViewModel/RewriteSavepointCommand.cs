using Kakuro.Base_Classes;
using System.ComponentModel;

namespace Kakuro.Commands.SavepointsViewModel
{
    public class RewriteSavepointCommand : RelayCommand
    {
        private ViewModels.SavepointsViewModel _savepointsViewModel;
        private readonly ViewModels.DashboardViewModel _dashboardViewModel;

        public RewriteSavepointCommand(ViewModels.SavepointsViewModel savepointsViewModel, ViewModels.DashboardViewModel dashboardViewModel)
        {
            _savepointsViewModel = savepointsViewModel;
            _dashboardViewModel = dashboardViewModel;

            _savepointsViewModel.PropertyChanged += OnSelectedSavepointPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _savepointsViewModel.SelectedSavepoint.Dashboard = _dashboardViewModel.CreateDashboardCopy();
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
