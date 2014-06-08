using System.Linq;
using core.extensions;
using data.models.contexts;
using data.models.write;

namespace core.services
{
    public class TransactionTypeService
    {
        DataContext context;

        protected TransactionTypeService() {}
        public TransactionTypeService(DataContext context)
        {
            this.context = context;
        }

        public virtual TransactionType Matches(string transaction_description)
        {
            var matches = context.TransactionMatches.ToList();

            return matches.FirstOrDefault(x => Matches(transaction_description, x)).IfNotNull(x => x.TransactionType) ?? TransactionType.Missing;
        }

        public virtual bool Matches(string transaction_description, TransactionMatch match)
        {
            return match.Description.ToLowerInvariant() == transaction_description.ToLowerInvariant();
        }
    }
}