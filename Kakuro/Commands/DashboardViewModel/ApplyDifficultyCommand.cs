using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using System.Windows.Input;

namespace Kakuro.Commands.DashboardViewModel
{
    // #BAD: tests shall be REwritten
    public class ApplyDifficultyCommand : RelayCommand
    {
        private IDashboardProvider _dashboardProvider;
        private ViewModels.DashboardViewModel _dashboardViewModel;
        private ICommand _restartStopwatchCommand;

        public ApplyDifficultyCommand(IDashboardProvider dashboardProvider, ViewModels.DashboardViewModel dashboardViewModel, ICommand restartStopwatchCommand)
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

            _restartStopwatchCommand.Execute(parameter);

            _dashboardProvider.GenerateDashboard(difficultyLevel);

            _dashboardViewModel.IsGameCompleted = false;
        }
    }
}
