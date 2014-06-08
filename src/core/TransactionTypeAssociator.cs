using core.commands;
using data.models.contexts;
using data.models.write;

namespace core
{
    public class TransactionTypeAssociator
    {
        DataContext context;
        AssociateTransactionsWithMissingTransactionTypesCommand command;

        public TransactionTypeAssociator(DataContext context, AssociateTransactionsWithMissingTransactionTypesCommand command)
        {
            this.context = context;
            this.command = command;
        }

        public virtual void Associate(int transaction_id, TransactionType transaction_type)
        {
            var transaction = context.BrokerageTransactions.Find(transaction_id);
            if (transaction == null)
                return;

            context.TransactionDescriptions.Add(new TransactionDescription { Description = transaction.TransactionDescription, TransactionTypeId = (int)transaction_type });
            transaction.TransactionType = transaction_type;

            context.SaveChanges();

            command.Execute(transaction_type);
        }
    }
}