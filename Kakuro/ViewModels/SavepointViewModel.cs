using Kakuro.Base_Classes;

namespace Kakuro.ViewModels
{
    public class SavepointViewModel : ViewModelBase
    {
        private string _name;
        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public SavepointViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
