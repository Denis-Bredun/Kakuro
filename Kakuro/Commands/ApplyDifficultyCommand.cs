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

        private IDashboardTemplateProvider _templateProvider;
        private ObservableCollection<DashboardItem> _dashboard;

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

            var template = _templateProvider.GenerateTemplate(difficultyLevel);

            var values = GenerateValues(template);


        }

        private int DetermineTheDashboardSize(DifficultyLevels difficultyLevel) => difficultyLevel switch
        {
            DifficultyLevels.Easy => EASY_LEVEL_SIZE,
            DifficultyLevels.Normal => NORMAL_LEVEL_SIZE,
            DifficultyLevels.Hard => HARD_LEVEL_SIZE,
            _ => throw new ArgumentException()
        };

        private int[,] GenerateValues(string[,] template)
        {
            // We need 1st and 3rd rules of Kakuro for generating:
            // 1.Each cell can contain numbers from 1 through 9
            // 2.The clues in the black cells tells the sum of the numbers next to that clue. (on the right or down)
            // 3.The numbers in consecutive white cells must be unique.

            int dashboardSize = template.GetLength(0);
            int[,] values = new int[dashboardSize, dashboardSize];




            for (int i = 0; i < dashboardSize; i++)
                for (int j = 0; j < dashboardSize; j++)
                    if (template[i, j] == "*")
                        values[i, j] = GenerateValueTillItsUnique(values, i, j);

            return values;
        }

        private int GenerateValueTillItsUnique(int[,] values, int i, int j)
        {
            Random random = new Random();
            int value = 0;
            bool isUnique = false;

            do
            {
                isUnique = true;

                value = random.Next(1, 10);

                CheckGeneratedValueForUniqueness(ref isUnique, values, i, j, value);

            } while (!isUnique);

            return value;
        }

        private void CheckGeneratedValueForUniqueness(ref bool isUnique, int[,] values, int i, int j, int value)
        {
            if (!IsUniqueAbove(values, i, j, value))
                isUnique = false;
            if (!IsUniqueBelow(values, i, j, value))
                isUnique = false;
            if (!IsUniqueLeft(values, i, j, value))
                isUnique = false;
            if (!IsUniqueRight(values, i, j, value))
                isUnique = false;
        }

        private bool IsUniqueAbove(int[,] values, int i, int j, int value) => values[i - 1, j] != value;

        private bool IsUniqueBelow(int[,] values, int i, int j, int value) => values[i + 1, j] != value;

        private bool IsUniqueLeft(int[,] values, int i, int j, int value) => values[i, j - 1] != value;

        private bool IsUniqueRight(int[,] values, int i, int j, int value) => values[i, j + 1] != value;
    }
}
