﻿using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.ViewModels;
using Kakuro.Models;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
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

        public string[,] Notes
        {
            get
            {
                var notesArray = new string[3, 3];

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        notesArray[i, j] = ConvertIntToString(_dashboardItem.Notes[i, j]);

                return notesArray;
            }
            set
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        _dashboardItem.Notes[i, j] = ConvertStringToInt(value[i, j]);

                OnPropertyChanged("Notes");
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
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
