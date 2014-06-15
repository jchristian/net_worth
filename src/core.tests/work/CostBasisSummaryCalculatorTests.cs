using System;
using System.Linq;
using core.tests.helpers;
using core.work;
using data.models.write;
using ExpectedObjects;
using FluentAssertions;
using Ploeh.AutoFixture;
using Xunit;

namespace core.tests.work
{
    public class CostBasisSummaryCalculatorTests
    {
        [Fact]
        public void when_calculating_a_lot()
        {
            var f = new Fixture();

            var transaction = new BrokerageTransaction
            {
                Security = new Security { Id = f.Create<int>(), Name = f.Create<string>(), Ticker = f.Create<string>() },
                TradeDate = f.Create<DateTime>(),
                Shares = f.Create<int>(),
                SharePrice = f.Create<decimal>(),
                GrossAmount = f.Create<decimal>()
            };
            var current_price = new SecurityPrice
                                {
                                    DateTime = f.Create<DateTime>(),
                                    Price = f.Create<decimal>()
                                };

            var actual = new CostBasisSummaryCalculator().CalculateLot(transaction, current_price);

            var expected = new
            {
                AquiredDate = transaction.TradeDate,
                Quantity = transaction.Shares,
                CostPerShare = transaction.SharePrice,
                TotalCost = transaction.GrossAmount,
                CurrentMarketValue = current_price.Price * transaction.Shares,
                CurrentMarketValueDate = current_price.DateTime,
                TotalGainLoss = (current_price.Price * transaction.Shares) - transaction.GrossAmount
            }.ToExpectedObject();
            expected.ShouldMatch(actual);
        }

        [Fact]
        public void when_calculating_a_lot_and_the_current_price_is_null()
        {
            var transaction = new BrokerageTransaction();
            var current_price = (SecurityPrice)null;

            var actual = new CostBasisSummaryCalculator().CalculateLot(transaction, current_price);

            var expected = new
            {
                CurrentMarketValue = (decimal?)null,
                CurrentMarketValueDate = (DateTime?)null,
                TotalGainLoss = (decimal?)null,
            }.ToExpectedObject();
            expected.ShouldMatch(actual);
        }

        [Fact]
        public void when_calculating_a_lot_and_there_are_short_term_capital_gains()
        {
            var f = new Fixture();

            var transaction = new BrokerageTransaction
            {
                Security = new Security { Id = f.Create<int>(), Name = f.Create<string>(), Ticker = f.Create<string>() },
                TradeDate = DateTime.Now.AddMonths(-6),
                Shares = f.Create<int>(),
                SharePrice = f.Create<decimal>(),
                GrossAmount = f.Create<decimal>()
            };
            var current_price = new SecurityPrice
            {
                DateTime = f.Create<DateTime>(),
                Price = f.Create<decimal>()
            };

            var actual = new CostBasisSummaryCalculator().CalculateLot(transaction, current_price);

            var expected = new
            {
                ShortTermCapitalGain = (transaction.Shares * current_price.Price) - transaction.GrossAmount,
                LongTermCapitalGain = (decimal?)0
            }.ToExpectedObject();
            expected.ShouldMatch(actual);
        }

        [Fact]
        public void when_calculating_a_lot_and_there_are_long_term_capital_gains()
        {
            var f = new Fixture();

            var transaction = new BrokerageTransaction
            {
                Security = new Security { Id = f.Create<int>(), Name = f.Create<string>(), Ticker = f.Create<string>() },
                TradeDate = DateTime.Now.AddMonths(-13),
                Shares = f.Create<int>(),
                SharePrice = f.Create<decimal>(),
                GrossAmount = f.Create<decimal>()
            };
            var current_price = new SecurityPrice
            {
                DateTime = f.Create<DateTime>(),
                Price = f.Create<decimal>()
            };

            var actual = new CostBasisSummaryCalculator().CalculateLot(transaction, current_price);

            var expected = new
            {
                ShortTermCapitalGain = (decimal?)0,
                LongTermCapitalGain = (transaction.Shares * current_price.Price) - transaction.GrossAmount
            }.ToExpectedObject();
            expected.ShouldMatch(actual);
        }

        [Fact]
        public void when_calculating_a_security_lot()
        {
            var f = new Fixture();

            var security = new Security { Id = f.Create<int>(), Name = f.Create<string>(), Ticker = f.Create<string>() };
            var transactions = Enumerable.Range(0, 10).Select(x => new BrokerageTransaction
            {
                Security = security,
                TradeDate = DateTime.Now.AddMonths(-2 * x),
                Shares = f.Create<int>(),
                SharePrice = f.Create<decimal>(),
                GrossAmount = f.Create<decimal>()
            }).ToList();
            var current_price = new SecurityPrice
            {
                DateTime = f.Create<DateTime>(),
                Price = f.Create<decimal>()
            };

            var actual = new CostBasisSummaryCalculator().CalculateSecurityLot(new GenericGrouping<Security, BrokerageTransaction>(security, transactions), current_price);

            var expected = new
            {
                SecurityId = security.Id,
                SecurityName = security.Name,
                SecurityTicker = security.Ticker,
                Quantity = transactions.Sum(x => x.Shares),
                TotalCost = transactions.Sum(x => x.GrossAmount),
                CurrentMarketValue = transactions.Sum(x => x.Shares * current_price.Price),
                TotalGainLoss = transactions.Sum(x => (x.Shares * current_price.Price) - x.GrossAmount)
            }.ToExpectedObject();
            expected.ShouldMatch(actual);
            actual.Lots.Count().Should().Be(10);
        }

        [Fact]
        public void when_calculating_the_cost_basis_summary()
        {
            var f = new Fixture();

            var first_security = new Security { Id = f.Create<int>(), Name = f.Create<string>(), Ticker = f.Create<string>() };
            var second_security = new Security { Id = f.Create<int>(), Name = f.Create<string>(), Ticker = f.Create<string>() };

            var first_transactions = Enumerable.Range(0, 10).Select(x => new BrokerageTransaction
            {
                Security = first_security,
                TradeDate = DateTime.Now.AddMonths(-2 * x),
                Shares = f.Create<int>(),
                SharePrice = f.Create<decimal>(),
                GrossAmount = f.Create<decimal>()
            }).ToList();
            var second_transactions = Enumerable.Range(0, 10).Select(x => new BrokerageTransaction
            {
                Security = second_security,
                TradeDate = DateTime.Now.AddMonths(-2 * x),
                Shares = f.Create<int>(),
                SharePrice = f.Create<decimal>(),
                GrossAmount = f.Create<decimal>()
            }).ToList();
            
            var first_current_price = new SecurityPrice
            {
                Security = first_security,
                DateTime = f.Create<DateTime>(),
                Price = f.Create<decimal>()
            };
            var second_current_price = new SecurityPrice
            {
                Security = second_security,
                DateTime = f.Create<DateTime>(),
                Price = f.Create<decimal>()
            };

            var actual = new CostBasisSummaryCalculator().Calculate(first_transactions.Concat(second_transactions).ToList(), new[] { first_current_price, second_current_price });

            var expected = new
            {
                TotalCost = first_transactions.Sum(x => x.GrossAmount) + second_transactions.Sum(x => x.GrossAmount),
                CurrentMarketValue = first_transactions.Sum(x => x.Shares * first_current_price.Price) + second_transactions.Sum(x => x.Shares * second_current_price.Price),
                TotalGainLoss = first_transactions.Sum(x => (x.Shares * first_current_price.Price) - x.GrossAmount) + second_transactions.Sum(x => (x.Shares * second_current_price.Price) - x.GrossAmount),
            }.ToExpectedObject();
            expected.ShouldMatch(actual);
            actual.SecurityLots.Count().Should().Be(2);
        }
    }
}