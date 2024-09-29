using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Models;
using Kakuro.ViewModels;
using System.Collections.ObjectModel;

using DashboardItemCollection = System.Collections.ObjectModel.ObservableCollection<System.Collections.ObjectModel.ObservableCollection<Kakuro.ViewModels.DashboardItemViewModel>>;

namespace Kakuro.Data_Access.Data_Providers
{
    public class DashboardProvider : IDashboardProvider
    {
        private IDashboardTemplateProvider _templateProvider;
        private DashboardItemCollection _dashboard;

        public DashboardProvider(IDashboardTemplateProvider templateProvider, DashboardItemCollection dashboard)
        {
            _dashboard ??= dashboard;
            _templateProvider ??= templateProvider;
        }

        public void GenerateDashboard(DifficultyLevels difficultyLevel)
        {
            _dashboard.Clear();

            var template = _templateProvider.GenerateTemplate(difficultyLevel);

            var values = GenerateValues(template);

            CreateDashboard(values.GetLength(0));

            InitializeDashboard(values);
        }

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
            bool isUnique;

            do
            {
                isUnique = false;

                value = random.Next(1, 10);

                isUnique = IsValueUnique(values, i, j, value);

            } while (!isUnique);

            return value;
        }

        private bool IsValueUnique(int[,] values, int i, int j, int value)
        {
            return IsUniqueAbove(values, i, j, value) && IsUniqueBelow(values, i, j, value)
                && IsUniqueLeft(values, i, j, value) && IsUniqueRight(values, i, j, value);
        }

        private bool IsUniqueAbove(int[,] values, int i, int j, int value) => values[i - 1, j] != value;

        private bool IsUniqueBelow(int[,] values, int i, int j, int value) => values[i + 1, j] != value;

        private bool IsUniqueLeft(int[,] values, int i, int j, int value) => values[i, j - 1] != value;

        private bool IsUniqueRight(int[,] values, int i, int j, int value) => values[i, j + 1] != value;

        private void CreateDashboard(int dashboardSize)
        {
            for (int i = 0; i < dashboardSize; i++)
            {
                _dashboard.Add(new ObservableCollection<DashboardItemViewModel>());

                for (int j = 0; j < dashboardSize; j++)
                    _dashboard[i].Add(new DashboardItemViewModel(new DashboardItem()));
            }
        }

        private void InitializeDashboard(int[,] values)
        {
            FillDashboardWithValues(values);

            CalculateSums();
        }

        private void FillDashboardWithValues(int[,] values)
        {
            int dashboardSize = values.GetLength(0);
            DashboardItemViewModel currentElement;

            for (int i = 0; i < dashboardSize; i++)
                for (int j = 0; j < dashboardSize; j++)
                    if (values[i, j] != 0)
                    {
                        currentElement = _dashboard[i][j];
                        currentElement.HiddenValue = values[i, j];
                        currentElement.CellType = CellType.ValueCell;
                    }
        }

        private void CalculateSums()
        {
            CalculateBottomSums();
            CalculateRightSums();
        }

        private void CalculateBottomSums()
        {
            SumCalculatingTemplate(true);
        }

        private void CalculateRightSums()
        {
            SumCalculatingTemplate(false);
        }

        private void SumCalculatingTemplate(bool isVerticalSum)
        {
            DashboardItemViewModel currentElement;
            int sum = 0;
            bool wasSumCollected = false;

            for (int i = 0; i < _dashboard.Count; i++)
            {
                for (int j = _dashboard.Count - 1; j >= 0; j--)
                {
                    currentElement = isVerticalSum ? _dashboard[j][i] : _dashboard[i][j];

                    if (currentElement.CellType == CellType.ValueCell)
                    {
                        sum += currentElement.HiddenValue;
                        wasSumCollected = true;
                    }
                    else if (wasSumCollected)
                    {
                        if (isVerticalSum)
                            currentElement.SumBottom = sum;
                        else
                            currentElement.SumRight = sum;

                        currentElement.CellType = CellType.SumCell;
                        sum = 0;
                        wasSumCollected = false;

                    }
                }
            }
        }
    }
}
