using Kakuro.Base_Classes;
using Kakuro.Enums;
using System.ComponentModel;

namespace Kakuro.Commands.SavepointsViewModel
{
    public class LoadSavepointCommand : RelayCommand
    {
        private ViewModels.SavepointsViewModel _savepointsViewModel;
        private readonly ViewModels.DashboardViewModel _dashboardViewModel;

        public LoadSavepointCommand(ViewModels.SavepointsViewModel savepointsViewModel, ViewModels.DashboardViewModel dashboardViewModel)
        {
            _savepointsViewModel = savepointsViewModel;
            _dashboardViewModel = dashboardViewModel;

            _savepointsViewModel.PropertyChanged += OnSelectedSavepointPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            int dashboardSize = _dashboardViewModel.Dashboard.Count;
            ViewModels.DashboardItemViewModel currentElement;

            for (int i = 1; i < dashboardSize - 1; i++)
            {
                for (int j = 1; j < dashboardSize - 1; j++)
                {
                    currentElement = _dashboardViewModel.Dashboard[i][j];
                    if (currentElement.CellType == CellType.ValueCell)
                        currentElement.DisplayValue = _savepointsViewModel.SelectedSavepoint.Dashboard[i][j].DisplayValue;
                }
            }
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
