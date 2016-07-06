using core.importers.matches;
using FluentAssertions;
using Xunit;

namespace core.tests.importers.matches
{
    public class VanguardTransactionImporterMatchTests
    {
        [Fact]
        public void when_the_header_matches()
        {
            var header = "Account Number,Trade Date,Process Date,Transaction Type,Transaction Description,Investment Name,Share Price,Shares,Gross Amount,Net Amount";

            var actual = new VanguardTransactionImporterMatch(null).Matches(header);

            actual.Should().BeTrue();
        }

        [Fact]
        public void when_the_text_contains_the_header_but_does_not_start_with_the_header()
        {
            var header = @"Blah Blah Blah
BlahAccount Number,Trade Date,Process Date,Transaction Type,Transaction Description,Investment Name,Share Price,Shares,Gross Amount,Net Amount";

            var actual = new VanguardTransactionImporterMatch(null).Matches(header);

            actual.Should().BeTrue();
        }

        [Fact]
        public void when_the_text_does_not_contain_the_header()
        {
            var header = "Account Number,Trade Date,&Process Date,Transaction Type,Transaction Description,Investment Name,Share Price,Shares,Gross Amount,Net Amount";

            var actual = new VanguardTransactionImporterMatch(null).Matches(header);

            actual.Should().BeFalse();
        }
    }
}