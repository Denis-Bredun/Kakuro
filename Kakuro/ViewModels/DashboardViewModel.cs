using Autofac;
using Kakuro.Base_Classes;
using Kakuro.Commands;
using Kakuro.Enums;
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

        public DashboardViewModel(ILifetimeScope scope, DashboardItemCollection dashboard)
        {
            Dashboard = dashboard;
            ApplyDifficultyCommand = scope.Resolve<ApplyDifficultyCommand>();
            VerifySolutionCommand = scope.Resolve<VerifySolutionCommand>();

            ApplyDifficultyCommand.Execute(DEFAULT_DIFFICULTY);
        }
    }
}
