using System.Data.SqlClient;
using System.Linq;
using core.extensions;
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

        public virtual int? Find(string security_description)
        {
            var descriptions = context.SecurityDescriptions.ToList();

            return descriptions.FirstOrDefault(x => x.Description.ToLowerInvariant() == security_description.ToLowerInvariant()).IfNotNull(x => x.SecurityId);
        }
    }
}