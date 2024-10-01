using Kakuro.Base_Classes;
using Kakuro.Interfaces.Game_Tools;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class VerifySolutionCommand : RelayCommand
    {
        private ISolutionVerifier _solutionVerifier;
        private readonly IOperationNotifier _operationNotifier;

        public VerifySolutionCommand(ISolutionVerifier solutionVerifier, IOperationNotifier operationNotifier)
        {
            _solutionVerifier = solutionVerifier;
            _operationNotifier = operationNotifier;
        }

        public override void Execute(object? parameter)
        {
            string message, failMessage = "", successMessage = "";
            bool isSolutionCorrect = _solutionVerifier.VerifyDashboardValues(out message);

            if (isSolutionCorrect)
                successMessage = message;
            else
                failMessage = message;

            _operationNotifier.NotifyOutcome(isSolutionCorrect, successMessage, failMessage);
        }
    }
}
