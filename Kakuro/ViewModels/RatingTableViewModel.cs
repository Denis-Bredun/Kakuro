using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    // #BAD: shall there be interfaces?
    public class RatingTableViewModel
    {
        private IRatingRecordProvider _ratingRecordProvider;

        public ObservableCollection<RatingRecord> EasyRatingRecords { get; }
        public ObservableCollection<RatingRecord> NormalRatingRecords { get; }
        public ObservableCollection<RatingRecord> HardRatingRecords { get; }

        public ICommand LoadRatingRecordsCommand { get; }
        public ICommand SaveRatingRecordCommand { get; }

        public RatingTableViewModel(IRatingRecordProvider ratingRecordProvider)
        {
            _ratingRecordProvider = ratingRecordProvider;
        }
    }
}
