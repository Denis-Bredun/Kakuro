using Kakuro.Base_Classes;
using Kakuro.Events;
using Kakuro.Models;
using Kakuro.ViewModels;

namespace Kakuro.Commands
{
    public class SendGameSessionCommand : RelayCommand
    {
        private DashboardViewModel _dashboardViewModel;
        private IEventAggregator _eventAggregator;

        public SendGameSessionCommand(DashboardViewModel dashboardViewModel, IEventAggregator eventAggregator)
        {
            _dashboardViewModel ??= dashboardViewModel;
            _eventAggregator ??= eventAggregator;
        }

        public override void Execute(object? parameter)
        {
            GameSession session = new GameSession(
                _dashboardViewModel.ChoosenDifficulty,
                _dashboardViewModel.StopWatchHours,
                _dashboardViewModel.StopWatchMinutes,
                _dashboardViewModel.StopWatchSeconds);

            _eventAggregator.GetEvent<GameCompletedEvent>().Publish(session);
        }
    }
}
