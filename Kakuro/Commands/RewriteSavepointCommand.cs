using Kakuro.Base_Classes;
using Kakuro.Models;
using Kakuro.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Kakuro.Commands
{
    public class RewriteSavepointCommand : RelayCommand
    {
        private SavepointsViewModel _savepointsViewModel;
        private readonly DashboardViewModel _dashboardViewModel;

        public RewriteSavepointCommand(SavepointsViewModel savepointsViewModel, DashboardViewModel dashboardViewModel)
        {
            _savepointsViewModel = savepointsViewModel;
            _dashboardViewModel = dashboardViewModel;

            _savepointsViewModel.PropertyChanged += OnSelectedSavepointPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _savepointsViewModel.SelectedSavepoint.Dashboard = CreateDashboardCopy(_dashboardViewModel.Dashboard);
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

        private DashboardItemCollection CreateDashboardCopy(DashboardItemCollection original)
        {
            var newCollection = new DashboardItemCollection();
            foreach (var innerCollection in original)
            {
                var newInnerCollection = new ObservableCollection<DashboardItemViewModel>();
                foreach (var item in innerCollection)
                {
                    var newItem = new DashboardItemViewModel(new DashboardItem
                    {
                        DisplayValue = item.ConvertStringToInt(item.DisplayValue),
                        HiddenValue = item.ConvertStringToInt(item.HiddenValue),
                        SumRight = item.ConvertStringToInt(item.SumRight),
                        SumBottom = item.ConvertStringToInt(item.SumBottom),
                        CellType = item.CellType
                    });
                    newInnerCollection.Add(newItem);
                }
                newCollection.Add(newInnerCollection);
            }
            return newCollection;
        }
    }
}
