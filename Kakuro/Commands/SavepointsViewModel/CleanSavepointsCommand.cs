using Kakuro.Base_Classes;
using Kakuro.Interfaces.Data_Access.Data_Providers;

namespace Kakuro.Commands.SavepointsViewModel
{
    public class CleanSavepointsCommand : RelayCommand
    {
        private ViewModels.SavepointsViewModel _savepointsViewModel;
        private ISavepointProvider _savepointProvider;

        public CleanSavepointsCommand(ViewModels.SavepointsViewModel savepointsViewModel, ISavepointProvider savepointProvider)
        {
            _savepointsViewModel = savepointsViewModel;
            _savepointProvider = savepointProvider;
        }

        public override void Execute(object? parameter)
        {
            for (int i = 0; i < _savepointsViewModel.Savepoints.Count; i++)
                _savepointProvider.Delete(_savepointsViewModel.Savepoints[i].Id);

            //_savepointProvider.CleanCache();

            _savepointsViewModel.Savepoints.Clear();
        }
    }
}
