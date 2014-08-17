using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using data.models.contexts;

namespace ui.web.Controllers
{
    public class TradesController : Controller
    {
        DataContext context;

        public TradesController(DataContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View(context.Lots
                               .Include(x => x.BrokerageTransaction)
                               .Include(x => x.BrokerageTransaction.Account)
                               .Include(x => x.BrokerageTransaction.Security)
                               .Include(x => x.Trades)
                               .ToList());
        }
    }
}