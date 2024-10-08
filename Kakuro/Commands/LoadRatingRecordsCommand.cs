using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using System.Collections.ObjectModel;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class LoadRatingRecordsCommand : RelayCommand
    {
        private IRatingRecordProvider _ratingRecordProvider;
        private RatingTableContainer _ratingTablesContainer;

        public LoadRatingRecordsCommand(IRatingRecordProvider ratingRecordProvider, RatingTableContainer ratingTablesContainer)
        {
            _ratingRecordProvider = ratingRecordProvider;
            _ratingTablesContainer = ratingTablesContainer;
        }
        public override void Execute(object? parameter)
        {
            var difficultyLevels = (DifficultyLevels[])Enum.GetValues(typeof(DifficultyLevels));

            foreach (var difficultyLevel in difficultyLevels)
                LoadDataForConcreteDifficulty(difficultyLevel);
        }

        private void LoadDataForConcreteDifficulty(DifficultyLevels difficultyLevel)
        {
            var iEnumerableData = _ratingRecordProvider.GetAll(difficultyLevel);
            _ratingTablesContainer[difficultyLevel] = ConvertIEnumerableToObservable(iEnumerableData);
        }

        private ObservableCollection<T> ConvertIEnumerableToObservable<T>(IEnumerable<T> values)
        {
            return values as ObservableCollection<T>;
        }
    }
}
