using System;
using System.Collections.Generic;

namespace data.models.read
{
    public class SecurityLot
    {
        public int Id { get; set; }
        public int SecurityId { get; set; }
        public string SecurityTicker { get; set; }
        public string SecurityName { get; set; }
        public IEnumerable<Lot> Lots { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime? CurrentMarketValueDate { get; set; }
        public decimal? CurrentMarketValue { get; set; }
        public decimal? ShortTermCapitalGain { get; set; }
        public decimal? LongTermCapitalGain { get; set; }
        public decimal? TotalGainLoss { get; set; }
    }
}