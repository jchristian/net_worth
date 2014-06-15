using System;
using System.Collections.Generic;
using System.Linq;
using core.extensions;
using data.models.read;
using data.models.write;

namespace core.work
{
    public class CostBasisSummaryCalculator
    {
        public CostBasisSummary Calculate(IEnumerable<BrokerageTransaction> transactions, IEnumerable<SecurityPrice> current_prices)
        {
            var security_lots = transactions.GroupBy(x => x.Security)
                                            .Select(x => CalculateSecurityLot(x, current_prices.FirstOrDefault(y => y.Security == x.Key)));

            return new CostBasisSummary
                   {
                       SecurityLots = security_lots,
                       TotalCost = security_lots.Sum(x => x.TotalCost),
                       CurrentMarketValue = security_lots.Sum(x => x.CurrentMarketValue),
                       ShortTermCapitalGain = security_lots.Sum(x => x.ShortTermCapitalGain),
                       LongTermCapitalGain = security_lots.Sum(x => x.LongTermCapitalGain),
                       TotalGainLoss = security_lots.Sum(x => x.TotalGainLoss)
                   };
        }

        public SecurityLot CalculateSecurityLot(IGrouping<Security, BrokerageTransaction> transactions_for_security, SecurityPrice current_price)
        {
            var lots = transactions_for_security.Select(x => CalculateLot(x, current_price)).ToList();

            return new SecurityLot
                   {
                       Lots = lots,
                       SecurityId = transactions_for_security.Key.Id,
                       SecurityName = transactions_for_security.Key.Name,
                       SecurityTicker = transactions_for_security.Key.Ticker,
                       Quantity = lots.Sum(x => x.Quantity),
                       TotalCost = lots.Sum(x => x.TotalCost),
                       CurrentMarketValue = lots.Sum(x => x.CurrentMarketValue),
                       CurrentMarketValueDate = current_price.IfNotNull(x => x.DateTime),
                       ShortTermCapitalGain = lots.Sum(x => x.ShortTermCapitalGain),
                       LongTermCapitalGain = lots.Sum(x => x.LongTermCapitalGain),
                       TotalGainLoss = lots.Sum(x => x.TotalGainLoss)
                   };
        }

        public Lot CalculateLot(BrokerageTransaction transaction, SecurityPrice current_price)
        {
            var lot = new Lot
            {
                AquiredDate = transaction.TradeDate,
                Quantity = transaction.Shares,
                CostPerShare = transaction.SharePrice,
                TotalCost = transaction.GrossAmount,
                CurrentMarketValue = current_price.IfNotNull(x => x.Price) * transaction.Shares,
                CurrentMarketValueDate = current_price.IfNotNull(x => x.DateTime),
            };

            lot.TotalGainLoss = lot.CurrentMarketValue - lot.TotalCost;
            lot.ShortTermCapitalGain = lot.AquiredDate > DateTime.Now.AddYears(-1) ? lot.TotalGainLoss : 0;
            lot.LongTermCapitalGain = lot.AquiredDate < DateTime.Now.AddYears(-1) ? lot.TotalGainLoss : 0;

            return lot;
        }
    }
}