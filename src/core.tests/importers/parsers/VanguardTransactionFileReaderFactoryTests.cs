using System;
using core.importers.parsers.readers;
using FluentAssertions;
using Microsoft.Ajax.Utilities;
using Xunit;

namespace core.tests.importers.parsers
{
    public class VanguardTransactionFileReaderFactoryTests
    {
        [Fact]
        public void when_there_is_a_header_and_body_and_surrounding_junk()
        {
            var transaction_section_header = "Account Number,Trade Date,Process Date,Transaction Type,Transaction Description,Investment Name,Share Price,Shares,Gross Amount,Net Amount";
            var transaction_section_body = @"this is some body
and some more body";
            var textToRead = @"this is some junk{0}{1}{0}{2}".FormatInvariant(Environment.NewLine, transaction_section_header, transaction_section_body)
                + @"

this is some extra junk";

            var actual = new VanguardTransactionFileReaderFactory().CreateTransactionReader(textToRead);

            actual.ReadToEnd().Should().Be("{0}{1}{2}".FormatInvariant(transaction_section_header, Environment.NewLine, transaction_section_body));
        }
    }
}