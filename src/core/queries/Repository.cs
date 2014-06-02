using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using data.models.contexts;
using data.models.write;

namespace core.queries
{
    public class Repository
    {
        DataContext context;

        protected Repository() {}
        public Repository(DataContext context)
        {
            this.context = context;
        }

        public virtual IEnumerable<BrokerageTransaction> GetAllTransactions()
        {
            return context.BrokerageTransactions.ToList();
        }

        public virtual BrokerageTransaction GetTransaction(int id)
        {
            return context.BrokerageTransactions
                .Include(x => x.Security)
                .SingleOrDefault(x => x.Id == id);
        }

        public virtual Security GetSecurity(int id)
        {
            return context.Securities
                .Include(x => x.SecurityDescriptions)
                .SingleOrDefault(x => x.Id == id);
        }
    }
}