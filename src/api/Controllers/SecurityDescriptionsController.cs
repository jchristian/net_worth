using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using api.Helpers;
using data.models.contexts;

namespace api.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class SecurityDescriptionsController : ApiController
    {
        DataContext context;

        public SecurityDescriptionsController(DataContext context)
        {
            this.context = context;
        }

        [Route("api/security/{securityId}/descriptions")]
        [HttpGet]
        public IEnumerable<object> GetAllForSecurity(int securityId)
        {
            return context.SecurityDescriptions
                          .Where(x => x.SecurityId == securityId)
                          .ToList();
        }
    }
}
