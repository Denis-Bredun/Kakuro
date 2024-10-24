﻿using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.ViewModels;

namespace Kakuro.Commands.DashboardViewModel
{
    // #BAD: tests shall be written
    public class CleanDashboardCommand : RelayCommand
    {
        private DashboardItemCollection _dashboard;

        public CleanDashboardCommand(DashboardItemCollection dashboard)
        {
            _dashboard ??= dashboard;
        }

        public override void Execute(object? parameter)
        {
            int dashboardSize = _dashboard.Count;
            DashboardItemViewModel currentElement;

            for (int i = 1; i < dashboardSize - 1; i++)
                for (int j = 1; j < dashboardSize - 1; j++)
                {
                    currentElement = _dashboard[i][j];
                    if (currentElement.CellType == CellType.ValueCell)
                        currentElement.DisplayValue = "";
                }
        }
    }
}
