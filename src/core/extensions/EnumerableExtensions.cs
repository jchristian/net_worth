using System;
using System.Collections.Generic;

namespace core.extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<U> Scan<T, U>(this IEnumerable<T> input, Func<U, T, U> next, U state)
        {
            yield return state;
            foreach (var item in input)
            {
                state = next(state, item);
                yield return state;
            }
        }
    }
}