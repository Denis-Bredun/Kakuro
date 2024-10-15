using Kakuro.Base_Classes;
using Kakuro.Models;
using System.Collections.ObjectModel;

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

        public SavepointsViewModel()
        {
            Savepoints = new ObservableCollection<Savepoint>();

            for (int i = 1; i <= 10; i++)
            {
                Savepoints.Add(new Savepoint
                {
                    Id = i,
                    Dashboard = new DashboardItemCollection()
                });
            }
        }
    }

}
