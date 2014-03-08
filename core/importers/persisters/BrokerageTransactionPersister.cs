using System.Collections.Generic;
using data.models.contexts;
using data.models.write;
using MoreLinq;

namespace core.importers.persisters
{
    public class BrokerageTransactionPersister : ICollectionPersister<BrokerageTransaction>
    {
        Account account;

        public BrokerageTransactionPersister(Account account)
        {
            this.account = account;
        }

        public void Persist(IEnumerable<BrokerageTransaction> transactions)
        {
            using (var context = new DataContext())
            {
                transactions.ForEach(transaction =>
                {
                    transaction.Account = account;
                    context.BrokerageTransactions.Add(transaction);
                });

                context.SaveChanges();
            }
        }
    }
}