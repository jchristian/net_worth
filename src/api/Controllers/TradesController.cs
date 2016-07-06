using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using data.models.contexts;

namespace api.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class TradesController : ApiController
    {
        private DataContext context;

        public TradesController(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<object> GetAll()
        {
            return context.Trades
                          .ToList()
                          .Select(x => new
                          {
                              x.Id,
                              x.AquireDate,
                              x.ClosingDate,
                              x.ClosingTransactionId,
                              x.PositionId,
                              x.ProfileAndLoss,
                              x.Quantity,
                              x.SellPrice
                          });
        }
    }
}