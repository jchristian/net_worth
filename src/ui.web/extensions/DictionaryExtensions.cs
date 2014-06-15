using System;
using System.Collections.Generic;

namespace ui.web.extensions
{
    public static class DictionaryExtensions
    {
        public static TVal GetValueOrDefault<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key)
        {
            return dictionary.GetValueOrDefault(key, () => default(TVal));
        }

        public static TVal GetValueOrDefault<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key, Func<TVal> @default)
        {
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            return @default();
        }

        public static TVal? GetValueOrDefault<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key, Func<TVal?> @default) where TVal : struct
        {
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            return @default();
        }
    }
}