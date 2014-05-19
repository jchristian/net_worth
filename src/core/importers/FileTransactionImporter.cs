using System.IO;
using core.importers.parsers;
using core.importers.persisters;

namespace core.importers
{
    public class FileTransactionImporter<T>
    {
        IFileParser<T> file_parser;
        ICollectionPersister<T> persister;

        public FileTransactionImporter(IFileParser<T> file_parser, ICollectionPersister<T> persister)
        {
            this.file_parser = file_parser;
            this.persister = persister;
        }

        public void Import(StreamReader reader)
        {
            persister.Persist(file_parser.Parse(reader));
        }
    }
}