using System;

namespace data.models.write
{
    public class Trade
    {
        public int Id { get; set; }
        public DateTime AquireDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public Lot Position { get; set; }
        public int PositionId { get; set; }
        public BrokerageTransaction ClosingTransaction { get; set; }
        public int ClosingTransactionId { get; set; }
        public decimal Quantity { get; set; }
        public decimal SellPrice { get; set; }
        public decimal ProfileAndLoss { get; set; }
    }
}