using Kakuro.Commands;
using Kakuro.Enums;
using Kakuro.Events;
using Kakuro.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    public class SettingsViewModel : IDisposable
    {
        private IEventAggregator _eventAggregator;
        private bool _disposed = false;

        public ObservableCollection<SettingViewModel> Settings { get; set; }
        public ICommand SendSettingsCommand { get; }
        public ICommand TurnOffAutoSubmitCommand { get; }

        private SubscriptionToken _correctAnswersTurnedOnSubscriptionToken;

        public SettingsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Settings = new ObservableCollection<SettingViewModel>
            {
                new SettingViewModel(new Setting(SettingType.ShowCorrectAnswers, false)),
                new SettingViewModel(new Setting(SettingType.AutoSubmit, true)),
                new SettingViewModel(new Setting(SettingType.HideTimer, false))
            };

            SendSettingsCommand = new SendSettingsCommand(_eventAggregator, Settings);
            TurnOffAutoSubmitCommand = new TurnOffAutoSubmitCommand(Settings);

            _correctAnswersTurnedOnSubscriptionToken = eventAggregator.GetEvent<CorrectAnswersTurnedOnEvent>().Subscribe(TurnOffAutoSubmitCommand.Execute);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_correctAnswersTurnedOnSubscriptionToken != null)
                    {
                        _correctAnswersTurnedOnSubscriptionToken.Dispose();
                        _correctAnswersTurnedOnSubscriptionToken = null;
                    }
                }
                _disposed = true;
            }
        }

        ~SettingsViewModel()
        {
            Dispose(false);
        }
    }
}
