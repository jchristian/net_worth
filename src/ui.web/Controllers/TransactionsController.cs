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

        public ActionResult AssociateSecurity(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var brokerage_transaction = context.BrokerageTransactions.Find(id);

            if (brokerage_transaction == null)
                return new HttpNotFoundResult();

            return View(new AssociateSecurityModel {
                TransactionId = brokerage_transaction.Id,
                Transaction = brokerage_transaction,
                Securities = context.Securities.ToList() });
        }

        [HttpPost]
        public ActionResult AssociateSecurity([Bind(Include = "TransactionId, SelectedSecurityId")] AssociateSecurityModel model)
        {
            if(model.SelectedSecurityId == null)
                return RedirectToAction("Index");

            security_description_associator.Associate(model.TransactionId, model.SelectedSecurityId.Value);

            return RedirectToAction("Index");
        }

        public ActionResult AssociateTransactionType(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var brokerage_transaction = context.BrokerageTransactions.Find(id);
            if (brokerage_transaction == null)
                return new HttpNotFoundResult();

            return View(new AssociateTransactionTypeModel
                        {
                            TransactionId = brokerage_transaction.Id,
                            Transaction = brokerage_transaction,
                            TransactionTypes = Enum.GetValues(typeof(TransactionType)).OfType<TransactionType>().ToList()
                        });
        }

        [HttpPost]
        public ActionResult AssociateTransactionType([Bind(Include = "TransactionId, SelectedTransactionType")] AssociateTransactionTypeModel model)
        {
            transaction_type_associator.Associate(model.TransactionId, model.SelectedTransactionType);

            return RedirectToAction("Index");
        }
    }
}