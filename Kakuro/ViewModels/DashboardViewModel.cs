using Kakuro.Base_Classes;
using Kakuro.Commands;
using Kakuro.Data_Access.Data_Providers;
using Kakuro.Enums;
using System.Windows.Input;
using DashboardItemCollection = System.Collections.ObjectModel.ObservableCollection<System.Collections.ObjectModel.ObservableCollection<Kakuro.ViewModels.DashboardItemViewModel>>;

namespace Kakuro.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private const DifficultyLevels DEFAULT_DIFFICULTY = DifficultyLevels.Easy;

        public DashboardItemCollection Dashboard { get; }
        public int CountOfRows => Dashboard.Count;
        public int CountOfColumns => Dashboard.Count;

        public ICommand ApplyDifficultyCommand { get; }

        public DashboardViewModel()
        {
            Dashboard = new DashboardItemCollection();
            ApplyDifficultyCommand = new ApplyDifficultyCommand(new DashboardProvider(new DashboardTemplateProvider(), Dashboard), Dashboard); // #BAD: we need DI! I already have a commend about it

            ApplyDifficultyCommand.Execute(DEFAULT_DIFFICULTY);
        }
    }
}
