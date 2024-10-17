using Kakuro.Interfaces.Game_Tools;
using Kakuro.Styles.Custom_Controls;

namespace Kakuro.Game_Tools
{
    // #BAD: tests shall be written
    public class OperationNotifier : IOperationNotifier
    {
        private readonly List<ToastNotificationWindow> _openNotifications;

        public OperationNotifier()
        {
            _openNotifications = new List<ToastNotificationWindow>();
        }

        public void NotifySuccess(string message = "Operation completed successfully.")
        {
            ShowToastNotification(message, "Success", true);
        }

        public void NotifyFailure(string message = "Operation failed.")
        {
            ShowToastNotification(message, "Failure", false);
        }

        public void NotifyOutcome(bool isSuccess,
            string successMessage = "Operation completed successfully.",
            string failMessage = "Operation failed.")
        {
            string caption = isSuccess ? "Success" : "Failure";
            string message = isSuccess ? successMessage : failMessage;
            ShowToastNotification(message, caption, isSuccess);
        }

        private void ShowToastNotification(string message, string caption, bool isSuccess)
        {
            var toast = new ToastNotificationWindow(message, caption, isSuccess);
            toast.ShowAndCloseAfterDelay(10000);
            _openNotifications.Add(toast);
        }

        public void CloseAllNotifications()
        {
            foreach (var notification in _openNotifications.ToList())
                notification.Close();

            _openNotifications.Clear();
        }
    }

}
