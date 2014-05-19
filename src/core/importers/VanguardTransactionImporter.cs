using core.importers.parsers;
using core.importers.persisters;
using data.models.write;

namespace core.importers
{
    public class VanguardTransactionImporter : FileTransactionImporter<BrokerageTransaction>
    {
        public VanguardTransactionImporter(VanguardTransactionParser file_parser, ICollectionPersister<BrokerageTransaction> persister) : base(file_parser, persister) {}
    }
}