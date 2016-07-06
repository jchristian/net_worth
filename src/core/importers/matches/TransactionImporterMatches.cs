using System.Collections.Generic;

namespace core.importers.matches
{
    public class TransactionImporterMatches
    {
        public virtual IEnumerable<ITransactionImporterMatch> ImporterMatches { get; }

        protected TransactionImporterMatches() { }
        public TransactionImporterMatches(VanguardTransactionImporterMatch vanguard_match)
        {
            ImporterMatches = new[]
                              {
                                  vanguard_match
                              };
        }
    }
}