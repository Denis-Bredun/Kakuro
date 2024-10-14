using Kakuro.Base_Classes;
using Kakuro.Models;
using Kakuro.ViewModels;
using System.Windows.Threading;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class StartStopwatchCommand : RelayCommand, IDisposable
    {
        private MyStopwatch _stopwatch;
        private DashboardViewModel _viewModel;
        private DispatcherTimer _timer;
        private bool _disposed = false;

        public StartStopwatchCommand(MyStopwatch stopwatch, DashboardViewModel viewModel)
        {
            _stopwatch = stopwatch;
            _viewModel = viewModel;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_stopwatch.IsRunning)
            {
                _viewModel.StopWatchHours = _stopwatch.ElapsedHours.ToString();
                _viewModel.StopWatchMinutes = _stopwatch.ElapsedMinutes.ToString();
                _viewModel.StopWatchSeconds = _stopwatch.ElapsedSeconds.ToString();
            }
        }

        public override void Execute(object? parameter)
        {
            _stopwatch.Start();
            _timer.Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_timer != null)
                    {
                        _timer.Stop();
                        _timer.Tick -= Timer_Tick;
                        _timer = null;
                    }
                }
                _disposed = true;
            }
        }

        ~StartStopwatchCommand()
        {
            Dispose(false);
        }
    }
}
