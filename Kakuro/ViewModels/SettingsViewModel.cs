using Kakuro.Commands;
using Kakuro.Enums;
using Kakuro.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    public class SettingsViewModel
    {
        private IEventAggregator _eventAggregator;

        public ObservableCollection<Setting> Settings { get; set; }
        public ICommand SendSettingsCommand { get; }

        public SettingsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Settings = new ObservableCollection<Setting>
            {
                new Setting { SettingType = SettingType.ShowCorrectAnswers, IsEnabled = false }
            };
            SendSettingsCommand = new SendSettingsCommand(_eventAggregator, Settings);
        }
    }
}
