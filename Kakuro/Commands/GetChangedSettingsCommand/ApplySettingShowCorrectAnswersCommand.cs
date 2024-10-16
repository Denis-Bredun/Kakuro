using Kakuro.Base_Classes;
using Kakuro.Events;
using System.Windows.Input;

namespace Kakuro.Commands.GetChangedSettingsCommand
{
    public class ApplySettingShowCorrectAnswersCommand : RelayCommand
    {
        private ViewModels.DashboardViewModel _dashboardViewModel;
        private ICommand _stopStopwatchCommand;
        private ICommand _addMinuteAndContinueStopwatchCommand;
        private ICommand _cleanDashboardCommand;
        private ICommand _showCorrectAnswersCommand;
        private IEventAggregator _eventAggregator;

        public ApplySettingShowCorrectAnswersCommand(
            ViewModels.DashboardViewModel dashboardViewModel,
            ICommand stopStopwatchCommand,
            ICommand addMinuteAndContinueStopwatchCommand,
            ICommand cleanDashboardCommand,
            IEventAggregator eventAggregator)
        {
            _dashboardViewModel = dashboardViewModel;
            _stopStopwatchCommand = stopStopwatchCommand;
            _addMinuteAndContinueStopwatchCommand = addMinuteAndContinueStopwatchCommand;
            _cleanDashboardCommand = cleanDashboardCommand;
            _showCorrectAnswersCommand = new ShowCorrectAnswersCommand(dashboardViewModel.Dashboard);
            _eventAggregator = eventAggregator;
        }

        public override void Execute(object? parameter)
        {
            var showCorrectAnswersSetting = (ViewModels.SettingViewModel)parameter;

            bool wasChanged = false;

            if (_dashboardViewModel.ShowCorrectAnswers != showCorrectAnswersSetting.IsEnabled)
                wasChanged = true;

            _dashboardViewModel.ShowCorrectAnswers = showCorrectAnswersSetting.IsEnabled;

            if (wasChanged)
            {
                TurnOffAutoSubmit(_dashboardViewModel.ShowCorrectAnswers);

                LockStopwatch(_dashboardViewModel.ShowCorrectAnswers);

                ShowOrEraseCorrectAnswers(_dashboardViewModel.ShowCorrectAnswers);
            }
        }

        private void TurnOffAutoSubmit(bool showCorrectAnswers)
        {
            if (showCorrectAnswers)
                _dashboardViewModel.AutoSubmit = false;

            _eventAggregator.GetEvent<CorrectAnswersTurnedOnEvent>().Publish(_dashboardViewModel.AutoSubmit);
        }

        private void LockStopwatch(bool showCorrectAnswers)
        {
            if (showCorrectAnswers)
                _stopStopwatchCommand.Execute(null);
            else
                _addMinuteAndContinueStopwatchCommand.Execute(null);
        }

        private void ShowOrEraseCorrectAnswers(bool showCorrectAnswers)
        {
            if (showCorrectAnswers)
                _showCorrectAnswersCommand.Execute(null);
            else
                _cleanDashboardCommand.Execute(null);
        }
    }
}
