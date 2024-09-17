namespace Kakuro.Models
{
    public class Savepoint
    {
        public int Id { get; set; }
        public List<DashboardItem> DashboardItems { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Savepoint other)
            {
                return Id == other.Id &&
                       DashboardItems.SequenceEqual(other.DashboardItems);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + Id.GetHashCode();
            hash = hash * 31 + (DashboardItems != null ? DashboardItems.GetHashCode() : 0);
            return hash;
        }
    }
}
