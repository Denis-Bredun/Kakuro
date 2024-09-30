using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.ViewModels;
using Kakuro.Models;

namespace Kakuro.ViewModels
{
    public class DashboardItemViewModel : ViewModelBase, IDashboardItemViewModel
    {
        private DashboardItem _dashboardItem;

        public string DisplayValue
        {
            get => ConvertIntToString(_dashboardItem.DisplayValue);
            set
            {
                _dashboardItem.DisplayValue = ConvertStringToInt(value);
                OnPropertyChanged("DisplayValue");
            }
        }

        public string HiddenValue
        {
            get => ConvertIntToString(_dashboardItem.HiddenValue);
            set
            {
                _dashboardItem.HiddenValue = ConvertStringToInt(value);
                OnPropertyChanged("HiddenValue");
            }
        }

        public string SumRight
        {
            get => ConvertIntToString(_dashboardItem.SumRight);
            set
            {
                _dashboardItem.SumRight = ConvertStringToInt(value);
                OnPropertyChanged("SumRight");
            }
        }

        public string SumBottom
        {
            get => ConvertIntToString(_dashboardItem.SumBottom);
            set
            {
                _dashboardItem.SumBottom = ConvertStringToInt(value);
                OnPropertyChanged("SumBottom");
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

        public CellType CellType
        {
            get => _dashboardItem.CellType;
            set
            {
                _dashboardItem.CellType = value;
                OnPropertyChanged("CellType");
            }
        }

        public DashboardItemViewModel(DashboardItem dashboardItem)
        {
            _dashboardItem = dashboardItem;
        }

        private int ConvertStringToInt(string value)
        {
            int enteredValue;
            try
            {
                enteredValue = string.IsNullOrEmpty(value) ? 0 : Convert.ToInt32(value);
            }
            catch (Exception)
            {
                int? previousValue = _dashboardItem.DisplayValue;
                return previousValue.HasValue ? previousValue.Value : 0;
            }

            return enteredValue;
        }

        private string ConvertIntToString(int? value)
        {
            return value == 0 ? "" : $"{value}";
        }
    }
}
