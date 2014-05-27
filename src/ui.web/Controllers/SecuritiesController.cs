using System.Linq;
using System.Web.Mvc;
using data.models.contexts;
using data.models.write;

namespace ui.web.Controllers
{
    public class SecuritiesController : Controller
    {
        DataContext context;

        public SecuritiesController(DataContext context)
        {
            this.context = context;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Ticker, Description")]Security security)
        {
            if (!context.Securities.Any(x => x.Ticker == security.Ticker))
            {
                context.Securities.Add(security);
                context.SaveChanges();
            }

            return RedirectToAction("List", "Transactions");
        }
    }
}