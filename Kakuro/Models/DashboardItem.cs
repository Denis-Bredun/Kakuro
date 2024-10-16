using Kakuro.Enums;
using System.Text.Json.Serialization;

namespace Kakuro.Models
{
    public class DashboardItem
    {
        private static int _countOfIds = 1;

        public int? ID { get; set; }
        public int? DisplayValue { get; set; }
        public int? HiddenValue { get; set; } // we need this so we could show right answers using settings
        public CellType CellType { get; set; }
        public int? SumRight { get; set; }
        public int? SumBottom { get; set; }

        public DashboardItem()
        {
            ID = _countOfIds;
            _countOfIds++;
        }

        [JsonConstructor]
        public DashboardItem(int? id, int? displayValue, int? hiddenValue, CellType cellType, int? sumRight, int? sumBottom)
        {
            ID = id;
            DisplayValue = displayValue;
            HiddenValue = hiddenValue;
            CellType = cellType;
            SumRight = sumRight;
            SumBottom = sumBottom;
        }

        // #BAD: i shall write tests for this model
        public override bool Equals(object? obj)
        {
            if (obj is DashboardItem other)
            {
                return ID == other.ID &&
                       DisplayValue == other.DisplayValue &&
                       HiddenValue == other.HiddenValue &&
                       CellType == other.CellType &&
                       SumRight == other.SumRight &&
                       SumBottom == other.SumBottom;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (ID?.GetHashCode() ?? 0) ^
                   (DisplayValue?.GetHashCode() ?? 0) ^
                   (HiddenValue?.GetHashCode() ?? 0) ^
                   (SumRight?.GetHashCode() ?? 0) ^
                   (SumBottom?.GetHashCode() ?? 0) ^
                   CellType.GetHashCode();
        }
    }
}
