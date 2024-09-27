namespace Kakuro.Models
{
    public class DashboardItem
    {
        private const int MAX_COUNT_OF_NOTES = 9;
        public int? Value { get; set; }
        public int[]? Notes { get; set; }
        public int? SumRight { get; set; }
        public int? SumBottom { get; set; }

        public DashboardItem()
        {
            Notes = new int[MAX_COUNT_OF_NOTES];
        }

        public override bool Equals(object? obj)
        {
            if (obj is DashboardItem other)
            {
                return Value == other.Value &&
                       (Notes == null && other.Notes == null ||
                       Notes != null && other.Notes != null && Notes.SequenceEqual(other.Notes)) &&
                       SumRight == other.SumRight &&
                       SumBottom == other.SumBottom;
            }

            return false;
        }

        public override int GetHashCode()
        {
            // Hash code for the array cannot be generated using a simple XOR, so I use a combination of values
            var notesHash = Notes?.Aggregate(0, (current, note) => current ^ note.GetHashCode()) ?? 0;
            return (Value?.GetHashCode() ?? 0) ^ notesHash ^
                   (SumRight?.GetHashCode() ?? 0) ^ (SumBottom?.GetHashCode() ?? 0);
        }
    }
}
