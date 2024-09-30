using System.Windows.Input;

namespace Kakuro.Interfaces.ViewModels
{
    public interface IDashboardViewModel
    {
        DashboardItemCollection Dashboard { get; }
        int CountOfRows { get; }
        int CountOfColumns { get; }
        ICommand ApplyDifficultyCommand { get; }
    }
}
