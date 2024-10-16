using System.Text.Json.Serialization;

namespace Kakuro.Models
{
    public class Savepoint
    {
        public int Id { get; set; }
        public DashboardItemCollection Dashboard { get; set; }

        [JsonConstructor]
        public Savepoint(int id, DashboardItemCollection dashboard)
        {
            Id = id;
            Dashboard = dashboard;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Savepoint other)
            {
                return Id == other.Id &&
                       Dashboard.SequenceEqual(other.Dashboard);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + Id.GetHashCode();
            hash = hash * 31 + (Dashboard != null ? Dashboard.GetHashCode() : 0);
            return hash;
        }
    }
}
