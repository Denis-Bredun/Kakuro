using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Models;

namespace Kakuro.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly Setting _setting;

        public SettingViewModel(Setting setting)
        {
            _setting = setting;
        }

        public SettingType SettingType
        {
            get => _setting.SettingType;
            set
            {
                _setting.SettingType = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => _setting.IsEnabled;
            set
            {
                _setting.IsEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}
