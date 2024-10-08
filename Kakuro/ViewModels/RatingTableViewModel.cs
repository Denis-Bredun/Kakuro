using Kakuro.Interfaces.Data_Access.Data_Providers;

namespace Kakuro.ViewModels
{
    // #BAD: tests should be written
    // #BAD: shall there be interfaces?
    public class RatingTableViewModel
    {
        private IRatingRecordProvider _ratingRecordProvider;

        public RatingTableViewModel(IRatingRecordProvider ratingRecordProvider)
        {
            _ratingRecordProvider = ratingRecordProvider;
        }
    }
}
