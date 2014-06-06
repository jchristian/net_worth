using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using core;
using core.importers;
using data.models.contexts;
using data.models.write;
using ui.web.Models.Transactions;

namespace ui.web.Controllers
{
    public class TransactionsController : Controller
    {
        VanguardTransactionImporter file_importer;
        SecurityDescriptionAssociator security_description_associator;
        TransactionTypeAssociator transaction_type_associator;
        DataContext context;

        public TransactionsController(VanguardTransactionImporter file_importer,
                                      SecurityDescriptionAssociator security_description_associator,
            TransactionTypeAssociator transaction_type_associator,
                                      DataContext context)
        {
            this.file_importer = file_importer;
            this.security_description_associator = security_description_associator;
            this.transaction_type_associator = transaction_type_associator;
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

            return RedirectToAction("Index");
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
            security_description_associator.Associate(transaction_id, security_id);

            return RedirectToAction("Index");
        }

        public ActionResult AddTransactionTypeDescriptor(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var brokerage_transaction = context.BrokerageTransactions.Find(id);

            if (brokerage_transaction == null)
                return new HttpNotFoundResult();

            return View(new AssociateTransactionTypeModel { Transaction = brokerage_transaction, TransactionTypes = Enum.GetValues(typeof(TransactionType)).OfType<TransactionType>().ToList() });
        }

        [Route("Transactions/AssociateTransactionType/{transaction_id}/{transaction_type}")]
        public ActionResult AssociateTransactionType(int transaction_id, TransactionType transaction_type)
        {
            transaction_type_associator.Associate(transaction_id, transaction_type);

            return RedirectToAction("Index");
        }
    }
}