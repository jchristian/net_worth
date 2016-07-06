using System.Collections.Generic;
using core.events;
using core.importers.persisters;
using data.models.write;

namespace core.commands
{
    public class AddBrokerageTransactionsCommand
    {
        BrokerageTransactionPersister persister;
        EventBus event_bus;

        public AddBrokerageTransactionsCommand(BrokerageTransactionPersister persister,
                                               EventBus event_bus)
        {
            this.persister = persister;
            this.event_bus = event_bus;
        }

        public virtual void Execute(IEnumerable<BrokerageTransaction> brokerage_transactions)
        {
            persister.Persist(brokerage_transactions);
            event_bus.Raise(new BrokerageTransactionsPersisted(brokerage_transactions));
        }
    }
}