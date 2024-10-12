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
        private ICommand ShowCorrectAnswersCommand { get; }

        public GetChangedSettingsCommands(DashboardViewModel dashboardViewModel)
        {
            ShowCorrectAnswersCommand = new ShowCorrectAnswersCommand(dashboardViewModel);
        }

        public override void Execute(object? parameter)
        {
            var settings = (ObservableCollection<Setting>)parameter;

            ShowCorrectAnswersCommand.Execute(settings.FirstOrDefault(el => el.SettingType == SettingType.ShowCorrectValues));
        }
    }
}
