using Kakuro.Enums;
using System.Windows.Input;

namespace Kakuro.Interfaces.ViewModels
{
    public interface IDashboardViewModel
    {
        DashboardItemCollection Dashboard { get; }
        DifficultyLevels ChoosenDifficulty { get; set; }
        public ICommand ApplyDifficultyCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand VerifySolutionCommand { get; }
        public ICommand CleanDashboardCommand { get; }
    }
}
