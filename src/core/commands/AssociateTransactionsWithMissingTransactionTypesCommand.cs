using core.services;
using data.models.contexts;
using data.models.write;

namespace core.commands
{
    public class AssociateTransactionsWithMissingTransactionTypesCommand
    {
        TransactionTypeService service;
        DataContext context;

        protected AssociateTransactionsWithMissingTransactionTypesCommand() {}
        public AssociateTransactionsWithMissingTransactionTypesCommand(DataContext context, TransactionTypeService service)
        {
            this.service = service;
            this.context = context;
        }

        public virtual void Execute(TransactionMatch match)
        {
            foreach (var transaction in context.BrokerageTransactions)
            {
                if (service.Matches(transaction.TransactionDescription, match) && transaction.TransactionType == TransactionType.Missing)
                    transaction.TransactionType = match.TransactionType;
            }

            context.SaveChanges();
        }
    }
}