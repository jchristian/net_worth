using System.Collections.Generic;
using System.IO;
using core.importers;
using core.importers.parsers;
using core.importers.persisters;
using data.models.write;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.nsubstitue;
using Machine.Specifications;
using NSubstitute;

namespace core.tests.importers
{
    public class FileTransactionImporterSpecs
    {
        public abstract class concern : Observes<FileTransactionImporter<BrokerageTransaction>> {}

        [Subject(typeof(FileTransactionImporterSpecs))]
        public class when_importing_brokerage_transactions : concern
        {
            Establish c = () =>
            {
                stream_reader = new StreamReader(new MemoryStream());

                var file_parser = depends.on<IFileParser<BrokerageTransaction>>();
                persister = depends.on<ICollectionPersister<BrokerageTransaction>>();

                file_parser.setup(x => x.Parse(stream_reader)).Return(parsed_transactions);
            };

            Because of = () =>
                sut.Import(stream_reader);

            It should_save_the_parsed_transactions = () =>
                persister.Received().Persist(parsed_transactions);
            
            static ICollectionPersister<BrokerageTransaction> persister;
            static IEnumerable<BrokerageTransaction> parsed_transactions;
            static StreamReader stream_reader;
        }
    }
}
