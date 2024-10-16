using Kakuro.Base_Classes;

namespace Kakuro.Commands.GetChangedSettingsCommand
{
    class ApplySettingHideTimerCommand : RelayCommand
    {
        private ViewModels.DashboardViewModel _dashboardViewModel;

        public ApplySettingHideTimerCommand(ViewModels.DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
        }

        public override void Execute(object? parameter)
        {
            var hideTimerSetting = (ViewModels.SettingViewModel)parameter;

            _dashboardViewModel.IsTimerVisible = !hideTimerSetting.IsEnabled;
        }
    }
}
