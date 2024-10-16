using Autofac;
using Kakuro.Base_Classes;
using Kakuro.Commands.SavepointsViewModel;
using Kakuro.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    public class SavepointsViewModel : ViewModelBase
    {
        public ObservableCollection<SavepointViewModel> Savepoints { get; }

        private SavepointViewModel _selectedSavepoint;
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

        public ICommand LoadSavepointCommand { get; }
        public ICommand CreateSavepointCommand { get; }
        public ICommand RewriteSavepointCommand { get; }
        public ICommand DeleteSavepointCommand { get; }
        public ICommand CleanSavepointsCommand { get; }

        private SubscriptionToken _newGameStartedSubscriptionToken;
        private bool _disposed;

        public SavepointsViewModel(ILifetimeScope scope, IEventAggregator eventAggregator)
        {
            Savepoints = new ObservableCollection<SavepointViewModel>();

            // #BAD: I think we shouldn't pass DashboardViewModel straightfully
            DeleteSavepointCommand = new DeleteSavepointCommand(this);
            CreateSavepointCommand = new CreateSavepointCommand(this, scope.Resolve<DashboardViewModel>());
            RewriteSavepointCommand = new RewriteSavepointCommand(this, scope.Resolve<DashboardViewModel>());
            LoadSavepointCommand = new LoadSavepointCommand(this, scope.Resolve<DashboardViewModel>());
            CleanSavepointsCommand = new CleanSavepointsCommand(this);

            _newGameStartedSubscriptionToken = eventAggregator.GetEvent<NewGameStartedEvent>().Subscribe(CleanSavepointsCommand.Execute);
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
