using System.Collections.Generic;
using System.IO;
using core.importers.parsers;
using core.importers.parsers.mappers;
using core.importers.parsers.readers;
using data.models.write;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.nsubstitue;
using FluentAssertions;
using Machine.Specifications;

namespace core.tests.importers.parsers
{
    public class VanguardTransactionParserSpecs
    {
        public abstract class concern : Observes<IFileParser<BrokerageTransaction>, VanguardTransactionParser> {}

        [Subject(typeof(VanguardTransactionParser))]
        public class when_parsing_the_transactions : concern
        {
            Establish c = () =>
            {
                stream_reader = new StreamReader(new MemoryStream());

                var csv_file_parser = depends.on<CSVFileParser>();
                var vanguard_transaction_file_reader_factory = depends.on<VanguardTransactionFileReaderFactory>();
                var mapper = depends.on<ParsedVanguardCVSFileMapper>();

                var csv_vanguard_transactions = new StringReader("");
                vanguard_transaction_file_reader_factory.setup(x => x.CreateTransactionReader(stream_reader)).Return(csv_vanguard_transactions);

                var parsed_vanguard_transactions = fake.an<ParsedCSVFile>();
                csv_file_parser.setup(x => x.Parse(csv_vanguard_transactions)).Return(parsed_vanguard_transactions);

                brokerage_transactions = new[] { new BrokerageTransaction() };
                mapper.setup(x => x.Map(parsed_vanguard_transactions)).Return(brokerage_transactions);
            };

            Because of = () =>
                actual = sut.Parse(stream_reader);

            It should_return_the_transactions = () =>
                actual.Should().Equal(brokerage_transactions);

            static StreamReader stream_reader;
            static IEnumerable<BrokerageTransaction> actual;
            static BrokerageTransaction[] brokerage_transactions;
        }
    }
}