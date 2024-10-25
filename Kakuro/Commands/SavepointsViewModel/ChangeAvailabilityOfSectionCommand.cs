using Kakuro.Base_Classes;

namespace Kakuro.Commands.SavepointsViewModel
{
    public class ChangeAvailabilityOfSectionCommand : RelayCommand
    {
        private ViewModels.SavepointsViewModel _savepointsViewModel;

        public ChangeAvailabilityOfSectionCommand(ViewModels.SavepointsViewModel savepointsViewModel)
        {
            _savepointsViewModel = savepointsViewModel;
        }

        public override void Execute(object? parameter)
        {
            _savepointsViewModel.CorrectAnswersAreShown = !_savepointsViewModel.CorrectAnswersAreShown;
        }
    }
}
