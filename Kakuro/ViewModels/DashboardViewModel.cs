using Autofac;
using Kakuro.Base_Classes;
using Kakuro.Commands;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.ViewModels;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    public class DashboardViewModel : ViewModelBase, IDashboardViewModel
    {
        private DifficultyLevels _choosenDifficulty;
        private IDashboardItemViewModel _selectedCell;

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

        public IDashboardItemViewModel SelectedCell
        {
            get => _selectedCell;
            set
            {
                _selectedCell = value;
                OnPropertyChanged("SelectedCell");
            }
        }

        public ICommand ApplyDifficultyCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand VerifySolutionCommand { get; }
        public ICommand CleanDashboardCommand { get; }
        public ICommand EraseSelectedCellCommand { get; }
        public ICommand CellGotFocusCommand { get; }

        public DashboardViewModel(ILifetimeScope scope, DashboardItemCollection dashboard)
        {
            ChoosenDifficulty = DifficultyLevels.Easy;
            Dashboard = dashboard;

            VerifySolutionCommand = scope.Resolve<VerifySolutionCommand>();
            ApplyDifficultyCommand = new ApplyDifficultyCommand(scope.Resolve<IDashboardProvider>(), this);
            CleanDashboardCommand = scope.Resolve<CleanDashboardCommand>();
            EraseSelectedCellCommand = new EraseSelectedCellCommand(this);
            CellGotFocusCommand = new CellGotFocusCommand(OnCellGotFocus);
            NewGameCommand = ApplyDifficultyCommand;

            ApplyDifficultyCommand.Execute(ChoosenDifficulty);
        }

        private void OnCellGotFocus(IDashboardItemViewModel selectedCell)
        {
            SelectedCell = selectedCell; // #BAD: shall this logic be in CellGotFocusCommand class?
        }
    }
}
