using Kakuro.Base_Classes;
using Kakuro.Models;
using System.Windows.Input;

namespace Kakuro.Commands
{
    public class AddMinuteAndContinueStopwatchCommand : RelayCommand
    {
        private MyStopwatch _stopwatch;
        private ICommand _startStopwatchCommand;

        public AddMinuteAndContinueStopwatchCommand(MyStopwatch stopwatch, ICommand startStopwatchCommand)
        {
            _stopwatch = stopwatch;
            _startStopwatchCommand = startStopwatchCommand;
        }

        public override void Execute(object? parameter)
        {
            _stopwatch.AddTime(TimeSpan.FromMinutes(1));

            _startStopwatchCommand.Execute(parameter);
        }
    }
}
