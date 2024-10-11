using Kakuro.Enums;

namespace Kakuro.Models
{
    public class DashboardItem
    {
        public int? DisplayValue { get; set; }
        public int? HiddenValue { get; set; } // we need this so we could show right answers using settings
        public CellType CellType { get; set; }
        public int? SumRight { get; set; }
        public int? SumBottom { get; set; }

        public DashboardItem()
        {
        }

        // #BAD: i shall write tests for this model
        public override bool Equals(object? obj)
        {
            if (obj is DashboardItem other)
            {
                return DisplayValue == other.DisplayValue &&
                       HiddenValue == other.HiddenValue &&
                       CellType == other.CellType &&
                       SumRight == other.SumRight &&
                       SumBottom == other.SumBottom;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (DisplayValue?.GetHashCode() ?? 0) ^
                   (HiddenValue?.GetHashCode() ?? 0) ^
                   (SumRight?.GetHashCode() ?? 0) ^
                   (SumBottom?.GetHashCode() ?? 0) ^
                   CellType.GetHashCode();
        }
    }
}
