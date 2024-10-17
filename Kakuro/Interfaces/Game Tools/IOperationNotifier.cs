namespace Kakuro.Interfaces.Game_Tools
{
    public interface IOperationNotifier
    {
        void NotifySuccess(string message = "Operation completed successfully.");
        void NotifyFailure(string message = "Operation failed.");
        void NotifyOutcome(bool isSuccess,
            string successMessage = "Operation completed successfully.",
            string failMessage = "Operation failed.");
        void CloseAllNotifications();
    }
}
