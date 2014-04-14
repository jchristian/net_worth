using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace data.Migrations
{
    public static class SeedData
    {
        public static void AddIfDoesNotExist<T, TProp>(this DbSet<T> set, Expression<Func<T, TProp>> identifierExpression, params T[] items) where T : class
        {
            foreach (var item in items)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var left = Expression.Property(parameter, (PropertyInfo)((MemberExpression)identifierExpression.Body).Member);
                var right = Expression.Property(Expression.Constant(item), (PropertyInfo)((MemberExpression)identifierExpression.Body).Member);
                var equal = Expression.Equal(left, right);
                var equalLambda = Expression.Lambda<Func<T, bool>>(equal, parameter);
                var setConstant = Expression.Constant(set);
                var anyCall = Expression.Call(typeof(Queryable).GetMethods().Single(x => x.Name == "Any" && x.GetParameters().Count() == 2).MakeGenericMethod(typeof(T)), setConstant, equalLambda);

                if (!Expression.Lambda<Func<bool>>(anyCall).Compile()())
                    set.Add(item);
            }
        }
    }
}