using System.Collections.Generic;

namespace core.importers.parsers
{
    public interface IFileParser<T>
    {
        IEnumerable<T> Parse(string file_path);
    }
}