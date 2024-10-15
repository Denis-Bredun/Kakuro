using Kakuro.Base_Classes;
using Kakuro.Models;
using System.Collections.ObjectModel;

namespace Kakuro.ViewModels
{
    public class SavepointsViewModel : ViewModelBase
    {
        public ObservableCollection<Savepoint> Savepoints { get; }

        public SavepointsViewModel()
        {
            Savepoints = new ObservableCollection<Savepoint>();
        }
    }
}
