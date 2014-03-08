using System.Collections.Generic;
using System.Dynamic;

namespace core.extensions
{
    public static class DynamicExtensions
    {
        public static string GetProperty(this ExpandoObject obj, string property_name)
        {
            return (string)((IDictionary<string, object>)obj)[property_name];
        }
    }
}