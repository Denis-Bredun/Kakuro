using Kakuro.Base_Classes;
using Kakuro.Enums;
using Kakuro.Models;
using Kakuro.ViewModels;
using System.Collections.ObjectModel;

namespace Kakuro.Commands
{
    // #BAD: tests shall be written
    public class ShowCorrectAnswersCommand : RelayCommand
    {
        private DashboardViewModel _dashboardViewModel;

        public ShowCorrectAnswersCommand(DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
        }
        public override void Execute(object? parameter)
        {
            // #BAD: if we have many settings, we will lose speed of application work because of all of this convertations
            var settings = (ObservableCollection<Setting>)parameter;

            Setting showCorrectAnswersSetting = settings.FirstOrDefault(el => el.SettingType == Enums.SettingType.ShowCorrectValues);

            _dashboardViewModel.ShowCorrectAnswers = showCorrectAnswersSetting.IsEnabled;

            var dashboardSize = _dashboardViewModel.Dashboard.Count;
            DashboardItemViewModel currentElement;
            for (var i = 1; i < dashboardSize - 1; i++)
                for (int j = 1; j < dashboardSize - 1; j++)
                {
                    currentElement = _dashboardViewModel.Dashboard[i][j];

                    if (currentElement.CellType == CellType.ValueCell)
                        currentElement.DisplayValue = _dashboardViewModel.ShowCorrectAnswers ? currentElement.HiddenValue : "";
                }
        }

        // Note: shall I lock the dashboard? Stopwatch? And reset it to 0 when turning off the setting. What if I start new game when setting is ON?
    }
}
