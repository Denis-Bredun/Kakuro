using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Models;
using Kakuro.ViewModels;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

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
            var dashboard = CreateDashboardCopy(_dashboardViewModel.Dashboard);

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

        private DashboardItemCollection CreateDashboardCopy(DashboardItemCollection original)
        {
            var newCollection = new DashboardItemCollection();
            foreach (var innerCollection in original)
            {
                var newInnerCollection = new ObservableCollection<DashboardItemViewModel>();
                foreach (var item in innerCollection)
                {
                    if (item.CellType != CellType.ValueCell)
                        continue;

                    var newItem = new DashboardItemViewModel(new DashboardItem
                    {
                        DisplayValue = item.ConvertStringToInt(item.DisplayValue),
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
