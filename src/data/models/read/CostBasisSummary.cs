using System.Collections.Generic;

namespace data.models.read
{
    public class CostBasisSummary
    {
        public int Id { get; set; }
        public IEnumerable<SecurityLot> SecurityLots { get; set; }
        public decimal TotalCost { get; set; }
        public decimal? CurrentMarketValue { get; set; }
        public decimal? ShortTermCapitalGain { get; set; }
        public decimal? LongTermCapitalGain { get; set; }
        public decimal? TotalGainLoss { get; set; }
    }
}