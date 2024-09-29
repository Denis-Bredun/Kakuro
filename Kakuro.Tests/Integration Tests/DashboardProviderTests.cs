using Kakuro.Data_Access.Data_Providers;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Models;
using Kakuro.ViewModels;
using System.Collections.ObjectModel;
using DashboardItemCollection = System.Collections.ObjectModel.ObservableCollection<System.Collections.ObjectModel.ObservableCollection<Kakuro.ViewModels.DashboardItemViewModel>>;

namespace Kakuro.Tests.Integration_Tests
{
    public class DashboardProviderTests : IDisposable
    {
        private IDashboardTemplateProvider _dashboardTemplateProvider;
        private DashboardItemCollection _dashboardItemCollection;
        private IDashboardProvider _dashboardProvider;

        public DashboardProviderTests()
        {
            _dashboardItemCollection = new DashboardItemCollection();
            _dashboardTemplateProvider = new DashboardTemplateProvider();
            _dashboardProvider = new DashboardProvider(_dashboardTemplateProvider, _dashboardItemCollection);
        }

        public void Dispose()
        {
            _dashboardItemCollection.Clear();
        }

        [Fact]
        public void Should_FitTemplate_When_GeneratedWithConcreteDifficulty()
        {
            // Arrange
            var difficulties = Enum.GetValues(typeof(DifficultyLevels));

            foreach (DifficultyLevels difficulty in difficulties)
            {
                // Arrange
                var template = _dashboardTemplateProvider.GenerateTemplate(difficulty);

                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                for (int i = 0; i < _dashboardItemCollection.Count; i++)
                {
                    for (int j = 0; j < _dashboardItemCollection[i].Count; j++)
                    {
                        if (_dashboardItemCollection[i][j].HiddenValue != 0)
                            Assert.Equal("*", template[i, j]);
                        else
                            Assert.Empty(template[i, j]);
                    }
                }
            }
        }

        [Fact]
        public void Should_NotMatchOtherDifficultyTemplate_When_GeneratedWithConcreteDifficulty()
        {
            // Arrange
            var easyTemplate = _dashboardTemplateProvider.GenerateTemplate(DifficultyLevels.Easy);
            var hardTemplate = _dashboardTemplateProvider.GenerateTemplate(DifficultyLevels.Hard);

            // Act
            _dashboardProvider.GenerateDashboard(DifficultyLevels.Easy);

            // Assert
            for (int i = 0; i < _dashboardItemCollection.Count; i++)
            {
                for (int j = 0; j < _dashboardItemCollection[i].Count; j++)
                {
                    if (easyTemplate[i, j] == "*" && hardTemplate[i, j] == "")
                    {
                        Assert.NotEqual(0, _dashboardItemCollection[i][j].HiddenValue);
                    }
                    else if (easyTemplate[i, j] == "" && hardTemplate[i, j] == "*")
                    {
                        Assert.Equal(0, _dashboardItemCollection[i][j].HiddenValue);
                    }
                    else if (easyTemplate[i, j] == "" && hardTemplate[i, j] == "")
                    {
                        Assert.Equal(0, _dashboardItemCollection[i][j].HiddenValue);
                    }
                    else
                    {
                        Assert.NotEqual(0, _dashboardItemCollection[i][j].HiddenValue);
                    }
                }
            }
        }

        [Fact]
        public void Should_MatchTemplateSize_After_ConsecutiveGenerationsWithDifferentDifficulties()
        {
            // Arrange
            var easyTemplate = _dashboardTemplateProvider.GenerateTemplate(DifficultyLevels.Easy);
            var hardTemplate = _dashboardTemplateProvider.GenerateTemplate(DifficultyLevels.Hard);

            // Act
            _dashboardProvider.GenerateDashboard(DifficultyLevels.Easy);
            _dashboardProvider.GenerateDashboard(DifficultyLevels.Hard);

            // Assert
            Assert.NotEqual(easyTemplate.GetLength(0), _dashboardItemCollection.Count);
            Assert.NotEqual(easyTemplate.GetLength(1), _dashboardItemCollection[0].Count);
            Assert.Equal(hardTemplate.GetLength(0), _dashboardItemCollection.Count);
            Assert.Equal(hardTemplate.GetLength(1), _dashboardItemCollection[0].Count);
        }

        [Fact]
        public void Should_ThrowArgumentException_When_InvalidDifficultyIsPassed()
        {
            // Arrange
            var invalidDifficulty = (DifficultyLevels)999;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _dashboardProvider.GenerateDashboard(invalidDifficulty));
        }

        [Fact]
        public void Should_HaveAtLeastOneNonZeroValue_When_DashboardIsGenerated()
        {
            // Arrange
            var difficulties = Enum.GetValues(typeof(DifficultyLevels));

            foreach (DifficultyLevels difficulty in difficulties)
            {
                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                var hasNonZeroValue = _dashboardItemCollection.Any(row => row.Any(item => item.HiddenValue != 0));
                Assert.True(hasNonZeroValue);
            }
        }

        [Fact]
        public void Should_CalculateCorrectSums_When_DashboardIsGenerated()
        {
            // Arrange
            var difficulties = Enum.GetValues(typeof(DifficultyLevels));

            foreach (DifficultyLevels difficulty in difficulties)
            {
                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                bool areVerticalSumsCorrect = ValidateSums(isVerticalSum: true);
                bool areHorizontalSumsCorrect = ValidateSums(isVerticalSum: false);
                Assert.True(areVerticalSumsCorrect && areHorizontalSumsCorrect);
            }
        }

        private bool ValidateSums(bool isVerticalSum)
        {
            for (int i = 0; i < _dashboardItemCollection.Count; i++)
            {
                int calculatedSum = 0;
                bool wasSumCollected = false;

                for (int j = _dashboardItemCollection.Count - 1; j >= 0; j--)
                {
                    var currentElement = isVerticalSum ? _dashboardItemCollection[j][i] : _dashboardItemCollection[i][j];

                    if (currentElement.CellType == CellType.ValueCell)
                    {
                        calculatedSum += currentElement.HiddenValue;
                        wasSumCollected = true;
                    }
                    else if (wasSumCollected)
                    {
                        var sumToCheck = isVerticalSum ? currentElement.SumBottom : currentElement.SumRight;

                        if (sumToCheck != calculatedSum)
                        {
                            return false;
                        }
                        calculatedSum = 0;
                        wasSumCollected = false;
                    }
                }
            }
            return true;
        }

        [Fact]
        public void Should_HaveCorrectCellTypes_When_DashboardIsGenerated()
        {
            // Arrange
            var difficulties = Enum.GetValues(typeof(DifficultyLevels));

            foreach (DifficultyLevels difficulty in difficulties)
            {
                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                bool areCellTypesCorrect = true;

                for (int i = 0; i < _dashboardItemCollection.Count; i++)
                {
                    for (int j = 0; j < _dashboardItemCollection[i].Count; j++)
                    {
                        var currentElement = _dashboardItemCollection[i][j];

                        if (currentElement.HiddenValue != 0)
                        {
                            if (currentElement.CellType != CellType.ValueCell)
                            {
                                areCellTypesCorrect = false;
                            }
                        }
                        else if (currentElement.SumBottom != 0 || currentElement.SumRight != 0)
                        {
                            if (currentElement.CellType != CellType.SumCell)
                            {
                                areCellTypesCorrect = false;
                            }
                        }
                        else
                        {
                            if (currentElement.CellType != CellType.EmptyCell)
                            {
                                areCellTypesCorrect = false;
                            }
                        }
                    }
                }

                Assert.True(areCellTypesCorrect);
            }
        }

        [Fact]
        public void Should_HaveCorrectCellTypes_ForAllDifficulties_When_ComparedToTemplates()
        {
            // Arrange
            var difficulties = Enum.GetValues(typeof(DifficultyLevels));
            bool areCellTypesCorrect = true;

            foreach (DifficultyLevels difficulty in difficulties)
            {
                // Arrange
                var template = _dashboardTemplateProvider.GenerateTemplate(difficulty);

                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                for (int i = 0; i < _dashboardItemCollection.Count; i++)
                {
                    for (int j = 0; j < _dashboardItemCollection[i].Count; j++)
                    {
                        var currentElement = _dashboardItemCollection[i][j];
                        var templateValue = template[i, j];

                        if (templateValue == "*")
                        {
                            if (currentElement.CellType != CellType.ValueCell)
                            {
                                areCellTypesCorrect = false;
                            }
                        }
                        else // templateValue == ""
                        {
                            if (currentElement.CellType != CellType.SumCell && currentElement.CellType != CellType.EmptyCell)
                            {
                                areCellTypesCorrect = false;
                            }
                        }
                    }
                }
            }

            Assert.True(areCellTypesCorrect);
        }

        [Fact]
        public void Should_HaveUniqueValues_When_DashboardIsGenerated()
        {
            // Arrange
            var difficulties = Enum.GetValues(typeof(DifficultyLevels));
            foreach (DifficultyLevels difficulty in difficulties)
            {
                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                bool areValuesUnique = true;

                for (int i = 0; i < _dashboardItemCollection.Count; i++)
                {
                    for (int j = 0; j < _dashboardItemCollection[i].Count; j++)
                    {
                        var currentElement = _dashboardItemCollection[i][j];

                        if (currentElement.CellType == CellType.ValueCell)
                        {
                            int currentValue = currentElement.HiddenValue;

                            if (!IsValueUnique(i, j, currentValue))
                            {
                                areValuesUnique = false;
                                break;
                            }
                        }
                    }

                    if (!areValuesUnique)
                    {
                        break;
                    }
                }

                Assert.True(areValuesUnique);
            }
        }

        private bool IsValueUnique(int i, int j, int value)
        {
            return IsUniqueAbove(i, j, value) && IsUniqueBelow(i, j, value)
                && IsUniqueLeft(i, j, value) && IsUniqueRight(i, j, value);
        }

        private bool IsUniqueAbove(int i, int j, int value)
        {
            return _dashboardItemCollection[i - 1][j].HiddenValue != value;
        }

        private bool IsUniqueBelow(int i, int j, int value)
        {
            return _dashboardItemCollection[i + 1][j].HiddenValue != value;
        }

        private bool IsUniqueLeft(int i, int j, int value)
        {
            return _dashboardItemCollection[i][j - 1].HiddenValue != value;
        }

        private bool IsUniqueRight(int i, int j, int value)
        {
            return _dashboardItemCollection[i][j + 1].HiddenValue != value;
        }

        [Fact]
        public void Should_HaveSumsInExpectedCells_When_DashboardIsGenerated()
        {
            // Arrange
            var difficulties = Enum.GetValues(typeof(DifficultyLevels));
            foreach (DifficultyLevels difficulty in difficulties)
            {
                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                for (int i = 0; i < _dashboardItemCollection.Count; i++)
                {
                    for (int j = 0; j < _dashboardItemCollection[i].Count; j++)
                    {
                        var currentElement = _dashboardItemCollection[i][j];

                        if (currentElement.CellType == CellType.SumCell)
                        {
                            Assert.True(currentElement.SumBottom != 0 || currentElement.SumRight != 0);
                        }
                    }
                }
            }
        }

        [Fact]
        public void Should_HaveExpectedNumberOfNonZeroValues_When_DashboardIsGenerated()
        {
            // Arrange
            var expectedCounts = new Dictionary<DifficultyLevels, int>
            {
                { DifficultyLevels.Easy, CountStarsInTemplate(DifficultyLevels.Easy) },
                { DifficultyLevels.Normal, CountStarsInTemplate(DifficultyLevels.Normal) },
                { DifficultyLevels.Hard, CountStarsInTemplate(DifficultyLevels.Hard) }
            };

            foreach (var difficulty in expectedCounts.Keys)
            {
                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                var nonZeroCount = _dashboardItemCollection.Sum(row => row.Count(item => item.CellType == CellType.ValueCell));
                Assert.Equal(expectedCounts[difficulty], nonZeroCount);
            }
        }

        private int CountStarsInTemplate(DifficultyLevels difficulty)
        {
            string[,] template = _dashboardTemplateProvider.GenerateTemplate(difficulty);

            int starCount = 0;

            for (int i = 0; i < template.GetLength(0); i++)
                for (int j = 0; j < template.GetLength(1); j++)
                    if (template[i, j] == "*")
                        starCount++;

            return starCount;
        }

        [Fact]
        public void Should_ContainOnlyExpectedValues_InEmptyCells_When_DashboardIsGenerated()
        {
            // Arrange
            var difficulties = Enum.GetValues(typeof(DifficultyLevels));
            foreach (DifficultyLevels difficulty in difficulties)
            {
                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                for (int i = 0; i < _dashboardItemCollection.Count; i++)
                {
                    for (int j = 0; j < _dashboardItemCollection[i].Count; j++)
                    {
                        var currentElement = _dashboardItemCollection[i][j];

                        if (currentElement.CellType == CellType.EmptyCell)
                        {
                            Assert.Equal(0, currentElement.HiddenValue);
                            Assert.Equal(0, currentElement.SumBottom);
                            Assert.Equal(0, currentElement.SumRight);
                        }
                    }
                }
            }
        }

        [Fact]
        public void Should_HaveValuesInExpectedRange_When_DashboardIsGenerated()
        {
            // Arrange
            var difficulties = Enum.GetValues(typeof(DifficultyLevels));
            foreach (DifficultyLevels difficulty in difficulties)
            {
                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                foreach (var row in _dashboardItemCollection)
                {
                    foreach (var item in row)
                    {
                        if (item.HiddenValue != 0)
                        {
                            Assert.InRange(item.HiddenValue, 1, 9);
                        }
                    }
                }
            }
        }

        [Fact]
        public void Should_HaveNoHiddenValuesInSumCells_When_DashboardIsGenerated()
        {
            // Arrange
            var difficulties = Enum.GetValues(typeof(DifficultyLevels));
            foreach (DifficultyLevels difficulty in difficulties)
            {
                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                foreach (var row in _dashboardItemCollection)
                {
                    foreach (var item in row)
                    {
                        if (item.CellType == CellType.SumCell)
                        {
                            Assert.Equal(0, item.HiddenValue);
                        }
                    }
                }
            }
        }

        [Fact]
        public void Should_UpdateDashboardCorrectly_When_DifficultyIsChanged()
        {
            // Arrange
            _dashboardProvider.GenerateDashboard(DifficultyLevels.Easy);
            var initialDashboard = _dashboardItemCollection.ToArray();

            // Act
            _dashboardProvider.GenerateDashboard(DifficultyLevels.Hard);
            var updatedDashboard = _dashboardItemCollection.ToArray();

            // Assert
            Assert.NotEqual(initialDashboard, updatedDashboard);
            Assert.NotEqual(initialDashboard.Length, updatedDashboard.Length);
        }

        [Fact]
        public void GetDashboardCount_Should_ReturnZero_When_Initialized()
        {
            // Arrange
            // Act
            var count = _dashboardProvider.GetDashboardCount();

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void GetDashboardCount_Should_ReturnCorrectCount_After_AddingItems()
        {
            // Arrange
            var item1 = new DashboardItemViewModel(new DashboardItem());
            var item2 = new DashboardItemViewModel(new DashboardItem());

            _dashboardItemCollection.Add(new ObservableCollection<DashboardItemViewModel> { item1 });
            _dashboardItemCollection.Add(new ObservableCollection<DashboardItemViewModel> { item2 });

            // Act
            var count = _dashboardProvider.GetDashboardCount();

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void GetDashboardCount_Should_ReturnCorrectCount_After_RemovingItems()
        {
            // Arrange
            var item1 = new DashboardItemViewModel(new DashboardItem());
            var item2 = new DashboardItemViewModel(new DashboardItem());
            _dashboardItemCollection.Add(new ObservableCollection<DashboardItemViewModel> { item1 });
            _dashboardItemCollection.Add(new ObservableCollection<DashboardItemViewModel> { item2 });

            // Act
            _dashboardItemCollection.RemoveAt(0);
            var count = _dashboardProvider.GetDashboardCount();

            // Assert
            Assert.Equal(1, count);
        }
    }
}
