using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.ViewModels;
using System.Windows.Input;

namespace Kakuro.Commands
{
    // #BAD: tests shall be REwritten
    public class ApplyDifficultyCommand : RelayCommand
    {
        private IDashboardProvider _dashboardProvider;
        private DashboardViewModel _dashboardViewModel;
        private ICommand _restartStopwatchCommand;

        public ApplyDifficultyCommand(IDashboardProvider dashboardProvider, DashboardViewModel dashboardViewModel, ICommand restartStopwatchCommand)
        {
            _dashboardProvider ??= dashboardProvider;
            _dashboardViewModel ??= dashboardViewModel;
            _restartStopwatchCommand ??= restartStopwatchCommand;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                throw new NullReferenceException("Parameter for ApplyDifficultyCommand is null!");

            if (!Enum.TryParse<DifficultyLevels>(parameter.ToString(), out var difficultyLevel))
                throw new ArgumentException("Parameter for ApplyDifficultyCommand is of incorrect type!");

            _dashboardViewModel.ChoosenDifficulty = difficultyLevel;

            _restartStopwatchCommand.Execute(null);

            _dashboardProvider.GenerateDashboard(difficultyLevel);
        }
    }
}
