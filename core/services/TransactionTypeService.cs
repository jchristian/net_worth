using System.Linq;
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

        public virtual TransactionType Find(string descriptor)
        {
            return (TransactionType)context.TransactionDescriptions.SqlQuery(@"
                SELECT      s.*
                FROM        Security s
                INNER JOIN  SecurityDescription sd
                        ON  s.Id = sd.SecurityId
                WHERE       sd.SecurityDescription LIKE @0", descriptor.ToLowerInvariant()).Single().TransactionTypeId;
        }
    }
}