using Kakuro.Base_Classes;
using Kakuro.Data_Access.Data_Providers;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Models;
using System.Collections.ObjectModel;

namespace Kakuro.Commands
{
    public class ApplyDifficultyCommand : RelayCommand
    {
        private const int
            EASY_LEVEL_SIZE = 6,
            NORMAL_LEVEL_SIZE = 10,
            HARD_LEVEL_SIZE = 16,
            BORDER_ITEMS_SIZE = 1; // That is, we have a game dashboard, for example, 6x6, in which there are
                                   // cells for entering a value, and we also have an additional line of
                                   // elements on the right, top, left and bottom, on which we write the
                                   // sums of the numbers.

        private ObservableCollection<DashboardItem> _dashboard;
        private IDashboardTemplateProvider _templateProvider;

        public ApplyDifficultyCommand(DashboardTemplateProvider templateProvider, ObservableCollection<DashboardItem> dashboard)
        {
            _dashboard ??= dashboard;
            _templateProvider = templateProvider;
        }

        public override void Execute(object? parameter)
        {
            var difficultyLevel = (DifficultyLevels)parameter;

            _dashboard.Clear();

            const int COUNT_OF_PARALLEL_SIDES = 2;

            int dashboardSize = DetermineTheDashboardSize(difficultyLevel) + BORDER_ITEMS_SIZE * COUNT_OF_PARALLEL_SIDES;

            // Template generating...

            // Dashboard generating...
        }

        private int DetermineTheDashboardSize(DifficultyLevels difficultyLevel) => difficultyLevel switch
        {
            DifficultyLevels.Easy => EASY_LEVEL_SIZE,
            DifficultyLevels.Normal => NORMAL_LEVEL_SIZE,
            DifficultyLevels.Hard => HARD_LEVEL_SIZE,
            _ => throw new NotImplementedException()
        };
    }
}
