using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using data.models.contexts;

namespace api.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class LotsController : ApiController
    {
        DataContext context;

        public LotsController(DataContext context)
        {
            this.context = context;
        }

        [Route("api/lots")]
        [HttpGet]
        public IEnumerable<object> GetAll()
        {
            return context.Lots
                          .ToList().Select(x => new
                                                {
                                                    x.Id,
                                                    x.BrokerageTransactionId,
                                                    x.IsOpen,
                                                    x.RemainingAmount,
                                                    x.RemainingShares
                                                });
        }
    }
}