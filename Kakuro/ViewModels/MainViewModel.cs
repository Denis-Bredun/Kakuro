﻿using Kakuro.Base_Classes;
using Kakuro.Interfaces.ViewModels;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    public class MainViewModel : ViewModelBase
    {
        public IDashboardViewModel DashboardViewModel { get; }
        public MainViewModel(IDashboardViewModel dashboardViewModel)
        {
            DashboardViewModel = dashboardViewModel;
        }
    }
}
