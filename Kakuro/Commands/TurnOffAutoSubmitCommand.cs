using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.ViewModels;
using System.Collections.ObjectModel;

namespace Kakuro.Commands
{
    public class TurnOffAutoSubmitCommand : RelayCommand
    {
        private ObservableCollection<SettingViewModel> _settings;

        public TurnOffAutoSubmitCommand(ObservableCollection<SettingViewModel> settings)
        {
            _settings = settings;
        }

        public override void Execute(object? parameter)
        {
            var autoSubmitEnabled = (bool)parameter;
            var autoSubmitSetting = _settings.FirstOrDefault(s => s.SettingType == SettingType.AutoSubmit);
            autoSubmitSetting.IsEnabled = autoSubmitEnabled;
        }
    }
}
