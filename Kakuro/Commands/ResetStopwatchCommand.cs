using Kakuro.Base_Classes;
using Kakuro.ViewModels;
using System.Diagnostics;

namespace Kakuro.Commands
{
    public class ResetStopwatchCommand : RelayCommand
    {
        private Stopwatch _stopwatch;
        private DashboardViewModel _viewModel;

        public ResetStopwatchCommand(Stopwatch stopwatch, DashboardViewModel viewModel)
        {
            _stopwatch = stopwatch;
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _stopwatch.Reset();
            _viewModel.StopWatchHours = "0";
            _viewModel.StopWatchMinutes = "0";
            _viewModel.StopWatchSeconds = "0";
        }
    }
}
