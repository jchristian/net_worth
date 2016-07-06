using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using api.Helpers;
using data.models.contexts;
using data.models.write;

namespace api.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class SecuritiesController : ApiController
    {
        DataContext context;

        public SecuritiesController(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<object> GetAll()
        {
            var securities = context.Securities
                                    .ToList()
                                    .Select(x => new
                                                 {
                                                     x.Id,
                                                     x.Name,
                                                     x.Ticker
                                                 });
            return securities;
        }
    }
}