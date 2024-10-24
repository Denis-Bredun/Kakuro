using System.Diagnostics;

namespace Kakuro.Models
{
    public class MyStopwatch : Stopwatch
    {
        public TimeSpan StartOffset { get; private set; }

        public MyStopwatch(TimeSpan startOffset)
        {
            StartOffset = startOffset;
        }

        public TimeSpan TotalElapsed => Elapsed + StartOffset;

        public int ElapsedHours => TotalElapsed.Hours;

        public int ElapsedMinutes => TotalElapsed.Minutes;

        public int ElapsedSeconds => TotalElapsed.Seconds;

        public void AddTime(TimeSpan timeToAdd)
        {
            StartOffset += timeToAdd;
        }

        public void CleanAdditionalTime()
        {
            StartOffset = new TimeSpan();
        }
    }
}
