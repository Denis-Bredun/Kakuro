using System.Text.Json.Serialization;

namespace Kakuro.Models
{
    public class RatingRecord : IComparable<RatingRecord>
    {
        public TimeOnly GameCompletionTime { get; set; }
        public DateOnly GameCompletionDate { get; set; }

        public string FormattedCompletionTime => GameCompletionTime.ToString("HH\\:mm\\:ss");

        public RatingRecord(int hour, int minute, int second)
        {
            GameCompletionTime = new TimeOnly(hour, minute, second);
            GameCompletionDate = DateOnly.FromDateTime(DateTime.Now);
        }

        [JsonConstructor]
        public RatingRecord(TimeOnly gameCompletionTime, DateOnly gameCompletionDate)
        {
            GameCompletionTime = gameCompletionTime;
            GameCompletionDate = gameCompletionDate;
        }

        public int CompareTo(RatingRecord? other)
        {
            if (other == null)
                throw new ArgumentNullException("", "While sorting rating records, one was passed as null.");

            return GameCompletionTime.CompareTo(other.GameCompletionTime);
        }
    }
}
