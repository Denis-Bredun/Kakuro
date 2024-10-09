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
    public class RatingTableViewModel : IDisposable
    {
        private IRatingRecordProvider _ratingRecordProvider;
        private RatingTableContainer _ratingTablesContainer;
        private IEventAggregator _eventAggregator;
        private bool _disposed = false;
        private SubscriptionToken? _gameCompletedSubscriptionToken;
        private EventHandler? _saveCompletedHandler;

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

            _eventAggregator = eventAggregator;

            LoadRatingRecordsCommand = new LoadRatingRecordsCommand(_ratingRecordProvider, _ratingTablesContainer);

            SaveRatingRecordCommand = new SaveRatingRecordCommand(_ratingRecordProvider);

            _saveCompletedHandler = (sender, e) =>
            {
                LoadRatingRecordsCommand.Execute(null);
            };

            ((SaveRatingRecordCommand)SaveRatingRecordCommand).SaveCompleted += _saveCompletedHandler;

            LoadRatingRecordsCommand.Execute(null);

            _gameCompletedSubscriptionToken = eventAggregator.GetEvent<GameCompletedEvent>().Subscribe(SaveRatingRecordCommand.Execute);
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
                    if (_saveCompletedHandler != null)
                    {
                        ((SaveRatingRecordCommand)SaveRatingRecordCommand).SaveCompleted -= _saveCompletedHandler;
                        _saveCompletedHandler = null;
                    }

                    if (_gameCompletedSubscriptionToken != null)
                    {
                        _gameCompletedSubscriptionToken.Dispose();
                        _gameCompletedSubscriptionToken = null;
                    }
                }
                _disposed = true;
            }
        }

        ~RatingTableViewModel()
        {
            Dispose(false);
        }
    }
}
