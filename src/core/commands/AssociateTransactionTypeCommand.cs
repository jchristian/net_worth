using data.models.contexts;
using data.models.write;

namespace core.commands
{
    public class AssociateTransactionTypeCommand
    {
        DataContext context;
        AssociateTransactionsWithMissingTransactionTypesCommand command;

        public AssociateTransactionTypeCommand(DataContext context, AssociateTransactionsWithMissingTransactionTypesCommand command)
        {
            this.context = context;
            this.command = command;
        }

        public virtual void Execute(int transaction_id, TransactionMatch transaction_match)
        {
            var transaction = context.BrokerageTransactions.Find(transaction_id);
            if (transaction == null)
                return;

            context.TransactionMatches.Add(transaction_match);
            transaction.TransactionType = transaction_match.TransactionType;

            context.SaveChanges();

            command.Execute(transaction_match);
        }
    }
}