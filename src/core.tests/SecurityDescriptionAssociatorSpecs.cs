using System.Collections.Generic;
using System.Data.Entity;
using core.commands;
using core.queries;
using data.models.write;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.nsubstitue;
using FluentAssertions;
using Machine.Specifications;
using NSubstitute;

namespace core.tests
{
    public class SecurityDescriptionAssociatorSpecs
    {
        public abstract class concern : Observes<SecurityDescriptionAssociator> {}

        [Subject(typeof(SecurityDescriptionAssociator))]
        public class when_associating_a_security : concern
        {
            Establish c = () =>
            {
                transaction = new BrokerageTransaction { Id = 1, SecurityDescription = "Security Description"};
                security = new Security { Id = 2, SecurityDescriptions = fake.an<ICollection<SecurityDescription>>() };

                associate_transactions_with_missing_securities_command = depends.on<AssociateTransactionsWithMissingSecuritiesCommand>();
                var repository = depends.on<Repository>();

                repository.setup(x => x.GetTransaction(transaction.Id)).Return(transaction);
                repository.setup(x => x.GetSecurity(security.Id)).Return(security);
            };

            Because of = () =>
                sut.Associate(transaction.Id, security.Id);

            It should_add_the_security_description_to_the_security = () =>
                security.SecurityDescriptions.Received().Add(Arg.Is<SecurityDescription>(x => x.Description == transaction.SecurityDescription));

            It should_associate_the_security_to_the_transaction = () =>
                transaction.SecurityId.Should().Be(security.Id);

            It should_update_the_transactions_with_missing_securities = () =>
                associate_transactions_with_missing_securities_command.Received().Execute(security);
            
            static Security security;
            static BrokerageTransaction transaction;
            static AssociateTransactionsWithMissingSecuritiesCommand associate_transactions_with_missing_securities_command;
        }
    }
}
