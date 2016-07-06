using System.Collections.Generic;
using data.models.write;

namespace core.events
{
    public class BrokerageTransactionsPersisted
    {
        public IEnumerable<BrokerageTransaction> Transactions { get; }

        public BrokerageTransactionsPersisted(IEnumerable<BrokerageTransaction> transactions)
        {
            Transactions = transactions;
        }
    }
}