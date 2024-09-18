namespace Kakuro.Models
{
    public class RatingRecord : IComparable<RatingRecord>
    {
        public TimeOnly GameCompletionTime { get; set; }
        public DateOnly GameCompletionDate { get; set; }

        public int CompareTo(RatingRecord? other)
        {
            if (other == null)
                throw new ArgumentNullException("", "While sorting rating records, one was passed as null.");

            return GameCompletionTime.CompareTo(other.GameCompletionTime);
        }
    }
}
