using Kakuro.Base_Classes;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    public class MainViewModel : ViewModelBase, IDisposable
    {
        public DashboardViewModel DashboardViewModel { get; }
        public RatingTableViewModel RatingTableViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
        public SavepointsViewModel SavepointsViewModel { get; }

        private bool _disposed = false;

        public MainViewModel(
            DashboardViewModel dashboardViewModel,
            RatingTableViewModel ratingTableViewModel,
            SettingsViewModel settingsViewModel,
            SavepointsViewModel savepointsViewModel)
        {
            DashboardViewModel = dashboardViewModel;
            RatingTableViewModel = ratingTableViewModel;
            SettingsViewModel = settingsViewModel;
            SavepointsViewModel = savepointsViewModel;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    RatingTableViewModel.Dispose();
                    DashboardViewModel.Dispose();
                    SettingsViewModel.Dispose();
                    SavepointsViewModel.Dispose();
                }
                _disposed = true;
            }
        }

        ~MainViewModel()
        {
            Dispose(false);
        }
    }
}
