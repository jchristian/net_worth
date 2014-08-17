using System.Linq;
using data.models.contexts;
using data.models.write;
using MoreLinq;

namespace core.commands
{
    public class CreateLotsCommand
    {
        DataContext data_context;

        public CreateLotsCommand(DataContext data_context)
        {
            this.data_context = data_context;
        }

        public void Execute()
        {
            var brokerage_transactions = data_context.BrokerageTransactions.ToList();
            var brokerage_transactions_with_lots = data_context.Lots.ToList().Select(x => x.BrokerageTransaction.Id).ToHashSet();

            var brokerage_transactions_without_lots = brokerage_transactions.Where(bt => !brokerage_transactions_with_lots.Contains(bt.Id) && bt.GrossAmount > 0).ToList();

            data_context.Lots.AddRange(brokerage_transactions_without_lots.Select(bt => new Lot
                                                                                        {
                                                                                            BrokerageTransaction = bt,
                                                                                            IsOpen = true,
                                                                                            RemainingShares = bt.Shares,
                                                                                            RemainingAmount = bt.GrossAmount
                                                                                        }).ToList());
            data_context.SaveChanges();
        }
    }
}