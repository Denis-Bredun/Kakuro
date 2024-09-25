using System.Collections.ObjectModel;

namespace Kakuro.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public ObservableCollection<DashboardItemViewModel> Dashboard { get; }

        public DashboardViewModel()
        {
            Dashboard = new ObservableCollection<DashboardItemViewModel>();

            // #PRIORITY: make forming the size of collection depending on the Difficulty level

            for (int i = 0; i < 36; i++)
            {
                Dashboard.Add(new DashboardItemViewModel(new Models.DashboardItem()));
            }
        }
    }
}
