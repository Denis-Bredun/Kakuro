using Kakuro.Base_Classes;

namespace Kakuro.Commands
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
            foreach (var row in _dashboard)
                foreach (var item in row)
                    if (item.CellType == Enums.CellType.ValueCell)
                        item.HiddenValue = "";
        }
    }
}
