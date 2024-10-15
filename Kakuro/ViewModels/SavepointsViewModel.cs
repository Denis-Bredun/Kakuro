using Kakuro.Base_Classes;
using Kakuro.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    public class SavepointsViewModel : ViewModelBase
    {
        public ObservableCollection<Savepoint> Savepoints { get; }

        private Savepoint _selectedSavepoint;
        public Savepoint SelectedSavepoint
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

        public ICommand Load { get; }
        public ICommand Create { get; }
        public ICommand Rewrite { get; }
        public ICommand Delete { get; }

        public SavepointsViewModel()
        {
            Savepoints = new ObservableCollection<Savepoint>();
        }
    }

}
