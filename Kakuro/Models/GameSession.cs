using Kakuro.Enums;

namespace Kakuro.Models
{
    public class GameSession
    {
        public DifficultyLevels Difficulty { get; }
        public string StopWatchHours { get; }
        public string StopWatchMinutes { get; }
        public string StopWatchSeconds { get; }

        public GameSession(DifficultyLevels difficulty, string hours, string minutes, string seconds)
        {
            Difficulty = difficulty;
            StopWatchHours = hours;
            StopWatchMinutes = minutes;
            StopWatchSeconds = seconds;
        }
    }
}
