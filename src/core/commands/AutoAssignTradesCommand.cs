using System.Linq;
using core.work;
using data.models.contexts;
using MoreLinq;

namespace core.commands
{
    public class AutoAssignTradesCommand
    {
        DataContext data_context;
        LIFOTradeCalculator trade_calculator;

        public AutoAssignTradesCommand(DataContext data_context, LIFOTradeCalculator trade_calculator)
        {
            this.data_context = data_context;
            this.trade_calculator = trade_calculator;
        }

        public void Execute()
        {
            var traded_transactions = data_context.Trades.ToList().Select(x => x.ClosingTransactionId).ToList().ToHashSet();
            var transactions = data_context.BrokerageTransactions.ToList().Where(x => !traded_transactions.Contains(x.Id)).ToList();
            var lots = data_context.Lots.ToList();

            data_context.Trades.AddRange(trade_calculator.Calculate(transactions, lots));
            data_context.SaveChanges();
        }
    }
}