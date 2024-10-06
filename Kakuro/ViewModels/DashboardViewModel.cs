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
        private bool _isMakingNotes;

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
                if (_selectedCell != null)
                    _selectedCell.IsSelected = false;

                if (value != null && value.CellType == CellType.ValueCell)
                {
                    _selectedCell = value;
                    _selectedCell.IsSelected = true;
                }
                else
                    _selectedCell = null;

                OnPropertyChanged(nameof(SelectedCell));
            }
        }

        public bool IsMakingNotes
        {
            get => _isMakingNotes;
            set
            {
                _isMakingNotes = value;
                OnPropertyChanged(nameof(IsMakingNotes));
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
