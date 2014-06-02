using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace core.tests.helpers
{
    public static class CollectionAssertionExtensions
    {
        public static void ShouldOnlyContain<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            actual.Should().OnlyContain(x => expected.Contains(x));
            expected.Should().OnlyContain(x => actual.Contains(x));
        }

        public static void ShouldOnlyContain<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> equality_comparer)
        {
            actual.Should().OnlyContain(x => expected.Contains(x, equality_comparer));
            expected.Should().OnlyContain(x => actual.Contains(x, equality_comparer));
        }

        public static bool OnlyContains<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            try
            {
                actual.Should().OnlyContain(x => expected.Contains(x));
                expected.Should().OnlyContain(x => actual.Contains(x));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}