using System.Collections.Generic;
using System.Linq;
using core.queries;
using data.models.write;

namespace core.importers.filters
{
    public class DuplicateBrokerageTransactionFilter
    {
        Repository repository;

        public DuplicateBrokerageTransactionFilter(Repository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<BrokerageTransaction> Filter(IEnumerable<BrokerageTransaction> items)
        {
            var current_transactions = repository.GetAllTransactions();
            return items.Where(x => !current_transactions.Any(y => x.AccountId == y.AccountId &&
                                                                   x.TradeDate == y.TradeDate &&
                                                                   x.SecurityId == y.SecurityId &&
                                                                   x.Shares == y.Shares &&
                                                                   x.SharePrice == y.SharePrice &&
                                                                   x.TransactionType == y.TransactionType)).ToArray();
            
        }
    }
}