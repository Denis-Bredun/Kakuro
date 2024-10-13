using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Models;
using static System.Convert;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class SaveRatingRecordCommand : RelayCommand
    {
        private IRatingRecordProvider _ratingRecordProvider;

        public event EventHandler? SaveCompleted;

        public SaveRatingRecordCommand(IRatingRecordProvider ratingRecordProvider)
        {
            _ratingRecordProvider ??= ratingRecordProvider;
        }
        public override void Execute(object? parameter)
        {
            if (parameter == null)
                throw new NullReferenceException("Parameter for SaveRatingRecordCommand is null!");

            GameSession session = (GameSession)parameter;

            (DifficultyLevels difficulty, string hours, string minutes, string seconds) = session;

            RatingRecord ratingRecord = new RatingRecord(ToInt32(hours), ToInt32(minutes), ToInt32(seconds));

            _ratingRecordProvider.Add(ratingRecord, difficulty);

            OnSaveCompleted();
        }

        protected virtual void OnSaveCompleted()
        {
            SaveCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
