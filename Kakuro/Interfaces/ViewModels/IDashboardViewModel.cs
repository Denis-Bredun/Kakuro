﻿using Kakuro.Enums;
using System.Windows.Input;

namespace Kakuro.Interfaces.ViewModels
{
    public interface IDashboardViewModel
    {
        DashboardItemCollection Dashboard { get; }
        IDashboardItemViewModel SelectedCell { get; set; }
        bool IsMakingNotes { get; set; }
        DifficultyLevels ChoosenDifficulty { get; set; }
        ICommand ApplyDifficultyCommand { get; }
        ICommand NewGameCommand { get; }
        ICommand VerifySolutionCommand { get; }
        ICommand CleanDashboardCommand { get; }
        ICommand EraseSelectedCellCommand { get; }
        ICommand CellGotFocusCommand { get; }
    }
}
