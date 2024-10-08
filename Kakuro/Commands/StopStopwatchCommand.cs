using Kakuro.Base_Classes;
using System.Diagnostics;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class StopStopwatchCommand : RelayCommand
    {
        private Stopwatch _stopwatch;

        public StopStopwatchCommand(Stopwatch stopwatch)
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
