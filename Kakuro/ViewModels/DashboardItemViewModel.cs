﻿using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Models;

namespace Kakuro.ViewModels
{
    public class DashboardItemViewModel : ViewModelBase
    {
        private DashboardItem _dashboardItem;

        // #PRIORITY: make validations for Value and Notes, so user couldn't enter smth except for 1-9 numbers

        public string DisplayValue
        {
            get => ConvertIntToString(_dashboardItem.DisplayValue);
            set
            {
                _dashboardItem.DisplayValue = ConvertStringToInt(value);
                OnPropertyChanged("DisplayValue");
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

        public int HiddenValue { get; set; }

        public CellType CellType { get; set; }

        public string SumRight { get; set; }

        public string SumBottom { get; set; }

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
