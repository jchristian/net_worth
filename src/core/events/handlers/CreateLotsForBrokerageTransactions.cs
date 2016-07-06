using core.commands;

namespace core.events.handlers
{
    public class CreateLotsForBrokerageTransactions : IHandle<BrokerageTransactionsPersisted>
    {
        CreateLotsCommand create_lots_command;

        protected CreateLotsForBrokerageTransactions() {}
        public CreateLotsForBrokerageTransactions(CreateLotsCommand create_lots_command)
        {
            this.create_lots_command = create_lots_command;
        }

        public virtual void Handle(BrokerageTransactionsPersisted @event)
        {
            create_lots_command.Execute(@event.Transactions);
        }
    }
}