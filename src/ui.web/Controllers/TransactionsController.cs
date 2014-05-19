using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using core.importers;
using data.models.contexts;

namespace ui.web.Controllers
{
    public class TransactionsController : Controller
    {
        readonly VanguardTransactionImporter file_importer;

        public TransactionsController(VanguardTransactionImporter file_importer)
        {
            this.file_importer = file_importer;
        }

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
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file];
                if (hpf.ContentLength == 0)
                    continue;
                var reader = new StreamReader(hpf.InputStream);
                file_importer.Import(reader);
                //var savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(hpf.FileName));
                //hpf.SaveAs(savedFileName);
            }

            return RedirectToAction("List");
        }
    }
}