using Kakuro.Base_Classes;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    public class MainViewModel : ViewModelBase, IDisposable
    {
        public DashboardViewModel DashboardViewModel { get; }
        public RatingTableViewModel RatingTableViewModel { get; }

        private bool _disposed = false;

        public MainViewModel(DashboardViewModel dashboardViewModel, RatingTableViewModel ratingTableViewModel)
        {
            DashboardViewModel = dashboardViewModel;
            RatingTableViewModel = ratingTableViewModel;
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
