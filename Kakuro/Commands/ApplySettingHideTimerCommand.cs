using Kakuro.Base_Classes;
using Kakuro.ViewModels;

namespace Kakuro.Commands
{
    class ApplySettingHideTimerCommand : RelayCommand
    {
        private DashboardViewModel _dashboardViewModel;

        public ApplySettingHideTimerCommand(DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
        }

        public override void Execute(object? parameter)
        {
            var hideTimerSetting = (SettingViewModel)parameter;

            _dashboardViewModel.IsTimerVisible = !hideTimerSetting.IsEnabled;
        }
    }
}
