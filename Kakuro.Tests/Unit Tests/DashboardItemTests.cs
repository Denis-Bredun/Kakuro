using Kakuro.Enums;
using Kakuro.Models;

namespace Kakuro.Tests.Unit_Tests
{
    public class DashboardItemTests
    {
        [Fact]
        public void Constructor_ShouldInitializeNotesArrayAndCellType_When_Called()
        {
            // Arrange & Act
            var dashboardItem = new DashboardItem();

            // Assert
            Assert.NotNull(dashboardItem.Notes);
            Assert.Equal(9, dashboardItem.Notes.Length);
            Assert.Equal(CellType.EmptyCell, dashboardItem.CellType);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_When_ObjectsAreEqual()
        {
            // Arrange
            var item1 = new DashboardItem
            {
                DisplayValue = 5,
                HiddenValue = 10,
                CellType = CellType.ValueCell,
                Notes = new int[] { 1, 2, 3 },
                SumRight = 15,
                SumBottom = 20
            };
            var item2 = new DashboardItem
            {
                DisplayValue = 5,
                HiddenValue = 10,
                CellType = CellType.ValueCell,
                Notes = new int[] { 1, 2, 3 },
                SumRight = 15,
                SumBottom = 20
            };

            // Act
            var areEqual = item1.Equals(item2);

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_When_ObjectsAreNotEqual()
        {
            // Arrange
            var item1 = new DashboardItem { DisplayValue = 5 };
            var item2 = new DashboardItem { DisplayValue = 10 };

            // Act
            var areEqual = item1.Equals(item2);

            // Assert
            Assert.False(areEqual);
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameValue_ForEqualItems()
        {
            // Arrange
            var item1 = new DashboardItem
            {
                DisplayValue = 5,
                HiddenValue = 10,
                CellType = CellType.ValueCell,
                Notes = new int[] { 1, 2, 3 },
                SumRight = 15,
                SumBottom = 20
            };
            var item2 = new DashboardItem
            {
                DisplayValue = 5,
                HiddenValue = 10,
                CellType = CellType.ValueCell,
                Notes = new int[] { 1, 2, 3 },
                SumRight = 15,
                SumBottom = 20
            };

            // Act
            var hash1 = item1.GetHashCode();
            var hash2 = item2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentValues_ForDifferentItems()
        {
            // Arrange
            var item1 = new DashboardItem { DisplayValue = 5 };
            var item2 = new DashboardItem { DisplayValue = 10 };

            // Act
            var hash1 = item1.GetHashCode();
            var hash2 = item2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
    }
}
