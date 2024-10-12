using Kakuro.Base_Classes;
using Kakuro.Models;
using Kakuro.ViewModels;
using System.Windows;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class StartStopwatchCommand : RelayCommand
    {
        private MyStopwatch _stopwatch;
        private DashboardViewModel _viewModel;

        public StartStopwatchCommand(MyStopwatch stopwatch, DashboardViewModel viewModel)
        {
            _stopwatch ??= stopwatch;
            _viewModel ??= viewModel;
        }

        public override void Execute(object? parameter)
        {
            _stopwatch.Start();
            Task.Run(async () =>
            {
                while (_stopwatch.IsRunning)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _viewModel.StopWatchHours = _stopwatch.ElapsedHours.ToString();
                        _viewModel.StopWatchMinutes = _stopwatch.ElapsedMinutes.ToString();
                        _viewModel.StopWatchSeconds = _stopwatch.ElapsedSeconds.ToString();
                    });

                    await Task.Delay(1000);
                }
            });
        }
    }
}
