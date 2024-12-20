﻿using Kakuro.Base_Classes;
using Kakuro.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kakuro.Commands.DashboardViewModel
{
    public class GetChangedSettingsCommand : RelayCommand
    {
        private ICommand _showCorrectAnswersCommand;
        private ICommand _autoSubmitCommand;
        private ICommand _hideTimerCommand;

        public GetChangedSettingsCommand(
            ViewModels.DashboardViewModel dashboardViewModel,
            ICommand stopStopwatchCommand,
            ICommand addMinuteAndContinueStopwatchCommand,
            ICommand cleanDashboardCommand,
            IEventAggregator eventAggregator)
        {
            _showCorrectAnswersCommand = new Commands.GetChangedSettingsCommand.ApplySettingShowCorrectAnswersCommand(
                dashboardViewModel,
                stopStopwatchCommand,
                addMinuteAndContinueStopwatchCommand,
                cleanDashboardCommand,
                eventAggregator);

            _autoSubmitCommand = new Commands.GetChangedSettingsCommand.ApplySettingAutoSubmitCommand(dashboardViewModel);

            _hideTimerCommand = new Commands.GetChangedSettingsCommand.ApplySettingHideTimerCommand(dashboardViewModel);
        }

        public override void Execute(object? parameter)
        {
            var settings = (ObservableCollection<ViewModels.SettingViewModel>)parameter;

            _showCorrectAnswersCommand.Execute(settings.FirstOrDefault(el => el.SettingType == SettingType.ShowCorrectAnswers));

            _autoSubmitCommand.Execute(settings.FirstOrDefault(el => el.SettingType == SettingType.AutoSubmit));

            _hideTimerCommand.Execute(settings.FirstOrDefault(el => el.SettingType == SettingType.HideTimer));
        }
    }
}
