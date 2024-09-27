using Kakuro.Base_Classes;
using Kakuro.Enums;
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

        public ApplyDifficultyCommand(ObservableCollection<DashboardItem> dashboard)
        {
            _dashboard ??= dashboard;
        }

        public override void Execute(object? parameter)
        {
            var difficultyLevel = (DifficultyLevels)parameter;

            _dashboard.Clear();

            const int COUNT_OF_PARALLEL_SIDES = 2;

            int dashboardSize = DetermineTheDashboardSize(difficultyLevel) + BORDER_ITEMS_SIZE * COUNT_OF_PARALLEL_SIDES;

            var template = GenerateTemplateForDashboard(dashboardSize);

            // Dashboard generating...
        }

        private int DetermineTheDashboardSize(DifficultyLevels difficultyLevel) => difficultyLevel switch
        {
            DifficultyLevels.Easy => EASY_LEVEL_SIZE,
            DifficultyLevels.Normal => NORMAL_LEVEL_SIZE,
            DifficultyLevels.Hard => HARD_LEVEL_SIZE,
            _ => throw new NotImplementedException()
        };

        // #* - means border and MAYBE sum
        // * - means sum
        // v - means value

        private string[,] GenerateTemplateForDashboard(int dashboardSize)
        {
            string[,] template = new string[dashboardSize, dashboardSize];
            Random random = new Random();

            for (int i = 0; i < dashboardSize; i++)
                for (int j = 0; j < dashboardSize; j++)
                    template[i, j] = DetermineValueOfTemplate(random, i, j, dashboardSize);

            return template;
        }

        private string DetermineValueOfTemplate(Random random, int i, int j, int dashboardSize)
        {
            if (IsElementOfBorder(i, j, dashboardSize))
                return "#*";
            else
            {
                int randomChoice = random.Next(2);
                return randomChoice == 0 ? "*" : "v";
            }
        }

        private bool IsElementOfBorder(int i, int j, int dashboardSize)
        {
            return i == 0 || i == dashboardSize - 1 || j == 0 || j == dashboardSize - 1;
        }

        private void GenerateDashboard()
        {
            // Dashboard generating...
        }
    }
}
