using System;

namespace core.extensions
{
    public static class ObjectExtensions
    {
        public static TReturn IfNotNullRef<T, TReturn>(this T obj, Func<T, TReturn> accessor) where TReturn : class
        {
            return obj == null ? null : accessor(obj);
        }

        public static TReturn? IfNotNull<T, TReturn>(this T obj, Func<T, TReturn> accessor) where TReturn : struct
        {
            return obj == null ? (TReturn?)null : accessor(obj);
        }
    }
}