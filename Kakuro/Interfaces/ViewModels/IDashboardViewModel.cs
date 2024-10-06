using Kakuro.Enums;
using System.Windows.Input;

namespace Kakuro.Interfaces.ViewModels
{
    public interface IDashboardViewModel
    {
        DashboardItemCollection Dashboard { get; }

        DifficultyLevels ChoosenDifficulty { get; set; }

        ICommand ApplyDifficultyCommand { get; }
        ICommand NewGameCommand { get; }
        ICommand VerifySolutionCommand { get; }
        ICommand CleanDashboardCommand { get; }
    }
}
