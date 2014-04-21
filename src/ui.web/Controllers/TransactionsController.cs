using System.Linq;
using System.Web.Mvc;
using data.models.contexts;

namespace ui.web.Controllers
{
    public class TransactionsController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            using (var context = new DataContext())
            {
                return View(context.BrokerageTransactions.ToArray());
            }
        }

        public ActionResult Import()
        {
            return RedirectToAction("List");
        }
    }
}