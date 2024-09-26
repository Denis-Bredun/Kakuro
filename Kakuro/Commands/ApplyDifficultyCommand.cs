﻿using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Models;
using System.Collections.ObjectModel;

namespace Kakuro.Commands
{
    public class ApplyDifficultyCommand : RelayCommand
    {
        private const int
            EASY_LEVEL_SIZE = 6,
            NORMAL_LEVEL_SIZE = 10,
            HARD_LEVEL_SIZE = 16,
            BORDER_ITEMS_SIZE = 1; // That is, we have a game dashboard, for example, 6x6, in which there are
                                   // cells for entering a value, and we also have an additional line of
                                   // elements on the right, top, left and bottom, on which we write the
                                   // sums of the numbers.

        private ObservableCollection<DashboardItem> _dashboard;

        public ApplyDifficultyCommand(ObservableCollection<DashboardItem> dashboard)
        {
            _dashboard ??= dashboard;
        }

        public override void Execute(object? parameter)
        {
            var difficultyLevel = (DifficultyLevels)parameter;

            _dashboard.Clear();


        }
    }
}
