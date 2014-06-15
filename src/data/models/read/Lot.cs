using System;

namespace data.models.read
{
    public class Lot
    {
        public DateTime AquiredDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal CostPerShare { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime? CurrentMarketValueDate { get; set; }
        public decimal? CurrentMarketValue { get; set; }
        public decimal? ShortTermCapitalGain { get; set; }
        public decimal? LongTermCapitalGain { get; set; }
        public decimal? TotalGainLoss { get; set; }
    }
}