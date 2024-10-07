﻿using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Models;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    // #BAD: shall there be interfaces?
    public class DashboardItemViewModel : ViewModelBase
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
            catch (Exception) // validation
            {
                int? previousValue = _dashboardItem.DisplayValue;       // so user can't enter letters and symbols like '(', '*', e.t.c. Only numbers from 1 to 9
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
