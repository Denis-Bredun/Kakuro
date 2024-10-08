﻿using Kakuro.Data_Access.Data_Providers;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;

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
                var hasNonZeroValue = _dashboardItemCollection.Any(row => row.Any(item => item.HiddenValue != ""));
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
                        calculatedSum += Convert.ToInt32(currentElement.HiddenValue);
                        wasSumCollected = true;
                    }
                    else if (wasSumCollected)
                    {
                        var sumToCheck = isVerticalSum ? Convert.ToInt32(currentElement.SumBottom) : Convert.ToInt32(currentElement.SumRight);

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

                        if (currentElement.HiddenValue != "")
                        {
                            if (currentElement.CellType != CellType.ValueCell)
                            {
                                areCellTypesCorrect = false;
                            }
                        }
                        else if (currentElement.SumBottom != "" || currentElement.SumRight != "")
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
                // Act
                _dashboardProvider.GenerateDashboard(difficulty);

                // Assert
                for (int i = 0; i < _dashboardItemCollection.Count; i++)
                {
                    for (int j = 0; j < _dashboardItemCollection[i].Count; j++)
                    {
                        var currentElement = _dashboardItemCollection[i][j];

                        if (currentElement.HiddenValue == "" && currentElement.SumBottom == ""
                            && currentElement.SumRight == "" && currentElement.CellType != CellType.EmptyCell)
                            areCellTypesCorrect = false;
                        else if ((currentElement.SumBottom != "" || currentElement.SumRight != "") && currentElement.CellType != CellType.SumCell)
                            areCellTypesCorrect = false;
                        else if (currentElement.HiddenValue != "" && currentElement.CellType != CellType.ValueCell)
                            areCellTypesCorrect = false;
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
                            int currentValue = string.IsNullOrEmpty(currentElement.HiddenValue) ? 0 : Convert.ToInt32(currentElement.HiddenValue);

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
            int valueAbove = string.IsNullOrEmpty(_dashboardItemCollection[i - 1][j].HiddenValue) ? 0 : Convert.ToInt32(_dashboardItemCollection[i - 1][j].HiddenValue);

            return valueAbove != value;
        }

        private bool IsUniqueBelow(int i, int j, int value)
        {
            int valueBelow = string.IsNullOrEmpty(_dashboardItemCollection[i + 1][j].HiddenValue) ? 0 : Convert.ToInt32(_dashboardItemCollection[i + 1][j].HiddenValue);

            return valueBelow != value;
        }

        private bool IsUniqueLeft(int i, int j, int value)
        {
            int valueLeft = string.IsNullOrEmpty(_dashboardItemCollection[i][j - 1].HiddenValue) ? 0 : Convert.ToInt32(_dashboardItemCollection[i][j - 1].HiddenValue);

            return valueLeft != value;
        }

        private bool IsUniqueRight(int i, int j, int value)
        {
            int valueRight = string.IsNullOrEmpty(_dashboardItemCollection[i][j + 1].HiddenValue) ? 0 : Convert.ToInt32(_dashboardItemCollection[i][j + 1].HiddenValue);

            return valueRight != value;
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
                            Assert.True(currentElement.SumBottom != "" || currentElement.SumRight != "");
                        }
                    }
                }
            }
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
                            Assert.Equal("", currentElement.HiddenValue);
                            Assert.Equal("", currentElement.SumBottom);
                            Assert.Equal("", currentElement.SumRight);
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
                        if (item.HiddenValue != "")
                        {
                            Assert.InRange(Convert.ToInt32(item.HiddenValue), 1, 9);
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
                            Assert.Equal("", item.HiddenValue);
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
    }
}
