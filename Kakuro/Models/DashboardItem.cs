using Kakuro.Enums;

namespace Kakuro.Models
{
    public class DashboardItem
    {
        public int? DisplayValue { get; set; }
        public int? HiddenValue { get; set; } // we need this so we could show right answers using settings
        public CellType CellType { get; set; }
        public int[,]? Notes { get; set; }
        public int? SumRight { get; set; }
        public int? SumBottom { get; set; }


        public DashboardItem()
        {
            Notes = new int[3, 3];
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
                       SumBottom == other.SumBottom &&
                       Notes.Cast<int>().SequenceEqual(other.Notes.Cast<int>());
            }

            return false;
        }

        public override int GetHashCode()
        {
            // Hash code for the array cannot be generated using a simple XOR, so I use a combination of values
            var notesHash = Notes.Cast<int>().Aggregate(0, (current, note) => current ^ note.GetHashCode());

            return (DisplayValue?.GetHashCode() ?? 0) ^
                   (HiddenValue?.GetHashCode() ?? 0) ^
                   notesHash ^
                   (SumRight?.GetHashCode() ?? 0) ^
                   (SumBottom?.GetHashCode() ?? 0) ^
                   CellType.GetHashCode();
        }
    }
}
