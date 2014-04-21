using core.importers.parsers.readers;
using developwithpassion.specifications.nsubstitue;
using FluentAssertions;
using Machine.Specifications;

namespace core.tests.importers.parsers.readers
{
    public class VanguardTransactionFileReaderFactorySpecs
    {
        public abstract class concern : Observes<VanguardTransactionFileReaderFactory> { }

        [Subject(typeof(VanguardTransactionFileReaderFactory))]
        public class when_reading_the_file : concern
        {
            public class with_transactions
            {
                Because of = () =>
                    reader_contents = sut.CreateTransactionReader(@".\importers\parsers\readers\vanguard_transaction_file_reader_factory_specs_test_file (with transactions).txt").ReadToEnd();

                It should_read_the_correct_lines = () =>
                {
                    reader_contents.Should().Be(@"Account Number,Trade Date,Process Date,Transaction Type,Transaction Description,Investment Name,Share Price,Shares,Gross Amount,Net Amount,
88059530569,07/31/2012,07/31/2012,Distribution,INCOME DIVIDEND,Prime Money Mkt Fund,1.0,0.18,0.18,0.18,
88059530569,07/31/2012,07/31/2012,Distribution,INCOME DIVIDEND,Prime Money Mkt Fund,1.0,0.18,0.18,0.18,
88059530569,07/31/2012,07/31/2012,Distribution,INCOME DIVIDEND,Prime Money Mkt Fund,1.0,0.18,0.18,0.18,
88059530569,07/31/2012,07/31/2012,Distribution,INCOME DIVIDEND,Prime Money Mkt Fund,1.0,0.18,0.18,0.18,");
                };

                static string reader_contents;
            }

            public class without_transactions
            {
                Because of = () =>
                    reader_contents = sut.CreateTransactionReader(@".\importers\parsers\readers\vanguard_transaction_file_reader_factory_specs_test_file (without transactions).txt").ReadToEnd();

                It should_read_the_correct_lines = () =>
                {
                    reader_contents.Should().Be(@"Account Number,Trade Date,Process Date,Transaction Type,Transaction Description,Investment Name,Share Price,Shares,Gross Amount,Net Amount,");
                };

                static string reader_contents;
            }
        }
    }
}
