using System.Collections.Generic;

namespace data.models.write
{
    public class Security
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public ICollection<SecurityDescription> SecurityDescriptions { get; set; }
        public ICollection<BrokerageTransaction> BrokerageTransactions { get; set; }
    }
}