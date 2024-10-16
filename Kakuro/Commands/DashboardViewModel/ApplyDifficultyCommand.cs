﻿using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Events;
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
        private IEventAggregator _eventAggregator;

        public ApplyDifficultyCommand(
            IDashboardProvider dashboardProvider,
            ViewModels.DashboardViewModel dashboardViewModel,
            ICommand restartStopwatchCommand,
            IEventAggregator eventAggregator)
        {
            _dashboardProvider ??= dashboardProvider;
            _dashboardViewModel ??= dashboardViewModel;
            _restartStopwatchCommand ??= restartStopwatchCommand;
            _eventAggregator ??= eventAggregator;
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

            _eventAggregator.GetEvent<NewGameStartedEvent>().Publish(true);

            _dashboardViewModel.IsGameCompleted = false;
        }
    }
}
