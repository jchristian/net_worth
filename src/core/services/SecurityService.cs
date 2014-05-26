using System.Data.SqlClient;
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
                SELECT      DISTINCT
                            s.*
                FROM        Securities s
                INNER JOIN  SecurityDescriptions sd
                        ON  s.Id = sd.Security_Id
                WHERE       sd.Description LIKE @0", new SqlParameter("@0", security_description.ToLowerInvariant())).SingleOrDefault();
        }
    }
}