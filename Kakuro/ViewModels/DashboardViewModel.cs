using Kakuro.Base_Classes;

using DoubleObservableCollection = System.Collections.ObjectModel.ObservableCollection<System.Collections.ObjectModel.ObservableCollection<Kakuro.Models.DashboardItem>>;

namespace Kakuro.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public DoubleObservableCollection Dashboard { get; }



        public DashboardViewModel()
        {
            Dashboard = new DoubleObservableCollection();
        }
    }
}
