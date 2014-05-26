using System.Data.Entity;

namespace data.models.write
{
    public class Security
    {
        public int Id { get; set; }
        public int? SpecId { get; set; }
        public string Ticker { get; set; }
        public string Description { get; set; }
        public IDbSet<SecurityDescription> SecurityDescriptions { get; set; }
    }
}