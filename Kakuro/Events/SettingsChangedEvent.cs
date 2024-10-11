using Kakuro.Models;
using System.Collections.ObjectModel;

namespace Kakuro.Events
{
    // #BAD: there shall be tests for using Event Aggregator
    public class SettingsChangedEvent : PubSubEvent<ObservableCollection<Setting>>
    {
    }
}
