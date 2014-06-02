using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
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

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult List()
        {
            return View(context.Securities.ToList());
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Ticker, Description")]Security security)
        {
            if (!context.Securities.Any(x => x.Ticker == security.Ticker))
            {
                context.Securities.Add(security);
                context.SaveChanges();
            }

            return RedirectToAction("Detail", new { Id = security.Id });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var security = context.Securities.Find(id);

            if (security == null)
                return new HttpNotFoundResult();

            return View(security);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, Ticker, Description")]Security security)
        {
            context.Securities.AddOrUpdate(security);
            context.SaveChanges();

            return RedirectToAction("Detail", new { Id = security.Id });
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var security = context.Securities.Find(id);

            if (security == null)
                return new HttpNotFoundResult();

            return View(security);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var security = context.Securities.Include(x => x.BrokerageTransactions).SingleOrDefault(x => x.Id == id);

                if (security != null)
                {
                    context.Securities.Remove(security);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}