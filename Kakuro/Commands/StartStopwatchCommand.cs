using Kakuro.Base_Classes;
using Kakuro.ViewModels;
using System.Diagnostics;

namespace Kakuro.Commands
{
    public class StartStopwatchCommand : RelayCommand
    {
        private Stopwatch _stopwatch;
        private DashboardViewModel _viewModel;

        public StartStopwatchCommand(Stopwatch stopwatch, DashboardViewModel viewModel)
        {
            _stopwatch = stopwatch;
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _stopwatch.Start();
            Task.Run(async () =>
            {
                while (_stopwatch.IsRunning)
                {
                    _viewModel.StopWatchHours = _stopwatch.Elapsed.Hours.ToString();
                    _viewModel.StopWatchMinutes = _stopwatch.Elapsed.Minutes.ToString();
                    _viewModel.StopWatchSeconds = _stopwatch.Elapsed.Seconds.ToString();
                    await Task.Delay(1000);
                }
            });
        }
    }
}
