using core.extensions;
using FluentAssertions;
using Xunit;

namespace core.tests.extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void when_cutting_off_before_a_single_line()
        {
            var actual = "the test is the best".CutBefore("is");

            actual.Should().Be("is the best");
        }

        [Fact]
        public void when_cutting_off_before_when_the_match_is_not_found_should_return_the_original_string()
        {
            var actual = "the test is the best".CutBefore("cat");

            actual.Should().Be("the test is the best");
        }

        [Fact]
        public void when_cutting_off_before_when_the_match_is_an_empty_string()
        {
            var actual = "the test is the best".CutBefore("");

            actual.Should().Be("the test is the best");
        }

        [Fact]
        public void when_cutting_off_before_when_the_match_is_null()
        {
            var actual = "the test is the best".CutBefore(null);

            actual.Should().Be("the test is the best");
        }

        [Fact]
        public void when_cutting_off_before_a_single_line_with_multiple_matches_should_cut_off_before_the_first_match()
        {
            var actual = "this test is the best".CutBefore("is");

            actual.Should().Be("is test is the best");
        }

        [Fact]
        public void when_cutting_off_before_mutliple_lines()
        {
            var actual = @"the test is the best
and there aren't any better
than the best test
in the west".CutBefore("than");

            actual.Should().Be(@"than the best test
in the west");
        }

        [Fact]
        public void when_get_lines_until_blank_and_the_string_is_empty()
        {
            var actual = "".GetLinesUntilBlankLine();

            actual.Should().Be("");
        }

        [Fact]
        public void when_get_lines_until_blank_and_the_string_is_null()
        {
            var actual = ((string)null).GetLinesUntilBlankLine();

            actual.Should().Be(null);
        }

        [Fact]
        public void when_get_lines_until_blank()
        {
            var actual = @"this is a test
of the emergency
 broadcast
system

thank you
for
your".GetLinesUntilBlankLine();

            actual.Should().Be(@"this is a test
of the emergency
 broadcast
system");
        }


        [Fact]
        public void when_get_lines_until_blank_and_there_are_no_blank_lines()
        {
            var actual = @"this is a test
of the emergency
 broadcast
system".GetLinesUntilBlankLine();

            actual.Should().Be(@"this is a test
of the emergency
 broadcast
system");
        }
    }
}