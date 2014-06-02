using System.Collections.Generic;

namespace data.models.write
{
    public class Security
    {
        public int Id { get; set; }
        public int? SpecId { get; set; }
        public string Ticker { get; set; }
        public string Description { get; set; }
        public ICollection<SecurityDescription> SecurityDescriptions { get; set; }
        public ICollection<BrokerageTransaction> BrokerageTransactions { get; set; }
    }
}