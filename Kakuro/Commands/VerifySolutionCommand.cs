using Kakuro.Base_Classes;
using Kakuro.Interfaces.Game_Tools;
using System.Windows;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class VerifySolutionCommand : RelayCommand
    {
        private ISolutionVerifier _solutionVerifier;

        public VerifySolutionCommand(ISolutionVerifier solutionVerifier)
        {
            _solutionVerifier = solutionVerifier;
        }

        public override void Execute(object? parameter)
        {
            bool isSolutionCorrect = _solutionVerifier.VerifyDashboardValues();

            MessageBox.Show(isSolutionCorrect.ToString()); // #PRIORITY: we should write tool for notifications output
        }
    }
}
