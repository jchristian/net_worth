using System.Collections.Generic;
using System.Linq;
using core.importers.persisters;
using core.tests.helpers;
using data.models.contexts;
using data.models.write;
using developwithpassion.specifications.nsubstitue;
using FluentAssertions;
using Machine.Specifications;
using System.Data.Entity;

namespace core.tests.importers.persisters
{
    public class BrokerageTransactionPersisterSpecs
    {
        public abstract class concern : Observes<BrokerageTransactionPersister> {}

        [Subject(typeof(BrokerageTransactionPersister))]
        public class when_persisting_transactions : concern
        {
            Establish c = () =>
            {
                account = depends.on(new Account());

                using (var context = new DataContext())
                {
                    context.Accounts.Add(account);

                    var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)context).ObjectContext;
                    objCtx.ExecuteStoreCommand("TRUNCATE TABLE BrokerageTransactions");

                    context.SaveChanges();
                }

                var security = new Security();
                transactions = Enumerable.Range(1, 3).Select(x => new BrokerageTransaction { ProcessDate = DummyDate.Get(), TradeDate = DummyDate.Get(), Security = security }).ToArray();
            };

            Because of = () =>
                sut.Persist(transactions);

            It should_add_the_transactions_to_the_context = () =>
            {
                using (var context = new DataContext())
                {
                    var transactions_from_db = context.BrokerageTransactions.Include(x => x.Account).ToList();
                    transactions_from_db.ForEach(x => x.Account.Id.Should().Be(account.Id));
                    transactions_from_db.Count.Should().Be(transactions.Count());
                }
            };

            static IEnumerable<BrokerageTransaction> transactions;
            static DataContext context;
            static Account account;
        }
    }
}
