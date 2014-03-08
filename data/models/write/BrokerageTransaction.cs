using System;

namespace data.models.write
{
    public class BrokerageTransaction
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime ProcessDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public string TransactionDescription { get; set; }
        public Security Security { get; set; }
        public decimal SharePrice { get; set; }
        public decimal Shares { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
}