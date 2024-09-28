using Kakuro.Base_Classes;

using DashboardItemCollection = System.Collections.ObjectModel.ObservableCollection<System.Collections.ObjectModel.ObservableCollection<Kakuro.ViewModels.DashboardItemViewModel>>;

namespace Kakuro.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public DashboardItemCollection Dashboard { get; }



        public DashboardViewModel()
        {
            Dashboard = new DashboardItemCollection();
        }
    }
}
