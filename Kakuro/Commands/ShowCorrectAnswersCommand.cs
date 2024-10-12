using Kakuro.Base_Classes;
using Kakuro.Models;
using Kakuro.ViewModels;

namespace Kakuro.Commands
{
    public class ShowCorrectAnswersCommand : RelayCommand
    {
        private DashboardViewModel _dashboardViewModel;

        public ShowCorrectAnswersCommand(DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
        }

        public override void Execute(object? parameter)
        {
            var showCorrectAnswersSetting = (Setting)parameter;

            _dashboardViewModel.ShowCorrectAnswers = showCorrectAnswersSetting.IsEnabled;
        }
    }
}
