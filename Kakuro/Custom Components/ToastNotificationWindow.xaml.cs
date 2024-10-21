using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Kakuro.Custom_Components
{
    /// <summary>
    /// Interaction logic for ToastNotificationWindow.xaml
    /// </summary>
    public partial class ToastNotificationWindow : Window
    {
        public ToastNotificationWindow(string message, string caption, bool isSuccess)
        {
            InitializeComponent();
            NotificationMessage.Text = $"{caption}: {message}";
            NotificationBorder.Background = isSuccess ? Brushes.Green : Brushes.Red;
            Loaded += ToastNotificationWindow_Loaded;
        }

        private void ToastNotificationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var workingArea = SystemParameters.WorkArea;
            Left = (workingArea.Width - Width) / 2;
            Top = workingArea.Height;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = workingArea.Height,
                To = workingArea.Height - Height - 20,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            BeginAnimation(TopProperty, animation);
        }

        public async void ShowAndCloseAfterDelay(int millisecondsDelay)
        {
            Show();
            await Task.Delay(millisecondsDelay);
            CloseNotification();
        }

        private void CloseNotification()
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = Top,
                To = SystemParameters.WorkArea.Height,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };

            animation.Completed += (s, e) => Close();
            BeginAnimation(TopProperty, animation);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseNotification();
        }
    }

}
