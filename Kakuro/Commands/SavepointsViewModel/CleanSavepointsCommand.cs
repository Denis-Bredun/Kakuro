using Kakuro.Base_Classes;

namespace Kakuro.Commands.SavepointsViewModel
{
    public class CleanSavepointsCommand : RelayCommand
    {
        private ViewModels.SavepointsViewModel _savepointsViewModel;

        public CleanSavepointsCommand(ViewModels.SavepointsViewModel savepointsViewModel)
        {
            _savepointsViewModel = savepointsViewModel;
        }

        public override void Execute(object? parameter)
        {
            _savepointsViewModel.Savepoints.Clear();
        }
    }
}
