using System.IO;
using core.importers.parsers;
using core.tests.helpers;
using developwithpassion.specifications.nsubstitue;
using FluentAssertions;
using Machine.Specifications;

namespace core.tests.importers.parsers
{
    public class CSVFileParserSpecs
    {
        public abstract class concern : Observes<CSVFileParser> { }

        [Subject(typeof(CSVFileParser))]
        public class when_parsing_a_csv_file : concern
        {
            public class and_there_are_rows
            {
                Establish c = () =>
                {
                    csv_reader = new StringReader(@"a, b, c
                                                1a, 1b, 1c
                                                2a, 2b, 2c");
                };

                Because of = () =>
                    parsed_csv_file = sut.Parse(csv_reader);

                It should_return_the_correct_field_names = () =>
                    parsed_csv_file.FieldNames.ShouldOnlyContain(new[] { "a", "b", "c" });

                It should_return_the_correct_rows = () =>
                    parsed_csv_file.Rows.ShouldOnlyContain(new[]
                                                       {
                                                           new { a = "1a", b = "1b", c = "1c" },
                                                           new { a = "2a", b = "2b", c = "2c" }
                                                       }, new PublicPropertyEqualityComparer<dynamic>());

                static TextReader csv_reader;
                static ParsedCSVFile parsed_csv_file;
            }

            public class and_there_are_no_rows
            {
                Establish c = () =>
                {
                    csv_reader = new StringReader(@"a, b, c");
                };

                Because of = () =>
                    parsed_csv_file = sut.Parse(csv_reader);

                It should_return_the_correct_field_names = () =>
                    parsed_csv_file.FieldNames.ShouldOnlyContain(new[] { "a", "b", "c" });

                It should_return_the_correct_rows = () =>
                    parsed_csv_file.Rows.Should().BeEmpty();

                static TextReader csv_reader;
                static ParsedCSVFile parsed_csv_file;
            }
        }
    }
}
