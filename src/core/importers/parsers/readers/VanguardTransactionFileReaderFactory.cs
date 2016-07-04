using System.IO;
using core.extensions;

namespace core.importers.parsers.readers
{
    public class VanguardTransactionFileReaderFactory
    {
        public virtual TextReader CreateTransactionReader(string text)
        {
            var transaction_section_header = "Account Number,Trade Date,Process Date,Transaction Type,Transaction Description,Investment Name,Share Price,Shares,Gross Amount,Net Amount";

            return new StringReader(text.CutBefore(transaction_section_header)
                                        .GetLinesUntilBlankLine());
        }
    }
}