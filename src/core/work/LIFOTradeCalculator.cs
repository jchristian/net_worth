using System;
using System.Collections.Generic;
using System.Linq;
using core.exceptions;
using data.models.write;

namespace core.work
{
    public class LIFOTradeCalculator
    {
        public IEnumerable<Trade> Calculate(IEnumerable<BrokerageTransaction> transactions)
        {
            var transactions_by_security = transactions.GroupBy(x => new[] { x.SecurityId, x.AccountId }).ToList();

            return transactions_by_security.SelectMany(CalculateTradesForSingleSecurityInAccount).ToList();
        }

        //Assume we aren't shorting positions
        public IEnumerable<Trade> CalculateTradesForSingleSecurityInAccount(IEnumerable<BrokerageTransaction> transactions)
        {
            var ordered_transactions = transactions.OrderBy(x => x.TradeDate).ToList();

            var open_positions = new List<OpenPosition>();
            var trades = new List<Trade>();

            foreach (var transaction in ordered_transactions)
            {
                if (transaction.GrossAmount > 0)
                {
                    OpenPosition(transaction, open_positions);
                }
                else if (transaction.GrossAmount < 0)
                {
                    trades.AddRange(ClosePositions(transaction, open_positions));
                }
            }

            return trades;
        }

        void OpenPosition(BrokerageTransaction transaction, List<OpenPosition> open_positions)
        {
            open_positions.Add(new OpenPosition
                               {
                                   AquireDate = transaction.TradeDate,
                                   AssociatedTransactionId = transaction.Id,
                                   AccountId = transaction.AccountId,
                                   SecurityId = transaction.SecurityId,
                                   Shares = transaction.Shares,
                                   SharePrice = transaction.SharePrice,
                                   TotalCost = transaction.GrossAmount
                               });
        }

        IEnumerable<Trade> ClosePositions(BrokerageTransaction transaction, List<OpenPosition> open_positions)
        {
            var quantity = transaction.Shares;

            var trades = new List<Trade>();
            while (quantity > 0 && open_positions.Any())
            {
                var position = open_positions.First();

                if (quantity >= position.Shares)
                {
                    open_positions.Remove(position);
                    quantity -= position.Shares;
                    trades.Add(new Trade
                               {
                                   AquireDate = position.AquireDate,
                                   ClosingDate = transaction.TradeDate,
                                   ClosingTransactionId = transaction.Id,
                                   PositionId = position.AssociatedTransactionId,
                                   Quantity = position.Shares,
                                   SellPrice = transaction.SharePrice,
                                   ProfileAndLoss = (position.Shares * transaction.SharePrice) - (position.Shares * position.SharePrice)
                               });
                }
                else
                {
                    position.Shares -= quantity;
                    trades.Add(new Trade
                    {
                        AquireDate = position.AquireDate,
                        ClosingDate = transaction.TradeDate,
                        ClosingTransactionId = transaction.Id,
                        PositionId = position.AssociatedTransactionId,
                        Quantity = quantity,
                        SellPrice = transaction.SharePrice,
                        ProfileAndLoss = (quantity * transaction.SharePrice) - (quantity * position.SharePrice)
                    });
                    quantity = 0;
                }
            }

            if (quantity > 0)
                throw new SoldTooManySharesException(transaction);

            return trades;
        }
    }

    public class OpenPosition
    {
        public int Id { get; set; }
        public DateTime AquireDate { get; set; }
        public BrokerageTransaction AssociatedTransaction { get; set; }
        public int AssociatedTransactionId { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public Security Security { get; set; }
        public int? SecurityId { get; set; }
        public decimal SharePrice { get; set; }
        public decimal Shares { get; set; }
        public decimal TotalCost { get; set; }
    }
}