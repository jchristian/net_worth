using System;

namespace core.importers.matches
{
    public class VanguardTransactionImporterMatch : ITransactionImporterMatch
    {
        private VanguardTransactionImporter importer;

        public VanguardTransactionImporterMatch(VanguardTransactionImporter importer)
        {
            this.importer = importer;
        }

        public bool Matches(string text)
        {
            var transaction_section_header = "Account Number,Trade Date,Process Date,Transaction Type,Transaction Description,Investment Name,Share Price,Shares,Gross Amount,Net Amount";
            return text.Contains(transaction_section_header);
        }

        public void Import(string text)
        {
            importer.Import(text);
        }
    }
}