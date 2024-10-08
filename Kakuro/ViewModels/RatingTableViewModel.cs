using Kakuro.Commands;
using Kakuro.Enums;
using Kakuro.Events;
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
        private RatingTableContainer _ratingTablesContainer;

        public ObservableCollection<RatingRecord> EasyRatingRecords { get; }
        public ObservableCollection<RatingRecord> NormalRatingRecords { get; }
        public ObservableCollection<RatingRecord> HardRatingRecords { get; }

        public ICommand LoadRatingRecordsCommand { get; }
        public ICommand SaveRatingRecordCommand { get; }

        public RatingTableViewModel(IRatingRecordProvider ratingRecordProvider, IEventAggregator eventAggregator)
        {
            _ratingRecordProvider = ratingRecordProvider;
            EasyRatingRecords = new ObservableCollection<RatingRecord>();
            NormalRatingRecords = new ObservableCollection<RatingRecord>();
            HardRatingRecords = new ObservableCollection<RatingRecord>();
            _ratingTablesContainer = new RatingTableContainer
            {
                { DifficultyLevels.Easy, EasyRatingRecords },
                { DifficultyLevels.Normal, NormalRatingRecords },
                { DifficultyLevels.Hard, HardRatingRecords }
            };

            LoadRatingRecordsCommand = new LoadRatingRecordsCommand(_ratingRecordProvider, _ratingTablesContainer);

            SaveRatingRecordCommand = new SaveRatingRecordCommand(_ratingRecordProvider);

            ((SaveRatingRecordCommand)SaveRatingRecordCommand).SaveCompleted += (sender, e) =>
            {
                LoadRatingRecordsCommand.Execute(null);
            };

            LoadRatingRecordsCommand.Execute(null);

            eventAggregator.GetEvent<GameCompletedEvent>().Subscribe(SaveRatingRecordCommand.Execute);
        }
    }
}
