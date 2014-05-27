﻿using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using core.importers;
using data.models.contexts;
using ui.web.Models.Transactions;

namespace ui.web.Controllers
{
    public class TransactionsController : Controller
    {
        VanguardTransactionImporter file_importer;
        DataContext context;

        public TransactionsController(VanguardTransactionImporter file_importer, DataContext context)
        {
            this.file_importer = file_importer;
            this.context = context;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var brokerage_transactions = context.BrokerageTransactions
                                                .Include(x => x.Account)
                                                .Include(x => x.Security)
                                                .ToList();
            return View(brokerage_transactions);
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
            }

            return RedirectToAction("List");
        }

        public ActionResult AddSecurityDescriptor(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var brokerage_transaction = context.BrokerageTransactions.Find(id);

            if (brokerage_transaction == null)
                return new HttpNotFoundResult();

            return View(new AssociateSecurityModel { Transaction = brokerage_transaction, Securities = context.Securities.ToList() });
        }

        [Route("Transactions/AssociateSecurity/{transaction_id}/{security_id}")]
        public ActionResult AssociateSecurity(int transaction_id, int security_id)
        {
            return new HttpNotFoundResult();
        }
    }
}