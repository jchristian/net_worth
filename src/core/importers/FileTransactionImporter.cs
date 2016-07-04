using core.commands;
using core.importers.filters;
using core.importers.parsers;

namespace core.importers
{
    public class VanguardTransactionImporter : IFileTransactionImporter
    {
        VanguardTransactionParser file_parser;
        DuplicateBrokerageTransactionFilter filter;
        AddBrokerageTransactionsCommand add_command;

        public VanguardTransactionImporter(VanguardTransactionParser file_parser,
                                           DuplicateBrokerageTransactionFilter filter,
                                           AddBrokerageTransactionsCommand add_command)
        {
            this.file_parser = file_parser;
            this.filter = filter;
            this.add_command = add_command;
        }

        public void Import(string text)
        {
            add_command.Execute(filter.Filter(file_parser.Parse(text)));
        }
    }
}