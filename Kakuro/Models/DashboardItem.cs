namespace Kakuro.Models
{
    public class DashboardItem
    {
        private const int MAX_COUNT_OF_NOTES = 9;
        public int? Value { get; set; }
        public int[]? Notes { get; set; }

        public DashboardItem()
        {
            Notes = new int[MAX_COUNT_OF_NOTES];
        }
    }
}
