using Kakuro.Base_Classes;
using Kakuro.ViewModels;

namespace Kakuro.Commands
{
    public class SentGameSessionCommand : RelayCommand
    {
        private DashboardViewModel _dashboardViewModel;
        private IEventAggregator _eventAggregator;

        public SentGameSessionCommand(DashboardViewModel dashboardViewModel, IEventAggregator eventAggregator)
        {
            _dashboardViewModel ??= dashboardViewModel;
            _eventAggregator ??= eventAggregator;
        }

        public override void Execute(object? parameter)
        {

        }
    }
}
