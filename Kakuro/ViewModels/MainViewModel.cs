using Kakuro.Base_Classes;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    public class MainViewModel : ViewModelBase
    {
        public DashboardViewModel DashboardViewModel { get; }
        public MainViewModel(DashboardViewModel dashboardViewModel)
        {
            DashboardViewModel = dashboardViewModel;
        }
    }
}
