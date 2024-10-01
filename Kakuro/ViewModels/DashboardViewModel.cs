using Kakuro.Base_Classes;
using Kakuro.Commands;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Game_Tools;
using Kakuro.Interfaces.ViewModels;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    public class DashboardViewModel : ViewModelBase, IDashboardViewModel
    {
        private const DifficultyLevels DEFAULT_DIFFICULTY = DifficultyLevels.Easy;

        public DashboardItemCollection Dashboard { get; }
        public int CountOfRows => Dashboard.Count;
        public int CountOfColumns => Dashboard.Count;

        public ICommand ApplyDifficultyCommand { get; }
        public ICommand VerifySolutionCommand { get; }

        public DashboardViewModel(IDashboardProvider dashboardProvider, ISolutionVerifier solutionVerifier, DashboardItemCollection dashboard)
        {
            Dashboard = dashboard;
            ApplyDifficultyCommand = new ApplyDifficultyCommand(dashboardProvider);
            VerifySolutionCommand = new VerifySolutionCommand(solutionVerifier);

            ApplyDifficultyCommand.Execute(DEFAULT_DIFFICULTY);
        }
    }
}
