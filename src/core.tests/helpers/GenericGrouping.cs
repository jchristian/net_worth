using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace core.tests.helpers
{
    public class GenericGrouping<TKey, TVal> : IGrouping<TKey, TVal>
    {
        IEnumerable<TVal> values;
        public TKey Key { get; private set; }

        public GenericGrouping(TKey key, IEnumerable<TVal> values)
        {
            this.values = values;
            Key = key;
        }

        public IEnumerator<TVal> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}