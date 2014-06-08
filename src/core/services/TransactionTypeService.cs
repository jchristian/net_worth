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
            var descriptions = context.TransactionDescriptions.ToList();

            return (TransactionType)(descriptions.FirstOrDefault(x => x.Description.ToLowerInvariant() == transaction_description.ToLowerInvariant())
                                                 .IfNotNull(x => x.TransactionTypeId)
                                     ?? (int)TransactionType.Missing);
        }
    }
}