using Kakuro.Models;

namespace Kakuro.Events
{
    // #BAD: there shall be tests for using Event Aggregator
    public class GameCompletedEvent : PubSubEvent<GameSession>
    {
    }
}
