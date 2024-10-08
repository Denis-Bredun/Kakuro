using Autofac;
using Kakuro.Base_Classes;
using Kakuro.Commands;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Game_Tools;
using System.Diagnostics;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    // #BAD: shall there be interfaces?
    public class DashboardViewModel : ViewModelBase
    {
        private DifficultyLevels _choosenDifficulty;
        private Stopwatch _stopwatch;
        private string _stopWatchHours;
        private string _stopWatchMinutes;
        private string _stopWatchSeconds;

        public DashboardItemCollection Dashboard { get; }

        public DifficultyLevels ChoosenDifficulty
        {
            get => _choosenDifficulty;
            set
            {
                _choosenDifficulty = value;
                OnPropertyChanged("ChoosenDifficulty");
            }
        }
        public string StopWatchHours
        {
            get { return _stopWatchHours; }
            set { _stopWatchHours = value; OnPropertyChanged("StopWatchHours"); }
        }
        public string StopWatchMinutes
        {
            get { return _stopWatchMinutes; }
            set { _stopWatchMinutes = value; OnPropertyChanged("StopWatchMinutes"); }
        }
        public string StopWatchSeconds
        {
            get { return _stopWatchSeconds; }
            set { _stopWatchSeconds = value; OnPropertyChanged("StopWatchSeconds"); }
        }

        public ICommand ApplyDifficultyCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand VerifySolutionCommand { get; }
        public ICommand CleanDashboardCommand { get; }
        public ICommand StartStopwatchCommand { get; }
        public ICommand StopStopwatchCommand { get; }
        public ICommand RestartStopwatchCommand { get; }
        public ICommand SentGameSessionCommand { get; }

        public DashboardViewModel(ILifetimeScope scope, DashboardItemCollection dashboard)
        {
            ChoosenDifficulty = DifficultyLevels.Easy;
            Dashboard = dashboard;

            _stopwatch = new Stopwatch();
            StartStopwatchCommand = new StartStopwatchCommand(_stopwatch, this);
            StopStopwatchCommand = new StopStopwatchCommand(_stopwatch);
            RestartStopwatchCommand = new RestartStopwatchCommand(_stopwatch, StartStopwatchCommand, StopStopwatchCommand);
            StopWatchHours = _stopwatch.Elapsed.Hours.ToString();
            StopWatchMinutes = _stopwatch.Elapsed.Minutes.ToString();
            StopWatchSeconds = _stopwatch.Elapsed.Seconds.ToString();

            SentGameSessionCommand = new SentGameSessionCommand(this);

            VerifySolutionCommand = new VerifySolutionCommand(
                scope.Resolve<ISolutionVerifier>(),
                scope.Resolve<IOperationNotifier>(),
                StopStopwatchCommand,
                SentGameSessionCommand);

            ApplyDifficultyCommand = new ApplyDifficultyCommand(
                scope.Resolve<IDashboardProvider>(),
                this,
                RestartStopwatchCommand);

            CleanDashboardCommand = scope.Resolve<CleanDashboardCommand>();
            NewGameCommand = ApplyDifficultyCommand;

            ApplyDifficultyCommand.Execute(ChoosenDifficulty);
        }
    }
}
