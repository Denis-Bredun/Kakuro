﻿using Kakuro.Models;
using System.Collections.ObjectModel;

namespace Kakuro.ViewModels
{
    public class SettingsViewModel
    {
        public ObservableCollection<Setting> Settings { get; set; }

        public SettingsViewModel()
        {
            Settings = new ObservableCollection<Setting>
            {
                new Setting { Name = "Show correct values", IsEnabled = false }
            };
        }
    }
}
