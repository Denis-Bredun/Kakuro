using Kakuro.Base_Classes;
using Kakuro.Interfaces.ViewModels;

namespace Kakuro.Commands
{
    public class CellGotFocusCommand : RelayCommand
    {
        private readonly Action<IDashboardItemViewModel> _execute;

        public CellGotFocusCommand(Action<IDashboardItemViewModel> execute)
        {
            _execute = execute;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is IDashboardItemViewModel selectedCell)
            {
                _execute(selectedCell);
            }
        }
    }
}
