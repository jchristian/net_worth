using System;
using System.Collections.Generic;
using System.Linq;
using core.exceptions;
using data.models.write;

namespace core.work
{
    public class LIFOTradeCalculator
    {
        public IEnumerable<Trade> Calculate(IEnumerable<BrokerageTransaction> brokerage_transactions, IEnumerable<Lot> lots)
        {
            var open_lots_by_security = lots.Where(x => x.IsOpen && x.BrokerageTransaction.SecurityId != null)
                                            .GroupBy(x => new { x.BrokerageTransaction.SecurityId, x.BrokerageTransaction.AccountId }).ToList();
            var sell_transactions_by_security = brokerage_transactions.Where(x => x.GrossAmount < 0 && x.SecurityId != null)
                                                                      .GroupBy(x => new { x.SecurityId, x.AccountId }).ToList();

            return sell_transactions_by_security
                .Join(open_lots_by_security, x => x.Key, x => x.Key, (sell_transactions, open_lots) => new { sell_transactions, open_lots })
                .SelectMany(x => CalculateTradesForSingleSecurityInAccount(x.sell_transactions, x.open_lots)).ToList();
        }

        //Assume we aren't shorting positions
        public IEnumerable<Trade> CalculateTradesForSingleSecurityInAccount(IEnumerable<BrokerageTransaction> sell_transactions, IEnumerable<Lot> lots)
        {
            var ordered_transactions = sell_transactions.OrderBy(x => x.TradeDate).ToList();
            var ordered_lots = lots.OrderBy(x => x.BrokerageTransaction.TradeDate).ToList();

            var trades = new List<Trade>();

            foreach (var transaction in ordered_transactions)
            {
                var quantity = -transaction.Shares;

                while (quantity > 0 && ordered_lots.Any())
                {
                    var position = ordered_lots.First();
                    if (quantity >= position.RemainingShares)
                    {
                        ordered_lots.Remove(position);
                        quantity -= position.RemainingShares;
                        trades.Add(new Trade
                        {
                            AquireDate = position.BrokerageTransaction.TradeDate,
                            ClosingDate = transaction.TradeDate,
                            ClosingTransactionId = transaction.Id,
                            PositionId = position.Id,
                            Quantity = position.RemainingShares,
                            SellPrice = transaction.SharePrice,
                            ProfileAndLoss = (position.RemainingShares * transaction.SharePrice) - (position.RemainingShares * position.BrokerageTransaction.SharePrice)
                        });
                        position.IsOpen = false;
                    }
                    else
                    {
                        position.RemainingShares -= quantity;
                        trades.Add(new Trade
                        {
                            AquireDate = position.BrokerageTransaction.TradeDate,
                            ClosingDate = transaction.TradeDate,
                            ClosingTransactionId = transaction.Id,
                            PositionId = position.Id,
                            Quantity = quantity,
                            SellPrice = transaction.SharePrice,
                            ProfileAndLoss = (quantity * transaction.SharePrice) - (quantity * position.BrokerageTransaction.SharePrice)
                        });
                        quantity = 0;
                    }
                }

                if (quantity > 0)
                    throw new SoldTooManySharesException(transaction);
            }

            return trades;
        }
    }
}