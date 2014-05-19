using System.Collections.Generic;
using System.IO;

namespace core.importers.parsers
{
    public interface IFileParser<T>
    {
        IEnumerable<T> Parse(StreamReader reader);
    }
}