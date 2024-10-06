using Kakuro.Enums;

namespace Kakuro.Interfaces.ViewModels
{
    public interface IDashboardItemViewModel
    {
        string DisplayValue { get; set; }
        string HiddenValue { get; set; }
        string SumRight { get; set; }
        string SumBottom { get; set; }
        string[,] Notes { get; set; }
        CellType CellType { get; set; }
        bool IsSelected { get; set; }
    }
}
