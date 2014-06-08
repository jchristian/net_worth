using System.Collections.Generic;
using System.Linq;
using core.queries;
using data.models.write;

namespace core.importers.persisters
{
    public class DuplicateBrokerageTransactionFilter : ICollectionPersister<BrokerageTransaction>
    {
        BrokerageTransactionPersister persister;
        Repository repository;

        public DuplicateBrokerageTransactionFilter(BrokerageTransactionPersister persister,
                                                   Repository repository)
        {
            this.persister = persister;
            this.repository = repository;
        }

        public void Persist(IEnumerable<BrokerageTransaction> items)
        {
            var current_transactions = repository.GetAllTransactions();
            var filtered_transactions = items.Where(x => !current_transactions.Any(y => x.AccountId == y.AccountId &&
                                                                                        x.TradeDate == y.TradeDate &&
                                                                                        x.SecurityId == y.SecurityId &&
                                                                                        x.Shares == y.Shares &&
                                                                                        x.SharePrice == y.SharePrice &&
                                                                                        x.TransactionType == y.TransactionType)).ToArray();
            persister.Persist(filtered_transactions);
        }
    }
}