using data.models.contexts;
using data.models.write;

namespace core.commands
{
    public class AssociateSecurityCommand
    {
        DataContext context;
        AssociateTransactionsWithMissingSecuritiesCommand command;

        public AssociateSecurityCommand(DataContext context, AssociateTransactionsWithMissingSecuritiesCommand command)
        {
            this.context = context;
            this.command = command;
        }

        public virtual void Execute(int transaction_id, int security_id)
        {
            var transaction = context.BrokerageTransactions.Find(transaction_id);
            if (transaction == null)
                return;

            transaction.SecurityId = security_id;
            context.SecurityDescriptions.Add(new SecurityDescription { SecurityId = security_id, Description = transaction.SecurityDescription });
            context.SaveChanges();

            command.Execute(security_id);
        }
    }
}