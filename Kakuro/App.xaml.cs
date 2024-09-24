using Kakuro.ViewModels;
using System.Windows;

namespace Kakuro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(new DashboardViewModel())
            };

            MainWindow.Show();

            // #PRIORITY: Make Dependency Injenction

            base.OnStartup(e);
        }
    }

}
