using Kakuro.Base_Classes;
using System.Diagnostics;
using System.Windows.Input;

namespace Kakuro.Commands
{
    public class RestartStopwatchCommand : RelayCommand
    {
        private Stopwatch _stopwatch;
        private ICommand _startStopwatchCommand;

        public RestartStopwatchCommand(Stopwatch stopwatch, ICommand startStopwatchCommand)
        {
            _stopwatch = stopwatch;
            _startStopwatchCommand = startStopwatchCommand;
        }

        public override void Execute(object? parameter)
        {
            _stopwatch.Reset();
            _startStopwatchCommand.Execute(parameter);
        }
    }
}
