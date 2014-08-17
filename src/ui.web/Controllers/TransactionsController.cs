using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using core.commands;
using core.importers;
using data.models.contexts;
using data.models.write;
using ui.web.Models.Transactions;

namespace ui.web.Controllers
{
    public class TransactionsController : Controller
    {
        VanguardTransactionImporter file_importer;
        AssociateSecurityCommand associate_security_command;
        AssociateTransactionTypeCommand associate_transaction_type_command;
        CreateLotsCommand create_lots_command;
        AutoAssignTradesCommand auto_assign_trades_command;
        DataContext context;

        public TransactionsController(VanguardTransactionImporter file_importer,
                                      AssociateSecurityCommand associate_security_command,
                                      AssociateTransactionTypeCommand associate_transaction_type_command,
                                      CreateLotsCommand create_lots_command,
                                      AutoAssignTradesCommand auto_assign_trades_command,
                                      DataContext context)
        {
            this.file_importer = file_importer;
            this.associate_security_command = associate_security_command;
            this.associate_transaction_type_command = associate_transaction_type_command;
            this.create_lots_command = create_lots_command;
            this.auto_assign_trades_command = auto_assign_trades_command;
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

            create_lots_command.Execute();
            auto_assign_trades_command.Execute();

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

            associate_security_command.Execute(model.TransactionId, model.SelectedSecurityId.Value);

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
                            TransactionTypes = Enum.GetValues(typeof(TransactionType)).OfType<TransactionType>().ToList(),
                            SelectedMatch = new TransactionMatch { Description = brokerage_transaction.TransactionDescription }
                        });
        }

        [HttpPost]
        public ActionResult AssociateTransactionType([Bind(Include = "TransactionId, SelectedMatch")] AssociateTransactionTypeModel model)
        {
            associate_transaction_type_command.Execute(model.TransactionId, model.SelectedMatch);

            return RedirectToAction("Index");
        }
    }
}