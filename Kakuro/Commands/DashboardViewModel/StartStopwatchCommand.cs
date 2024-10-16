using Kakuro.Base_Classes;
using Kakuro.Models;
using System.Windows;

namespace Kakuro.Commands.DashboardViewModel
{
    // #BAD: tests shall be written
    public class StartStopwatchCommand : RelayCommand
    {
        private MyStopwatch _stopwatch;
        private ViewModels.DashboardViewModel _viewModel;

        public StartStopwatchCommand(MyStopwatch stopwatch, ViewModels.DashboardViewModel viewModel)
        {
            _stopwatch ??= stopwatch;
            _viewModel ??= viewModel;
        }

        public override void Execute(object? parameter)
        {
            _stopwatch.Start();
            Task.Run(async () =>
            {
                while (_stopwatch.IsRunning && Application.Current != null)
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
