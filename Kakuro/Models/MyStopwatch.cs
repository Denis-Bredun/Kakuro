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

        public TimeSpan TotalElapsed
        {
            get
            {
                return Elapsed + StartOffset;
            }
        }

        public int ElapsedHours
        {
            get
            {
                return TotalElapsed.Hours;
            }
        }

        public int ElapsedMinutes
        {
            get
            {
                return TotalElapsed.Minutes;
            }
        }

        public int ElapsedSeconds
        {
            get
            {
                return TotalElapsed.Seconds;
            }
        }

        public void AddTime(TimeSpan timeToAdd)
        {
            StartOffset += timeToAdd;
        }
    }
}
