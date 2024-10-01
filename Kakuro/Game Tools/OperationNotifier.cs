using Kakuro.Interfaces.Game_Tools;
using System.Windows;

namespace Kakuro.Game_Tools
{
    // #BAD: tests shall be written
    public class OperationNotifier : IOperationNotifier
    {
        public void NotifySuccess(string message = "Operation completed successfully.")
        {
            MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
        }

        public void NotifyFailure(string message = "Operation failed.")
        {
            MessageBox.Show(message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
        }

        public void NotifyOutcome(bool isSuccess,
            string successMessage = "Operation completed successfully.",
            string failMessage = "Operation failed.")
        {
            string caption = isSuccess ? "Success" : "Failure";
            string message = isSuccess ? successMessage : failMessage;
            MessageBoxImage icon = isSuccess ? MessageBoxImage.Information : MessageBoxImage.Error;

            MessageBox.Show(message, caption, MessageBoxButton.OK, icon, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
