using System.Linq;
using data.models.contexts;
using data.models.write;

namespace core.commands
{
    public class AssociateTransactionsWithMissingSecuritiesCommand
    {
        DataContext context;

        protected AssociateTransactionsWithMissingSecuritiesCommand() {}
        public AssociateTransactionsWithMissingSecuritiesCommand(DataContext context)
        {
            this.context = context;
        }

        public virtual void Execute(Security security)
        {
            foreach (var transaction in context.BrokerageTransactions)
            {
                if (security.SecurityDescriptions.Any(x => x.Description.ToLowerInvariant() == transaction.SecurityDescription.ToLowerInvariant())
                    && transaction.SecurityId == null)
                    transaction.SecurityId = security.Id;
            }

            context.SaveChanges();
        }
    }
}