using System.Collections.Generic;
using data.models.contexts;
using data.models.write;
using MoreLinq;

namespace core.importers.persisters
{
    public class BrokerageTransactionPersister
    {
        DataContext context;

        protected BrokerageTransactionPersister() {}
        public BrokerageTransactionPersister(DataContext context)
        {
            this.context = context;
        }

        public virtual void Persist(IEnumerable<BrokerageTransaction> transactions)
        {
            transactions.ForEach(transaction => context.BrokerageTransactions.Add(transaction));
            context.SaveChanges();
        }
    }
}