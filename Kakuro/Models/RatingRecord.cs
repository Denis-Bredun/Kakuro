namespace Kakuro.Models
{
    public class RatingRecord : IComparable<RatingRecord>
    {
        public TimeOnly GameCompletionTime { get; }
        public DateOnly GameCompletionDate { get; }

        public RatingRecord(int hour, int minute, int second)
        {
            GameCompletionTime = new TimeOnly(hour, minute, second);
            GameCompletionDate = DateOnly.FromDateTime(DateTime.Now);
        }

        public int CompareTo(RatingRecord? other)
        {
            if (other == null)
                throw new ArgumentNullException("", "While sorting rating records, one was passed as null.");

            return GameCompletionTime.CompareTo(other.GameCompletionTime);
        }
    }
}
