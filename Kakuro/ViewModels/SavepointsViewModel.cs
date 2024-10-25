using Autofac;
using Kakuro.Base_Classes;
using Kakuro.Commands.SavepointsViewModel;
using Kakuro.Events;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Game_Tools;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    public class SavepointsViewModel : ViewModelBase
    {
        private SavepointViewModel _selectedSavepoint;
        private ISavepointProvider _savepointProvider;
        private IOperationNotifier _operationNotifier;
        private bool _correctAnswersAreShown;

        public ObservableCollection<SavepointViewModel> Savepoints { get; }

        public bool CorrectAnswersAreShown
        {
            get => _correctAnswersAreShown;
            set
            {
                _correctAnswersAreShown = value;
                OnPropertyChanged("CorrectAnswersAreShown");
            }
        }

        public SavepointViewModel SelectedSavepoint
        {
            get => _selectedSavepoint;
            set
            {
                if (_selectedSavepoint != value)
                {
                    _selectedSavepoint = value;
                    OnPropertyChanged(nameof(SelectedSavepoint));
                }
            }
        }

        public bool IsCreatingAllowed
        {
            get => _isCreatingAllowed;
            set
            {
                _isCreatingAllowed = value;
                OnPropertyChanged("IsCreatingAllowed");
            }
        }

        public ICommand LoadSavepointCommand { get; }
        public ICommand CreateSavepointCommand { get; }
        public ICommand RewriteSavepointCommand { get; }
        public ICommand DeleteSavepointCommand { get; }
        public ICommand CleanSavepointsCommand { get; }
        public ICommand ChangeAvailabilityOfSectionCommand { get; }

        private SubscriptionToken _newGameStartedSubscriptionToken;
        private SubscriptionToken _correctAnswersTurnedOnSubscriptionToken;
        private bool _disposed;
        private bool _isCreatingAllowed;

        public SavepointsViewModel(
            ILifetimeScope scope,
            IEventAggregator eventAggregator,
            ISavepointProvider savepointProvider)
        {
            Savepoints = new ObservableCollection<SavepointViewModel>();
            _savepointProvider = savepointProvider;
            _operationNotifier = scope.Resolve<IOperationNotifier>();
            IsCreatingAllowed = true;
            CorrectAnswersAreShown = false;

            // #BAD: I think we shouldn't pass DashboardViewModel straightfully
            DeleteSavepointCommand = new DeleteSavepointCommand(this, _savepointProvider);
            CreateSavepointCommand = new CreateSavepointCommand(this, scope.Resolve<DashboardViewModel>(), _savepointProvider);
            RewriteSavepointCommand = new RewriteSavepointCommand(this, scope.Resolve<DashboardViewModel>(), _savepointProvider, _operationNotifier);
            LoadSavepointCommand = new LoadSavepointCommand(this, scope.Resolve<DashboardViewModel>(), _savepointProvider);
            CleanSavepointsCommand = new CleanSavepointsCommand(this, _savepointProvider);
            ChangeAvailabilityOfSectionCommand = new ChangeAvailabilityOfSectionCommand(this);

            _newGameStartedSubscriptionToken = eventAggregator.GetEvent<NewGameStartedEvent>().Subscribe(CleanSavepointsCommand.Execute);
            _correctAnswersTurnedOnSubscriptionToken = eventAggregator.GetEvent<CorrectAnswersTurnedOnEvent>().Subscribe(ChangeAvailabilityOfSectionCommand.Execute);
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
                    if (_newGameStartedSubscriptionToken != null)
                    {
                        _newGameStartedSubscriptionToken.Dispose();
                        _newGameStartedSubscriptionToken = null;
                    }

                    if (_correctAnswersTurnedOnSubscriptionToken != null)
                    {
                        _correctAnswersTurnedOnSubscriptionToken.Dispose();
                        _correctAnswersTurnedOnSubscriptionToken = null;
                    }

                    (DeleteSavepointCommand as IDisposable)?.Dispose();
                    (CreateSavepointCommand as IDisposable)?.Dispose();
                    (RewriteSavepointCommand as IDisposable)?.Dispose();
                    (LoadSavepointCommand as IDisposable)?.Dispose();

                    CleanSavepointsCommand.Execute(null);
                }
                _disposed = true;
            }
        }

        ~SavepointsViewModel()
        {
            Dispose(false);
        }
    }

}
