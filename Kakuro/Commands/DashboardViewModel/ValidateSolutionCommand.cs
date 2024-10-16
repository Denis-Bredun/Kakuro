using Kakuro.Base_Classes;
using Kakuro.Interfaces.Game_Tools;
using System.Windows.Input;

namespace Kakuro.Commands.DashboardViewModel
{
    // #BAD: tests shall be written
    public class ValidateSolutionCommand : RelayCommand
    {
        private ISolutionVerifier _solutionVerifier;
        private IOperationNotifier _operationNotifier;
        private ICommand _stopStopwatchCommand;
        private ICommand _sentGameSessionCommand;
        private ViewModels.DashboardViewModel _viewModel;

        public ValidateSolutionCommand(
            ISolutionVerifier solutionVerifier,
            IOperationNotifier operationNotifier,
            ICommand stopStopwatchCommand,
            ICommand sentGameSessionCommand,
            ViewModels.DashboardViewModel viewModel)
        {
            _solutionVerifier ??= solutionVerifier;
            _operationNotifier ??= operationNotifier;
            _stopStopwatchCommand ??= stopStopwatchCommand;
            _sentGameSessionCommand ??= sentGameSessionCommand;
            _viewModel ??= viewModel;
        }

        public override void Execute(object? parameter)
        {
            string message, failMessage = "", successMessage = "";
            bool isSolutionCorrect = _solutionVerifier.ValidateDashboard(out message);

            if (isSolutionCorrect)
            {
                successMessage = message;
                _viewModel.IsGameCompleted = true;
                _stopStopwatchCommand.Execute(parameter);
                _sentGameSessionCommand.Execute(parameter);
            }
            else
                failMessage = message;

            _operationNotifier.NotifyOutcome(isSolutionCorrect, successMessage, failMessage);
        }
    }
}
