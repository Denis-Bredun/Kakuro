using Autofac;
using Kakuro.Base_Classes;
using Kakuro.Commands;
using Kakuro.Enums;
using Kakuro.Events;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Game_Tools;
using Kakuro.Models;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    // #BAD: shall there be interfaces?
    public class DashboardViewModel : ViewModelBase
    {
        private DifficultyLevels _choosenDifficulty;
        private MyStopwatch _stopwatch;
        private string _stopWatchHours, _stopWatchMinutes, _stopWatchSeconds;
        private bool _isGameCompleted, _showCorrectAnswers, _autoSubmit, _disposed;
        private SubscriptionToken _settingsChangedSubscriptionTokens;

        public DashboardItemCollection Dashboard { get; }
        public bool IsGameCompleted
        {
            get => _isGameCompleted;
            set
            {
                _isGameCompleted = value;
                OnPropertyChanged("IsGameCompleted");
            }
        }
        public bool ShowCorrectAnswers
        {
            get => _showCorrectAnswers;
            set
            {
                _showCorrectAnswers = value;
                OnPropertyChanged("ShowCorrectAnswers");
            }
        }
        public bool AutoSubmit
        {
            get => _autoSubmit;
            set
            {
                _autoSubmit = value;
                OnPropertyChanged("AutoSubmit");
            }
        }
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
        public ICommand ValidateSolutionCommand { get; }
        public ICommand CleanDashboardCommand { get; }
        public ICommand StartStopwatchCommand { get; }
        public ICommand StopStopwatchCommand { get; }
        public ICommand RestartStopwatchCommand { get; }
        public ICommand SentGameSessionCommand { get; }
        public ICommand GetChangedSettingsCommands { get; }
        public ICommand AddMinuteAndContinueStopwatchCommand { get; }
        public ICommand AutoSubmitCommand { get; }

        public DashboardViewModel(ILifetimeScope scope, IEventAggregator eventAggregator, DashboardItemCollection dashboard)
        {
            ChoosenDifficulty = DifficultyLevels.Easy;
            Dashboard = dashboard;
            IsGameCompleted = false;
            ShowCorrectAnswers = false;
            AutoSubmit = true;

            // #BAD: we shall create commands and some other objects through Lazy way

            _stopwatch = new MyStopwatch(new TimeSpan());
            StartStopwatchCommand = new StartStopwatchCommand(_stopwatch, this);
            StopStopwatchCommand = new StopStopwatchCommand(_stopwatch);
            RestartStopwatchCommand = new RestartStopwatchCommand(_stopwatch, StartStopwatchCommand, StopStopwatchCommand);
            AddMinuteAndContinueStopwatchCommand = new AddMinuteAndContinueStopwatchCommand(_stopwatch, StartStopwatchCommand);
            StopWatchHours = _stopwatch.Elapsed.Hours.ToString();
            StopWatchMinutes = _stopwatch.Elapsed.Minutes.ToString();
            StopWatchSeconds = _stopwatch.Elapsed.Seconds.ToString();

            SentGameSessionCommand = new SendGameSessionCommand(this, eventAggregator);

            ValidateSolutionCommand = new ValidateSolutionCommand(
                scope.Resolve<ISolutionVerifier>(),
                scope.Resolve<IOperationNotifier>(),
                StopStopwatchCommand,
                SentGameSessionCommand,
                this);

            ApplyDifficultyCommand = new ApplyDifficultyCommand(
                scope.Resolve<IDashboardProvider>(),
                this,
                RestartStopwatchCommand);

            CleanDashboardCommand = scope.Resolve<CleanDashboardCommand>();
            NewGameCommand = ApplyDifficultyCommand;

            GetChangedSettingsCommands = new GetChangedSettingsCommands(
                this,
                StopStopwatchCommand,
                AddMinuteAndContinueStopwatchCommand,
                CleanDashboardCommand);

            AutoSubmitCommand = new AutoSubmitCommand(this, ValidateSolutionCommand);

            ApplyDifficultyCommand.Execute(ChoosenDifficulty);

            _settingsChangedSubscriptionTokens = eventAggregator.GetEvent<SettingsChangedEvent>().Subscribe(GetChangedSettingsCommands.Execute);
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
                    if (_settingsChangedSubscriptionTokens != null)
                    {
                        _settingsChangedSubscriptionTokens.Dispose();
                        _settingsChangedSubscriptionTokens = null;
                    }
                }
                _disposed = true;
            }
        }

        ~DashboardViewModel()
        {
            Dispose(false);
        }
    }
}
