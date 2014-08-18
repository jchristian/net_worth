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

        public virtual TransactionType Find(string transaction_description)
        {
            var matches = context.TransactionMatches.ToList();
            var match = matches.FirstOrDefault(x => Matches(transaction_description, x)).IfNotNull(x => x.TransactionType);
            return match ?? TransactionType.Missing;
        }

        public virtual TransactionType Find(string transaction_type, string transaction_description)
        {
            var matches = context.TransactionMatches.ToList();
            var type_match = matches.FirstOrDefault(x => Matches(transaction_type, x)).IfNotNull(x => x.TransactionType);
            var description_match = matches.FirstOrDefault(x => Matches(transaction_description, x)).IfNotNull(x => x.TransactionType);

            return type_match ?? description_match ?? TransactionType.Missing;
        }

        public virtual bool Matches(string transaction_description, TransactionMatch match)
        {
            if(match.TransactionMatchType == TransactionMatchType.ExactMatch)
                return match.Description.ToLowerInvariant() == transaction_description.ToLowerInvariant();
            if(match.TransactionMatchType == TransactionMatchType.ContainsMatch)
                return transaction_description.ToLowerInvariant().Contains(match.ContainsMatchString.ToLowerInvariant());
            return false;
        }
    }
}