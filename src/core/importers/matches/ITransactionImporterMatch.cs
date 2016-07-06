namespace core.importers.matches
{
    public interface ITransactionImporterMatch : IFileTransactionImporter
    {
        bool Matches(string text);
    }
}