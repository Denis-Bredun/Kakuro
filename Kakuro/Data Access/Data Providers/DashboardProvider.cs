using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Models;
using Kakuro.ViewModels;
using System.Collections.ObjectModel;

namespace Kakuro.Data_Access.Data_Providers
{
    public class DashboardProvider : IDashboardProvider
    {
        private IDashboardTemplateProvider _templateProvider;
        private DashboardItemCollection _dashboard;
        private Random _random;

        public DashboardProvider(IDashboardTemplateProvider templateProvider, DashboardItemCollection dashboard)
        {
            _random ??= new Random();
            _dashboard ??= dashboard;
            _templateProvider ??= templateProvider;
        }

        public void GenerateDashboard(DifficultyLevels difficultyLevel)
        {
            _dashboard.Clear();

            var template = _templateProvider.GenerateTemplate(difficultyLevel);

            CreateDashboard(template);

            CalculateSumsAndValues();
        }

        private void CreateDashboard(string[,] dashboard)
        {
            int dashboardSize = dashboard.GetLength(0);

            for (int i = 0; i < dashboardSize; i++)
            {
                _dashboard.Add(new ObservableCollection<DashboardItemViewModel>());

                for (int j = 0; j < dashboardSize; j++)
                {
                    var dashboardItem = new DashboardItem();
                    var wrapper = new DashboardItemViewModel(dashboardItem);

                    if (!IsElementOnBorder(i, j, dashboardSize) && dashboard[i, j] == "*")
                        wrapper.CellType = CellType.ValueCell;

                    _dashboard[i].Add(wrapper);
                }
            }
        }

        private bool IsElementOnBorder(int i, int j, int dashboardSize) => i == 0 || j == 0 || i == dashboardSize - 1 || j == dashboardSize - 1;

        private void CalculateSumsAndValues()
        {
            CalculateBottomSums(true);
            CalculateRightSums();
        }

        private void CalculateBottomSums(bool generateNewValues = false)
        {
            SumCalculatingTemplate(true, generateNewValues);
        }

        private void CalculateRightSums(bool generateNewValues = false)
        {
            SumCalculatingTemplate(false, generateNewValues);
        }

        private void SumCalculatingTemplate(bool isVerticalSum, bool generateNewValues)
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
                        if (generateNewValues)
                        {
                            if (isVerticalSum)
                                GenerateValueTillItsUnique(j, i);
                            else
                                GenerateValueTillItsUnique(i, j);
                        }

                        sum += Convert.ToInt32(currentElement.HiddenValue);
                        wasSumCollected = true;
                    }
                    else if (wasSumCollected)
                    {
                        if (isVerticalSum)
                            currentElement.SumBottom = sum.ToString();
                        else
                            currentElement.SumRight = sum.ToString();

                        currentElement.CellType = CellType.SumCell;
                        sum = 0;
                        wasSumCollected = false;

                    }
                }
            }
        }

        private void GenerateValueTillItsUnique(int i, int j)
        {
            // We need 1st and 3rd rules of Kakuro for generating:
            // 1.Each cell can contain numbers from 1 through 9
            // 2.The clues in the black cells tells the sum of the numbers next to that clue. (on the right or down)
            // 3.The numbers in consecutive white cells must be unique.

            int value = 0;
            bool isUnique;

            do
            {
                isUnique = false;

                value = _random.Next(1, 10);

                isUnique = IsValueUnique(i, j, value);

            } while (!isUnique);

            _dashboard[i][j].HiddenValue = value.ToString();
        }

        private bool IsValueUnique(int i, int j, int value)
        {
            return IsUniqueAbove(i, j, value) && IsUniqueBelow(i, j, value)
                && IsUniqueLeft(i, j, value) && IsUniqueRight(i, j, value);
        }

        private bool IsUniqueAbove(int i, int j, int value) => ConvertDashboardValueToInt(_dashboard[i - 1][j].HiddenValue) != value;

        private bool IsUniqueBelow(int i, int j, int value) => ConvertDashboardValueToInt(_dashboard[i + 1][j].HiddenValue) != value;

        private bool IsUniqueLeft(int i, int j, int value) => ConvertDashboardValueToInt(_dashboard[i][j - 1].HiddenValue) != value;

        private bool IsUniqueRight(int i, int j, int value) => ConvertDashboardValueToInt(_dashboard[i][j - 1].HiddenValue) != value;

        private int ConvertDashboardValueToInt(string value) => string.IsNullOrEmpty(value) ? 0 : Convert.ToInt32(value);
    }
}
