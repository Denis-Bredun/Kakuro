using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kakuro.Commands
{
    public class GetChangedSettingsCommands : RelayCommand
    {
        private ICommand _showCorrectAnswersCommand;
        private ICommand _autoSubmitCommand;
        private ICommand _hideTimerCommand;

        public GetChangedSettingsCommands(
            DashboardViewModel dashboardViewModel,
            ICommand stopStopwatchCommand,
            ICommand addMinuteAndContinueStopwatchCommand,
            ICommand cleanDashboardCommand,
            IEventAggregator eventAggregator)
        {
            _showCorrectAnswersCommand = new ApplySettingShowCorrectAnswersCommand(
                dashboardViewModel,
                stopStopwatchCommand,
                addMinuteAndContinueStopwatchCommand,
                cleanDashboardCommand,
                eventAggregator);

            _autoSubmitCommand = new ApplySettingAutoSubmitCommand(dashboardViewModel);

            _hideTimerCommand = new ApplySettingHideTimerCommand(dashboardViewModel);
        }

        public override void Execute(object? parameter)
        {
            var settings = (ObservableCollection<SettingViewModel>)parameter;

            _showCorrectAnswersCommand.Execute(settings.FirstOrDefault(el => el.SettingType == SettingType.ShowCorrectAnswers));

            _autoSubmitCommand.Execute(settings.FirstOrDefault(el => el.SettingType == SettingType.AutoSubmit));

            _hideTimerCommand.Execute(settings.FirstOrDefault(el => el.SettingType == SettingType.HideTimer));
        }
    }
}
