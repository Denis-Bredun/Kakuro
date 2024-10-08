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

        public void Deconstruct(out DifficultyLevels difficulty, out string hours, out string minutes, out string seconds)
        {
            difficulty = Difficulty;
            hours = StopWatchHours;
            minutes = StopWatchMinutes;
            seconds = StopWatchSeconds;
        }
    }
}
