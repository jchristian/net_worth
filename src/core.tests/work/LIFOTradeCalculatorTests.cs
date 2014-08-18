using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using core.tests.helpers;
using core.work;
using data.models.write;
using Xunit;

namespace core.tests.work
{
    public class LIFOTradeCalculatorTests
    {
        [Fact]
        public void when_calculating_trades_and_there_are_no_selling_transactions()
        {
            var first_transaction = new BrokerageTransaction { Id = 1, SecurityId = 10, TradeDate = new DateTime(2010, 1, 1), Shares = 10, SharePrice = 100, GrossAmount = 1000 };
            var second_transaction = new BrokerageTransaction { Id = 2, SecurityId = 10, TradeDate = new DateTime(2010, 1, 2), Shares = 10, SharePrice = 150, GrossAmount = 1500 };
            var trades = new LIFOTradeCalculator().Calculate(new[] { first_transaction, second_transaction },
                                                             Enumerable.Empty<Lot>());

            Assert.Equal(Enumerable.Empty<Trade>(), trades, new PublicPropertyEqualityComparer<Trade>());
        }

        [Fact]
        public void when_calculating_trades_and_there_is_a_perfect_match()
        {
            var first_transaction = new BrokerageTransaction { Id = 1, SecurityId = 10, TradeDate = new DateTime(2010, 1, 1), Shares = 10, SharePrice = 100, GrossAmount = 1000 };
            var second_transaction = new BrokerageTransaction { Id = 2, SecurityId = 10, TradeDate = new DateTime(2010, 1, 2), Shares = 10, SharePrice = 150, GrossAmount = 1500 };
            var third_transaction = new BrokerageTransaction { Id = 3, SecurityId = 10, TradeDate = new DateTime(2010, 1, 5), Shares = -10, SharePrice = 200, GrossAmount = -2000 };
            var trades = new LIFOTradeCalculator().Calculate(new[] { first_transaction, second_transaction, third_transaction },
                                                             CreateLots(new[] { first_transaction, second_transaction })
                                                                 .Concat(new[] { new Lot { IsOpen = false } })
                                                                 .ToList());

            Assert.Equal(new[]
            {
                new Trade { PositionId = first_transaction.Id , AquireDate = first_transaction.TradeDate,
                            ClosingTransactionId = third_transaction.Id, ClosingDate = third_transaction.TradeDate,
                            Quantity = 10, SellPrice = third_transaction.SharePrice, ProfileAndLoss = 1000 }
            }, trades, new PublicPropertyEqualityComparer<Trade>());
        }

        [Fact]
        public void when_calculating_trades_and_the_sell_partially_closes_a_position()
        {
            var first_transaction = new BrokerageTransaction { Id = 1, SecurityId = 10, TradeDate = new DateTime(2010, 1, 1), Shares = 10, SharePrice = 100, GrossAmount = 1000 };
            var second_transaction = new BrokerageTransaction { Id = 2, SecurityId = 10, TradeDate = new DateTime(2010, 1, 2), Shares = 10, SharePrice = 150, GrossAmount = 1500 };
            var third_transaction = new BrokerageTransaction { Id = 3, SecurityId = 10, TradeDate = new DateTime(2010, 1, 5), Shares = -5, SharePrice = 200, GrossAmount = -1000 };
            var trades = new LIFOTradeCalculator().Calculate(new[] { third_transaction },
                                                             CreateLots(new[] { first_transaction, second_transaction }));

            Assert.Equal(new[]
            {
                new Trade { PositionId = first_transaction.Id , AquireDate = first_transaction.TradeDate,
                            ClosingTransactionId = third_transaction.Id, ClosingDate = third_transaction.TradeDate,
                            Quantity = 5, SellPrice = third_transaction.SharePrice, ProfileAndLoss = 500 }
            }, trades, new PublicPropertyEqualityComparer<Trade>());
        }

        [Fact]
        public void when_calculating_trades_and_the_sell_closes_one_position_and_partially_another()
        {
            var first_transaction = new BrokerageTransaction { Id = 1, SecurityId = 10, TradeDate = new DateTime(2010, 1, 1), Shares = 10, SharePrice = 100, GrossAmount = 1000 };
            var second_transaction = new BrokerageTransaction { Id = 2, SecurityId = 10, TradeDate = new DateTime(2010, 1, 2), Shares = 10, SharePrice = 150, GrossAmount = 1500 };
            var third_transaction = new BrokerageTransaction { Id = 3, SecurityId = 10, TradeDate = new DateTime(2010, 1, 5), Shares = -15, SharePrice = 200, GrossAmount = -1000 };

            var lots = CreateLots(new[] { first_transaction, second_transaction });
            var trades = new LIFOTradeCalculator().Calculate(new[] { third_transaction },
                                                             lots);

            Assert.Equal(new[]
            {
                new Trade { PositionId = first_transaction.Id , AquireDate = first_transaction.TradeDate,
                            ClosingTransactionId = third_transaction.Id, ClosingDate = third_transaction.TradeDate,
                            Quantity = 10, SellPrice = third_transaction.SharePrice, ProfileAndLoss = 1000 },
                new Trade { PositionId = second_transaction.Id , AquireDate = second_transaction.TradeDate,
                            ClosingTransactionId = third_transaction.Id, ClosingDate = third_transaction.TradeDate,
                            Quantity = 5, SellPrice = third_transaction.SharePrice, ProfileAndLoss = 250 },
            }, trades, new PublicPropertyEqualityComparer<Trade>());

            Assert.False(lots.First().IsOpen);
        }

        [Fact]
        public void when_calculating_trades_and_there_are_multiple_complicated_sells()
        {
            var first_transaction = new BrokerageTransaction { Id = 1, SecurityId = 10, TradeDate = new DateTime(2010, 1, 1), Shares = 10, SharePrice = 100, GrossAmount = 1000 };
            var second_transaction = new BrokerageTransaction { Id = 2, SecurityId = 10, TradeDate = new DateTime(2010, 1, 2), Shares = 10, SharePrice = 150, GrossAmount = 1500 };
            var third_transaction = new BrokerageTransaction { Id = 3, SecurityId = 10, TradeDate = new DateTime(2010, 1, 5), Shares = -15, SharePrice = 200, GrossAmount = -1000 };
            var fourth_transaction = new BrokerageTransaction { Id = 4, SecurityId = 10, TradeDate = new DateTime(2010, 1, 10), Shares = 10, SharePrice = 50, GrossAmount = 500 };
            var fifth_transaction = new BrokerageTransaction { Id = 5, SecurityId = 10, TradeDate = new DateTime(2010, 1, 11), Shares = -10, SharePrice = 100, GrossAmount = -1000 };
            var sixth_transaction = new BrokerageTransaction { Id = 6, SecurityId = 10, TradeDate = new DateTime(2010, 1, 12), Shares = -2, SharePrice = 10, GrossAmount = -20 };
            var seventh_transaction = new BrokerageTransaction { Id = 7, SecurityId = 10, TradeDate = new DateTime(2010, 1, 13), Shares = -3, SharePrice = 20, GrossAmount = -60 };
            var trades = new LIFOTradeCalculator().Calculate(new[] { third_transaction, fifth_transaction, sixth_transaction, seventh_transaction },
                                                             CreateLots(new[] { first_transaction, second_transaction, fourth_transaction }));

            Assert.Equal(new[]
            {
                new Trade { PositionId = first_transaction.Id , AquireDate = first_transaction.TradeDate,
                            ClosingTransactionId = third_transaction.Id, ClosingDate = third_transaction.TradeDate,
                            Quantity = 10, SellPrice = third_transaction.SharePrice, ProfileAndLoss = 1000 },
                new Trade { PositionId = second_transaction.Id , AquireDate = second_transaction.TradeDate,
                            ClosingTransactionId = third_transaction.Id, ClosingDate = third_transaction.TradeDate,
                            Quantity = 5, SellPrice = third_transaction.SharePrice, ProfileAndLoss = 250 },
                new Trade { PositionId = second_transaction.Id , AquireDate = second_transaction.TradeDate,
                            ClosingTransactionId = fifth_transaction.Id, ClosingDate = fifth_transaction.TradeDate,
                            Quantity = 5, SellPrice = fifth_transaction.SharePrice, ProfileAndLoss = -250 },
                new Trade { PositionId = fourth_transaction.Id , AquireDate = fourth_transaction.TradeDate,
                            ClosingTransactionId = fifth_transaction.Id, ClosingDate = fifth_transaction.TradeDate,
                            Quantity = 5, SellPrice = fifth_transaction.SharePrice, ProfileAndLoss = 250 },
                new Trade { PositionId = fourth_transaction.Id , AquireDate = fourth_transaction.TradeDate,
                            ClosingTransactionId = sixth_transaction.Id, ClosingDate = sixth_transaction.TradeDate,
                            Quantity = 2, SellPrice = sixth_transaction.SharePrice, ProfileAndLoss = -80 },
                new Trade { PositionId = fourth_transaction.Id , AquireDate = fourth_transaction.TradeDate,
                            ClosingTransactionId = seventh_transaction.Id, ClosingDate = seventh_transaction.TradeDate,
                            Quantity = 3, SellPrice = seventh_transaction.SharePrice, ProfileAndLoss = -90 },
            }, trades, new PublicPropertyEqualityComparer<Trade>());
        }

        IEnumerable<Lot> CreateLots(IEnumerable<BrokerageTransaction> transactions)
        {
            return transactions.Select(x => new Lot
                                            {
                                                Id = x.Id,
                                                BrokerageTransaction = x,
                                                IsOpen = true,
                                                RemainingShares = x.Shares,
                                                RemainingAmount = x.GrossAmount
                                            }).ToList();
        }
    }
}