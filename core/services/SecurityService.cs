using System.Linq;
using data.models.contexts;
using data.models.write;

namespace core.services
{
    public class SecurityService
    {
        DataContext context;

        protected SecurityService() {}
        public SecurityService(DataContext context)
        {
            this.context = context;
        }

        public virtual Security Find(string security_description)
        {
            return context.Securities.SqlQuery(@"
                SELECT      s.*
                FROM        Security s
                INNER JOIN  SecurityDescription sd
                        ON  s.Id = sd.SecurityId
                WHERE       sd.SecurityDescription LIKE @0", security_description.ToLowerInvariant()).Single();
        }
    }
}