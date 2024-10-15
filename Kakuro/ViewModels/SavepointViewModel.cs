using Kakuro.Base_Classes;
using Kakuro.Models;

namespace Kakuro.ViewModels
{
    public class SavepointViewModel : ViewModelBase
    {
        private Savepoint _savepoint;

        public int Id
        {
            get => _savepoint.Id;
            set
            {
                _savepoint.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get => _savepoint.Name;
            set
            {
                _savepoint.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public DashboardItemCollection Dashboard
        {
            get => _savepoint.Dashboard;
            set
            {
                _savepoint.Dashboard = value;
                OnPropertyChanged("Dashboard");
            }
        }

        public SavepointViewModel(Savepoint savepoint)
        {
            _savepoint = savepoint;
        }
    }

}
