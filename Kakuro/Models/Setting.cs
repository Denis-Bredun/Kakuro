using Kakuro.Enums;

namespace Kakuro.Models
{
    public class Setting
    {
        public SettingType SettingType { get; set; }
        public bool IsEnabled { get; set; }

        public Setting(SettingType settingType, bool isEnabled)
        {
            SettingType = settingType;
            IsEnabled = isEnabled;
        }
    }
}
