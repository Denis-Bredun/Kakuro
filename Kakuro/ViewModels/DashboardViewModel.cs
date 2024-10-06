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
        private DifficultyLevels _choosenDifficulty;

        public DashboardItemCollection Dashboard { get; }
        public DifficultyLevels ChoosenDifficulty
        {
            get => _choosenDifficulty;
            set
            {
                _choosenDifficulty = value;
                OnPropertyChanged("ChoosenDifficulty");
            }
        }

        public ICommand ApplyDifficultyCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand VerifySolutionCommand { get; }

        public DashboardViewModel(ILifetimeScope scope, DashboardItemCollection dashboard)
        {
            ChoosenDifficulty = DifficultyLevels.Easy;
            Dashboard = dashboard;

            VerifySolutionCommand = scope.Resolve<VerifySolutionCommand>();
            ApplyDifficultyCommand = scope.Resolve<ApplyDifficultyCommand>();
            NewGameCommand = ApplyDifficultyCommand;
            ((ApplyDifficultyCommand)ApplyDifficultyCommand).DashboardViewModel = this; // #BAD: I don't think that the way we set DashboardViewModel outside the class
                                                                                        // is a good practice

            ApplyDifficultyCommand.Execute(ChoosenDifficulty);
        }
    }
}
