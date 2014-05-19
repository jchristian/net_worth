using System.Collections.Generic;
using data.models.contexts;
using data.models.write;
using MoreLinq;

namespace core.importers.persisters
{
    public class BrokerageTransactionPersister : ICollectionPersister<BrokerageTransaction>
    {
        public void Persist(IEnumerable<BrokerageTransaction> transactions)
        {
            using (var context = new DataContext())
            {
                transactions.ForEach(transaction =>
                {
                    context.BrokerageTransactions.Add(transaction);
                });

                context.SaveChanges();
            }
        }
    }
}