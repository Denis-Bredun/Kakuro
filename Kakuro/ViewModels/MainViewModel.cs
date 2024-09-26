using Kakuro.Base_Classes;

namespace Kakuro.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase DashboardViewModel { get; }
        public MainViewModel(ViewModelBase dashboardViewModel)
        {
            DashboardViewModel = dashboardViewModel;
        }
    }
}
