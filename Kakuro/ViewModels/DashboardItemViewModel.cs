using Kakuro.Models;

namespace Kakuro.ViewModels
{
    public class DashboardItemViewModel : ViewModelBase
    {
        private DashboardItem _dashboardItem;

        // #PRIORITY: make validations for Value and Notes, so user couldn't enter smth except for 1-9 numbers

        public string Value
        {
            get => ConvertIntToString(_dashboardItem.Value);
            set
            {
                _dashboardItem.Value = ConvertStringToInt(value);
                OnPropertyChanged("Value");
            }
        }

        public string[] Notes
        {
            get => _dashboardItem.Notes.Select(el => ConvertIntToString(el)).ToArray();
            set
            {
                for (int i = 0; i < _dashboardItem.Notes.Length; i++)
                {
                    _dashboardItem.Notes[i] = ConvertStringToInt(value[i]);
                }
            }
        }

        public DashboardItemViewModel(DashboardItem dashboardItem)
        {
            _dashboardItem = dashboardItem;
        }

        private int ConvertStringToInt(string value)
        {
            return string.IsNullOrEmpty(value) ? 0 : Convert.ToInt32(value);
        }

        private string ConvertIntToString(int? value)
        {
            return value == 0 ? "" : $"{value}";
        }
    }
}
