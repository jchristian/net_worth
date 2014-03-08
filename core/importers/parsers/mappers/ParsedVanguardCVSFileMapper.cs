using System.Collections.Generic;
using System.Linq;
using data.models.write;

namespace core.importers.parsers.mappers
{
    public class ParsedVanguardCVSFileMapper
    {
        ParsedVanguardCVSRowMapper row_mapper;

        protected ParsedVanguardCVSFileMapper() {}
        public ParsedVanguardCVSFileMapper(ParsedVanguardCVSRowMapper row_mapper)
        {
            this.row_mapper = row_mapper;
        }

        public virtual IEnumerable<BrokerageTransaction> Map(ParsedCSVFile parsed_file)
        {
            return parsed_file.Rows.Select(row_mapper.Map).ToArray();
        }
    }
}