using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Models;
using Kakuro.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kakuro.Commands
{
    public class GetChangedSettingsCommands : RelayCommand
    {
        private ICommand _showCorrectAnswersCommand;

        public GetChangedSettingsCommands(
            DashboardViewModel dashboardViewModel,
            ICommand stopStopwatchCommand,
            ICommand addMinuteAndContinueStopwatchCommand,
            ICommand cleanDashboardCommand)
        {
            _showCorrectAnswersCommand = new ApplySettingShowCorrectAnswersCommand(
                dashboardViewModel,
                stopStopwatchCommand,
                addMinuteAndContinueStopwatchCommand,
                cleanDashboardCommand);
        }

        public override void Execute(object? parameter)
        {
            var settings = (ObservableCollection<Setting>)parameter;

            _showCorrectAnswersCommand.Execute(settings.FirstOrDefault(el => el.SettingType == SettingType.ShowCorrectValues));
        }
    }
}
