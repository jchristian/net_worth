using System.Collections.Generic;
using System.Linq;
using data.models.contexts;
using data.models.write;

namespace core.commands
{
    public class CreateLotsCommand
    {
        DataContext data_context;

        public CreateLotsCommand(DataContext data_context)
        {
            this.data_context = data_context;
        }

        public void Execute(IEnumerable<BrokerageTransaction> brokerage_transactions)
        {
            var brokerage_transactions_without_lots = brokerage_transactions.Where(bt => bt.GrossAmount > 0).ToList();

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