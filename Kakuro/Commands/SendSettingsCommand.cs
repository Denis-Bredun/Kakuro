using Kakuro.Base_Classes;
using Kakuro.Events;
using Kakuro.Models;
using System.Collections.ObjectModel;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class SendSettingsCommand : RelayCommand
    {
        private IEventAggregator _eventAggregator;
        private ObservableCollection<Setting> _settings;

        public SendSettingsCommand(IEventAggregator eventAggregator, ObservableCollection<Setting> settings)
        {
            _eventAggregator = eventAggregator;
            _settings = settings;
        }

        public override void Execute(object? parameter)
        {
            _eventAggregator.GetEvent<SettingsChangedEvent>().Publish(_settings);
        }
    }
}
