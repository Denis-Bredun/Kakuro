using Kakuro.Base_Classes;
using Kakuro.Models;
using System.Windows.Input;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class RestartStopwatchCommand : RelayCommand
    {
        private MyStopwatch _stopwatch;
        private ICommand _startStopwatchCommand;
        private ICommand _stopStopwatchCommand;

        public RestartStopwatchCommand(MyStopwatch stopwatch, ICommand startStopwatchCommand, ICommand stopStopwatchCommand)
        {
            _stopwatch ??= stopwatch;
            _startStopwatchCommand ??= startStopwatchCommand;
            _stopStopwatchCommand ??= stopStopwatchCommand;
        }

        public override void Execute(object? parameter)
        {
            _stopStopwatchCommand.Execute(parameter);
            _stopwatch.Reset();
            _startStopwatchCommand.Execute(parameter);
        }
    }
}
