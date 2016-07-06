using System.Linq;
using core.importers.matches;

namespace core.importers
{
    public class GenericBrokerageTransactionImporter : IFileTransactionImporter
    {
        private TransactionImporterMatches matches;

        public GenericBrokerageTransactionImporter(TransactionImporterMatches matches)
        {
            this.matches = matches;
        }

        public void Import(string text)
        {
            var matching_importer = matches.ImporterMatches.FirstOrDefault(x => x.Matches(text));

            if (matching_importer == null)
                throw new InvalidFileForImportException("Not recognized as a valid file for importing.");

            matching_importer.Import(text);
        }
    }
}