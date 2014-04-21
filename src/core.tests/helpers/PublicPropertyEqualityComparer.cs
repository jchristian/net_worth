using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace core.tests.helpers
{
    public class PublicPropertyEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).All(property => property.GetValue(x) == property.GetValue(y));
        }

        public int GetHashCode(T obj)
        {
            unchecked
            {
                var hashCode = 0;

                foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => x.GetValue(x)))
                    hashCode = (hashCode * 397) ^ (property != null ? property.GetHashCode() : 0);

                return hashCode;
            }
        }
    }
}