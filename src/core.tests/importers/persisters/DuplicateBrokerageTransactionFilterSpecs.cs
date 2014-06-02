using System;
using System.Collections.Generic;
using core.importers.persisters;
using core.queries;
using core.tests.helpers;
using data.models.write;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.nsubstitue;
using Machine.Specifications;
using NSubstitute;

namespace core.tests.importers.persisters
{
    public class DuplicateBrokerageTransactionFilterSpecs
    {
        public abstract class concern : Observes<ICollectionPersister<BrokerageTransaction>,
                                            DuplicateBrokerageTransactionFilter> {}

        [Subject(typeof(DuplicateBrokerageTransactionFilter))]
        public class when_persisting_the_brokerage_transactions : concern
        {
            Establish c = () =>
            {
                duplicate_transaction = new BrokerageTransaction
                                        {
                                            Account = new Account { Id = 1 },
                                            TradeDate = new DateTime(2010, 1, 1),
                                            SecurityId = 2,
                                            SharePrice = 10,
                                            Shares = 20,
                                            TransactionType = TransactionType.Buy
                                        };
                var non_duplicate_transaction = new BrokerageTransaction
                                                {
                                                    Account = new Account { Id = 1 },
                                                    TradeDate = new DateTime(2010, 1, 1),
                                                    SecurityId = 3,
                                                    SharePrice = 10,
                                                    Shares = 20,
                                                    TransactionType = TransactionType.Buy
                                                };
                non_duplicate_transaction_to_persist = new BrokerageTransaction
                                                       {
                                                           Account = new Account { Id = 1 },
                                                           TradeDate = new DateTime(2010, 1, 1),
                                                           SecurityId = 4,
                                                           SharePrice = 10,
                                                           Shares = 20,
                                                           TransactionType = TransactionType.Buy
                                                       };

                persister = depends.on<BrokerageTransactionPersister>();
                var repository = depends.on<Repository>();

                repository.setup(x => x.GetAllTransactions()).Return(new[] { duplicate_transaction, non_duplicate_transaction });
            };

            Because of = () =>
                sut.Persist(new[] { duplicate_transaction, non_duplicate_transaction_to_persist });

            It should_filter_the_duplicate_transactions = () =>
                persister.Received().Persist(Arg.Is<IEnumerable<BrokerageTransaction>>(x => x.OnlyContains(new[] { non_duplicate_transaction_to_persist })));

            static BrokerageTransaction duplicate_transaction;
            static BrokerageTransaction non_duplicate_transaction_to_persist;
            static BrokerageTransactionPersister persister;
        }
    }
}
