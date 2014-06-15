using System;
using core.work;
using data.models.write;
using ExpectedObjects;
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
    }
}