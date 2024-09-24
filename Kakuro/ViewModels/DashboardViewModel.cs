using System.Collections.ObjectModel;

namespace Kakuro.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public ObservableCollection<DashboardItemViewModel> Dashboard { get; }

        public DashboardViewModel()
        {
            Dashboard = new ObservableCollection<DashboardItemViewModel>();
        }
    }
}
