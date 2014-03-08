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

        public void Import(string file_path)
        {
            persister.Persist(file_parser.Parse(file_path));
        }
    }
}