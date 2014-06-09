using core.services;
using data.models.write;
using FluentAssertions;
using Xunit;

namespace core.tests.services
{
    public class TransactionTypeServiceTests
    {
        [Fact]
        public void when_matching_for_an_exact_match_and_the_description_is_an_exact_match_should_return_true()
        {
            var transaction_match = new TransactionMatch { TransactionMatchType = TransactionMatchType.ExactMatch, Description = "Dividend" };
            var description = "dividend";
            var actual = new TransactionTypeService(null).Matches(description, transaction_match);
            actual.Should().BeTrue();
        }

        [Fact]
        public void when_matching_for_an_exact_match_and_the_description_is_not_an_exact_match_should_return_false()
        {
            var transaction_match = new TransactionMatch { TransactionMatchType = TransactionMatchType.ExactMatch, Description = "Dividend" };
            var description = "dividend and stuff";
            var actual = new TransactionTypeService(null).Matches(description, transaction_match);
            actual.Should().BeFalse();
        }

        [Fact]
        public void when_matching_for_a_contains_match_and_the_description_is_exact_match_should_return_true()
        {
            var transaction_match = new TransactionMatch { TransactionMatchType = TransactionMatchType.ContainsMatch, ContainsMatchString = "Dividend" };
            var description = "dividend";
            var actual = new TransactionTypeService(null).Matches(description, transaction_match);
            actual.Should().BeTrue();
        }

        [Fact]
        public void when_matching_for_a_contains_match_and_the_description_is_in_the_middle_should_return_true()
        {
            var transaction_match = new TransactionMatch { TransactionMatchType = TransactionMatchType.ContainsMatch, ContainsMatchString = "Dividend" };
            var description = "a dividend b";
            var actual = new TransactionTypeService(null).Matches(description, transaction_match);
            actual.Should().BeTrue();
        }

        [Fact]
        public void when_matching_for_a_contains_match_and_the_description_is_not_contained_should_return_false()
        {
            var transaction_match = new TransactionMatch { TransactionMatchType = TransactionMatchType.ContainsMatch, ContainsMatchString = "Dividend" };
            var description = "a diviZdend b";
            var actual = new TransactionTypeService(null).Matches(description, transaction_match);
            actual.Should().BeFalse();
        }
    }
}