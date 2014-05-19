using System.Collections.Generic;
using System.IO;
using core.importers.parsers.mappers;
using core.importers.parsers.readers;
using data.models.write;

namespace core.importers.parsers
{
    public class VanguardTransactionParser : IFileParser<BrokerageTransaction>
    {
        CSVFileParser csv_parser;
        VanguardTransactionFileReaderFactory vanguard_transaction_file_reader_factory;
        ParsedVanguardCVSFileMapper mapper;

        public VanguardTransactionParser(CSVFileParser csv_parser, VanguardTransactionFileReaderFactory vanguard_transaction_file_reader_factory, ParsedVanguardCVSFileMapper mapper)
        {
            this.csv_parser = csv_parser;
            this.vanguard_transaction_file_reader_factory = vanguard_transaction_file_reader_factory;
            this.mapper = mapper;
        }

        public IEnumerable<BrokerageTransaction> Parse(StreamReader reader)
        {
            var parsed_file = csv_parser.Parse(vanguard_transaction_file_reader_factory.CreateTransactionReader(reader));

            return mapper.Map(parsed_file);
        }
    }
}