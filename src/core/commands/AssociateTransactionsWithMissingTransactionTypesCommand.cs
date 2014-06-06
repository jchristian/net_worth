using System.Linq;
using data.models.contexts;
using data.models.write;

namespace core.commands
{
    public class AssociateTransactionsWithMissingTransactionTypesCommand
    {
        DataContext context;

        protected AssociateTransactionsWithMissingTransactionTypesCommand() {}
        public AssociateTransactionsWithMissingTransactionTypesCommand(DataContext context)
        {
            this.context = context;
        }

        public virtual void Execute(TransactionType transaction_type)
        {
            var transaction_descriptions = context.TransactionDescriptions.Where(x => x.TransactionTypeId == (int)transaction_type).ToList();
            foreach (var transaction in context.BrokerageTransactions)
            {
                if (transaction_descriptions.Any(x => x.Description.ToLowerInvariant() == transaction.TransactionDescription.ToLowerInvariant())
                    && transaction.TransactionType == TransactionType.Missing)
                    transaction.TransactionType = transaction_type;
            }

            context.SaveChanges();
        }
    }
}