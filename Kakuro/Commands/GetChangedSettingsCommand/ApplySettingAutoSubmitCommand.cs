using Kakuro.Base_Classes;

namespace Kakuro.Commands.GetChangedSettingsCommand
{
    public class ApplySettingAutoSubmitCommand : RelayCommand
    {
        private ViewModels.DashboardViewModel _dashboardViewModel;

        public ApplySettingAutoSubmitCommand(ViewModels.DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
        }
        public override void Execute(object? parameter)
        {
            var autoSubmitSetting = (ViewModels.SettingViewModel)parameter;

            _dashboardViewModel.AutoSubmit = autoSubmitSetting.IsEnabled;
        }
    }
}
