using Kakuro.Base_Classes;
using Kakuro.Models;

namespace Kakuro.Commands.DashboardViewModel
{
    // #BAD: tests shall be written
    public class StopStopwatchCommand : RelayCommand
    {
        private MyStopwatch _stopwatch;

        public StopStopwatchCommand(MyStopwatch stopwatch)
        {
            _stopwatch ??= stopwatch;
        }

        public override void Execute(object? parameter)
        {
            if (_stopwatch.IsRunning)
                _stopwatch.Stop();
        }
    }
}
