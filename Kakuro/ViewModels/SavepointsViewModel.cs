using Autofac;
using Kakuro.Base_Classes;
using Kakuro.Commands.SavepointsViewModel;
using Kakuro.Events;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    public class SavepointsViewModel : ViewModelBase
    {
        private SavepointViewModel _selectedSavepoint;
        private ISavepointProvider _savepointProvider;

        public ObservableCollection<SavepointViewModel> Savepoints { get; }

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

        public SavepointsViewModel(
            ILifetimeScope scope,
            IEventAggregator eventAggregator,
            ISavepointProvider savepointProvider)
        {
            Savepoints = new ObservableCollection<SavepointViewModel>();
            _savepointProvider = savepointProvider;

            // #BAD: I think we shouldn't pass DashboardViewModel straightfully
            DeleteSavepointCommand = new DeleteSavepointCommand(this, _savepointProvider);
            CreateSavepointCommand = new CreateSavepointCommand(this, scope.Resolve<DashboardViewModel>(), _savepointProvider);
            RewriteSavepointCommand = new RewriteSavepointCommand(this, scope.Resolve<DashboardViewModel>(), _savepointProvider);
            LoadSavepointCommand = new LoadSavepointCommand(this, scope.Resolve<DashboardViewModel>(), _savepointProvider);
            CleanSavepointsCommand = new CleanSavepointsCommand(this, _savepointProvider);

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
