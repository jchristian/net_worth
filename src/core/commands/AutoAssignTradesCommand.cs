using System.Linq;
using core.work;
using data.models.contexts;

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
            //var transactions = data_context.BrokerageTransactions.ToList();
        }
    }
}