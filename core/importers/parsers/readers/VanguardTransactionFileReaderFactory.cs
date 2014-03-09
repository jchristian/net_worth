using System.IO;
using System.Text;

namespace core.importers.parsers.readers
{
    public class VanguardTransactionFileReaderFactory
    {
        public TextReader CreateTransactionReader(string file_path)
        {
            using (var reader = File.OpenText(file_path))
            {
                AdvanceToTransactionSection(reader);
                
                return new StringReader(ReadCurrentSection(reader));
            }
        }

        string ReadCurrentSection(StreamReader reader)
        {
            var builder = new StringBuilder();
            string next_line;

            while (string.IsNullOrWhiteSpace((next_line = reader.ReadLine()))) {}
            builder.AppendLine(next_line);

            while (!string.IsNullOrWhiteSpace((next_line = reader.ReadLine())))
                builder.AppendLine(next_line);
            return builder.ToString().Trim();
        }
        
        void AdvanceToTransactionSection(StreamReader reader)
        {
            while (!string.IsNullOrWhiteSpace(reader.ReadLine())) { }
        }
    }
}