using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using core.extensions;
using LumenWorks.Framework.IO.Csv;

namespace core.importers.parsers
{
    public class CSVFileParser
    {
        public ParsedCSVFile Parse(TextReader source)
        {
            using (var reader = new CsvReader(source, true))
            {
                return new ParsedCSVFile(GetRows(reader).ToArray(), reader.GetFieldHeaders().Select(StringExtensions.RemoveWhitespace).ToArray());
            }
        }

        IEnumerable<dynamic> GetRows(CsvReader reader)
        {
            var fieldCount = reader.FieldCount;
            var headers = reader.GetFieldHeaders();

            while (reader.ReadNextRecord())
            {
                IDictionary<string, object> row = new ExpandoObject();
                for (var i = 0; i < fieldCount; i++)
                    row.Add(headers[i].RemoveWhitespace(), reader[i]);
                yield return row;
            }
        }
    }
}