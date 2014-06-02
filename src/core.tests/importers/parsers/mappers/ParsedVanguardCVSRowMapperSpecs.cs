using System;
using System.Dynamic;
using core.importers.parsers.mappers;
using core.services;
using data.models.write;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.nsubstitue;
//using ExpectedObjects;
using ExpectedObjects;
using Machine.Specifications;

namespace core.tests.importers.parsers.mappers
{
    public class ParsedVanguardCVSRowMapperSpecs
    {
        public abstract class concern : Observes<ParsedVanguardCVSRowMapper> {}

        [Subject(typeof(ParsedVanguardCVSRowMapper))]
        public class when_mapping_a_row : concern
        {
            Establish c = () =>
            {
                prime_money_market_security = new Security();
                account = new Account();

                var security_mapper = depends.on<SecurityService>();
                var account_mapper = depends.on<AccountService>();
                var transaction_type_service = depends.on<TransactionTypeService>();

                row = new ExpandoObject();
                row.AccountNumber = "12345";
                row.TradeDate = "07/05/2012";
                row.ProcessDate = "07/06/2012";
                row.TransactionType = "Buy";
                row.TransactionDescription = "Description of the buy";
                row.InvestmentName = "Prime Money Mkt Fund";
                row.SharePrice = "2.00";
                row.Shares = "1000";
                row.GrossAmount = "2000";
                row.NetAmount = "1999";

                security_mapper.setup(x => x.Find("Prime Money Mkt Fund")).Return(prime_money_market_security);
                account_mapper.setup(x => x.Find("12345")).Return(account);
                transaction_type_service.setup(x => x.Find("Buy")).Return(TransactionType.Buy);
            };

            Because of = () =>
                actual = sut.Map(row);

            It should_map_all_the_properties_correctly = () =>
                new
                {
                    AccountId = account.Id,
                    TradeDate = new DateTime(2012, 7, 5),
                    ProcessDate = new DateTime(2012, 7, 6),
                    TransactionType = TransactionType.Buy,
                    TransactionDescription = "Description of the buy",
                    SecurityId = prime_money_market_security.Id,
                    SharePrice = 2m,
                    Shares = 1000m,
                    GrossAmount = 2000m,
                    NetAmount = 1999m,
                }.ToExpectedObject().ShouldMatch(actual);

            static Security prime_money_market_security;
            static Account account;
            static BrokerageTransaction actual;
            static dynamic row;
        }
    }
}
