using Kakuro.ViewModels;
using System.Windows;

namespace Kakuro
{
    public partial class MainWindow : Window, IDisposable
    {
        private MainViewModel _mainViewModel;
        private bool _disposed = false;

        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();

            _mainViewModel = mainViewModel;
            DataContext = _mainViewModel;
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
                    _mainViewModel.Dispose();
                }
                _disposed = true;
            }
        }

        ~MainWindow()
        {
            Dispose(false);
        }

        private void Window_Closed(object sender, EventArgs e) // #BAD: i guess i shall do it using commands
        {
            Dispose();
            base.OnClosed(e);
        }
    }
}