using System.Collections.Generic;
using System.Linq;
using data.models.contexts;
using data.models.write;

namespace core.queries
{
    public class Repository
    {
        DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public virtual IEnumerable<BrokerageTransaction> GetAllTransactions()
        {
            return context.BrokerageTransactions.ToList();
        }
    }
}