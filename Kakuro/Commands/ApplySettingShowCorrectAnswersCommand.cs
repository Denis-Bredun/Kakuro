using Kakuro.Base_Classes;
using Kakuro.Models;
using Kakuro.ViewModels;
using System.Windows.Input;

namespace Kakuro.Commands
{
    public class ApplySettingShowCorrectAnswersCommand : RelayCommand
    {
        private DashboardViewModel _dashboardViewModel;
        private ICommand _stopStopwatchCommand;
        private ICommand _addMinuteAndContinueStopwatchCommand;
        private ICommand _cleanDashboardCommand;
        private ICommand _showCorrectAnswersCommand;

        public ApplySettingShowCorrectAnswersCommand(
            DashboardViewModel dashboardViewModel,
            ICommand stopStopwatchCommand,
            ICommand addMinuteAndContinueStopwatchCommand,
            ICommand cleanDashboardCommand)
        {
            _dashboardViewModel = dashboardViewModel;
            _stopStopwatchCommand = stopStopwatchCommand;
            _addMinuteAndContinueStopwatchCommand = addMinuteAndContinueStopwatchCommand;
            _cleanDashboardCommand = cleanDashboardCommand;
            _showCorrectAnswersCommand = new ShowCorrectAnswersCommand(dashboardViewModel.Dashboard);
        }

        public override void Execute(object? parameter)
        {
            var showCorrectAnswersSetting = (Setting)parameter;

            _dashboardViewModel.ShowCorrectAnswers = showCorrectAnswersSetting.IsEnabled;

            LockStopwatch(_dashboardViewModel.ShowCorrectAnswers);

            ShowOrEraseCorrectAnswers(_dashboardViewModel.ShowCorrectAnswers);
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
