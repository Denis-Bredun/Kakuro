using Kakuro.Base_Classes;
using Kakuro.Commands;
using Kakuro.Models;
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

        public SavepointsViewModel()
        {
            Savepoints = new ObservableCollection<SavepointViewModel>();

            for (int i = 1; i <= 10; i++)
            {
                var savepoint = new Savepoint(i, $"Savepoint #{i}", new DashboardItemCollection());
                Savepoints.Add(new SavepointViewModel(savepoint));
            }

            DeleteSavepointCommand = new DeleteSavepointCommand(this);
        }
    }

}
