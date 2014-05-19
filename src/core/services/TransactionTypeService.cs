using System.Data.SqlClient;
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

        public virtual TransactionType Find(string descriptor)
        {
            var db_sql_query = context.TransactionDescriptions.SqlQuery(@"
                SELECT      t.*
                FROM        TransactionDescriptions t
                WHERE       t.Description LIKE @0", new SqlParameter("@0", descriptor.ToLowerInvariant())).ToList();
            return (TransactionType)(db_sql_query.SingleOrDefault().IfNotNull(x => x.TransactionTypeId)
                                     ?? (int?)TransactionType.Missing).Value;
        }
    }
}