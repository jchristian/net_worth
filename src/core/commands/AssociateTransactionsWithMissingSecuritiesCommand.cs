using System.Linq;
using data.models.contexts;

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

        public virtual void Execute(int security_id)
        {
            var descriptions = context.SecurityDescriptions.Where(x => x.SecurityId == security_id).ToList();
            foreach (var transaction in context.BrokerageTransactions)
            {
                if (descriptions.Any(x => x.Description.ToLowerInvariant() == transaction.SecurityDescription.ToLowerInvariant())
                    && transaction.SecurityId == null)
                    transaction.SecurityId = security_id;
            }

            context.SaveChanges();
        }
    }
}