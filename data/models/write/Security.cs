using System.Data.Entity;

namespace data.models.write
{
    public class Security
    {
        public int Id { get; set; }
        public DbSet<SecurityDescription> SecurityDescriptions { get; set; } 
    }
}