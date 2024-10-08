using Kakuro.Base_Classes;
using Kakuro.ViewModels;

namespace Kakuro.Commands
{
    public class SentGameSessionCommand : RelayCommand
    {
        private DashboardViewModel _dashboardViewModel;

        public SentGameSessionCommand(DashboardViewModel dashboardViewModel)
        {
            _dashboardViewModel ??= dashboardViewModel;
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
