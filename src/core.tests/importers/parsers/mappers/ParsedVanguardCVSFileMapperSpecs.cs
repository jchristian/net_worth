using System.Collections.Generic;
using core.importers.parsers;
using core.importers.parsers.mappers;
using core.tests.helpers;
using data.models.write;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.nsubstitue;
using Machine.Specifications;

namespace core.tests.importers.parsers.mappers
{
    public class ParsedVanguardCVSFileMapperSpecs
    {
        public abstract class concern : Observes<ParsedVanguardCVSFileMapper> {}

        [Subject(typeof(ParsedVanguardCVSFileMapper))]
        public class when_mapped_a_parsed_vanguard_cvs_file : concern
        {
            Establish c = () =>
            {
                first_mapped_row = new BrokerageTransaction();
                second_mapped_row = new BrokerageTransaction();
                third_mapped_row = new BrokerageTransaction();
                
                var row_mapper = depends.on<ParsedVanguardCVSRowMapper>();

                var first_parsed_row = new object();
                var second_parsed_row = new object();
                var third_parsed_row = new object();
                parsed_file = new ParsedCSVFile(new dynamic[] { first_parsed_row, second_parsed_row, third_parsed_row }, null);

                row_mapper.setup(x => x.Map(first_parsed_row)).Return(first_mapped_row);
                row_mapper.setup(x => x.Map(second_parsed_row)).Return(second_mapped_row);
                row_mapper.setup(x => x.Map(third_parsed_row)).Return(third_mapped_row);
            };

            Because of = () =>
                actual = sut.Map(parsed_file);

            It should_map_the_rows = () =>
                actual.ShouldOnlyContain(new[] { first_mapped_row, second_mapped_row, third_mapped_row });
            
            static IEnumerable<BrokerageTransaction> actual;
            static BrokerageTransaction first_mapped_row;
            static BrokerageTransaction second_mapped_row;
            static BrokerageTransaction third_mapped_row;
            static ParsedCSVFile parsed_file;
        }
    }
}
