using Kakuro.Base_Classes;
using Kakuro.Models;
using Kakuro.ViewModels;

namespace Kakuro.Commands
{
    public class ApplySettingAutoSubmitCommand : RelayCommand
    {
        private DashboardViewModel _dashboardViewModel;

        public ApplySettingAutoSubmitCommand(DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
        }
        public override void Execute(object? parameter)
        {
            var autoSubmitSetting = (Setting)parameter;

            _dashboardViewModel.AutoSubmit = autoSubmitSetting.IsEnabled;
        }
    }
}
