using System.Collections.Generic;

namespace data.models.write
{
    public class Lot
    {
        public int Id { get; set; }
        public BrokerageTransaction BrokerageTransaction { get; set; }
        public int BrokerageTransactionId { get; set; }
        public ICollection<Trade> Trades { get; set; }
        public bool IsOpen { get; set; }
        public decimal RemainingShares { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}