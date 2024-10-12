using Kakuro.Base_Classes;
using Kakuro.Models;
using Kakuro.ViewModels;
using System.Windows.Input;

namespace Kakuro.Commands
{
    public class ShowCorrectAnswersCommand : RelayCommand
    {
        private DashboardViewModel _dashboardViewModel;
        private ICommand _stopStopwatchCommand;
        private ICommand _addMinuteAndContinueStopwatchCommand;

        public ShowCorrectAnswersCommand(DashboardViewModel dashboardViewModel, ICommand stopStopwatchCommand, ICommand addMinuteAndContinueStopwatchCommand)
        {
            _dashboardViewModel = dashboardViewModel;
            _stopStopwatchCommand = stopStopwatchCommand;
            _addMinuteAndContinueStopwatchCommand = addMinuteAndContinueStopwatchCommand;
        }

        public override void Execute(object? parameter)
        {
            var showCorrectAnswersSetting = (Setting)parameter;

            _dashboardViewModel.ShowCorrectAnswers = showCorrectAnswersSetting.IsEnabled;

            LockStopwatch(_dashboardViewModel.ShowCorrectAnswers);
        }

        private void LockStopwatch(bool showCorrectAnswers)
        {
            if (showCorrectAnswers)
                _stopStopwatchCommand.Execute(null);
            else
                _addMinuteAndContinueStopwatchCommand.Execute(null);
        }
    }
}
