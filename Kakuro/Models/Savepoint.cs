namespace Kakuro.Models
{
    public class Savepoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DashboardItemCollection Dashboard { get; set; }

        public Savepoint(int id, string name, DashboardItemCollection dashboard)
        {
            Id = id;
            Name = name;
            Dashboard = dashboard;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Savepoint other)
            {
                return Id == other.Id &&
                       Name == other.Name &&
                       Dashboard.SequenceEqual(other.Dashboard);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + Id.GetHashCode();
            hash = hash * 31 + (Name != null ? Name.GetHashCode() : 0);
            hash = hash * 31 + (Dashboard != null ? Dashboard.GetHashCode() : 0);
            return hash;
        }
    }

}
