using System.Runtime.Remoting.Contexts;
using core.commands;
using core.queries;
using data.models.write;

namespace core
{
    public class SecurityDescriptionAssociator
    {
        Repository repository;
        AssociateTransactionsWithMissingSecuritiesCommand command;

        public SecurityDescriptionAssociator(Repository repository, AssociateTransactionsWithMissingSecuritiesCommand command)
        {
            this.repository = repository;
            this.command = command;
        }

        public virtual void Associate(int transaction_id, int security_id)
        {
            var transaction = repository.GetTransaction(transaction_id);
            var security = repository.GetSecurity(security_id);

            security.SecurityDescriptions.Add(new SecurityDescription { Security = security, Description = transaction.SecurityDescription });
            transaction.SecurityId = security.Id;

            command.Execute(security);
        }
    }
}