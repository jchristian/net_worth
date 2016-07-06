using System;

namespace core.importers
{
    public class InvalidFileForImportException : Exception
    {
        public InvalidFileForImportException(string message) : base(message) { }
    }
}