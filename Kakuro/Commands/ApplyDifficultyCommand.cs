using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;

namespace Kakuro.Commands
{
    public class ApplyDifficultyCommand : RelayCommand
    {
        private IDashboardProvider _dashboardProvider;
        private readonly Dictionary<DifficultyLevels, int> _dashboardCountsForDifficulties;
        private const int TOTAL_SIZE_OF_BORDER_FROM_TWO_SIDES = 2;

        public ApplyDifficultyCommand(IDashboardProvider dashboardProvider)
        {
            _dashboardProvider ??= dashboardProvider;

            _dashboardCountsForDifficulties = new Dictionary<DifficultyLevels, int>
            {
                { DifficultyLevels.Easy, 6 },
                { DifficultyLevels.Normal, 10 },
                { DifficultyLevels.Hard, 16 }
            };
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                throw new NullReferenceException("Parameter for ApplyDifficultyCommand is null!");

            if (!Enum.TryParse<DifficultyLevels>(parameter.ToString(), out var difficultyLevel))
                throw new ArgumentException("Parameter for ApplyDifficultyCommand is of incorrect type!");

            int dashboardSize = _dashboardCountsForDifficulties[difficultyLevel] + TOTAL_SIZE_OF_BORDER_FROM_TWO_SIDES;

            if (dashboardSize != _dashboardProvider.GetDashboardCount())
                _dashboardProvider.GenerateDashboard(difficultyLevel);
        }
    }
}
