using Kakuro.Base_Classes;
using Kakuro.Interfaces.Data_Access.Data_Providers;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class SaveRatingRecordCommand : RelayCommand
    {
        private IRatingRecordProvider _ratingRecordProvider;
        private RatingTableContainer _ratingTablesContainer;

        public event EventHandler? SaveCompleted;

        public SaveRatingRecordCommand(IRatingRecordProvider ratingRecordProvider, RatingTableContainer ratingTablesContainer)
        {
            _ratingRecordProvider = ratingRecordProvider;
            _ratingTablesContainer = ratingTablesContainer;
        }
        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();

            OnSaveCompleted();
        }

        protected virtual void OnSaveCompleted()
        {
            SaveCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
