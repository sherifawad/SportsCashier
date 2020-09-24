using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SportsCashier.Helpers
{
    public static class LinqHelpers
    {
        public static Func<T, bool> GetEqualsExp<T, TR>(TR nameOfParameter, TR valueToCompare)
        {
            var parameter = Expression.Parameter(typeof(T));
            Expression predicate = Expression.Constant(true);
            Type ex = typeof(TR);
            MethodInfo mi = ex.GetMethod(nameof(nameOfParameter));
            Expression property = Expression.Property(parameter, mi);
            Expression equal = Expression.Equal(property, Expression.Constant(valueToCompare));
            predicate = Expression.AndAlso(predicate, equal);
            return Expression.Lambda<Func<T, bool>>(predicate, parameter).Compile();
        }
        public static Func<T, bool> GetEqualsExp<T, TR>(string nameOfParameter, TR valueToCompare)
        {
            var parameter = Expression.Parameter(typeof(T));
            Expression predicate = Expression.Constant(true);
            Expression property = Expression.Property(parameter, nameOfParameter);
            Expression equal = Expression.Equal(property, Expression.Constant(valueToCompare));
            predicate = Expression.AndAlso(predicate, equal);
            return Expression.Lambda<Func<T, bool>>(predicate, parameter).Compile();
        }
        public static Func<T, bool> GetEqualsExp<T>(string nameOfParameter, int valueToCompare)
        {
            var parameter = Expression.Parameter(typeof(T));
            Expression predicate = Expression.Constant(true);
            Expression property = Expression.Property(parameter, nameOfParameter);
            Expression equal = Expression.Equal(property, Expression.Constant(valueToCompare));
            predicate = Expression.AndAlso(predicate, equal);
            return Expression.Lambda<Func<T, bool>>(predicate, parameter).Compile();
        }
        public static Func<T, bool> GetEqualsExp<T>(string nameOfParameter, string valueToCompare)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            Expression predicate = Expression.Constant(true);
            Expression property = Expression.Property(parameter, nameOfParameter);
            Expression equal = Expression.Equal(property, Expression.Constant(valueToCompare));
            predicate = Expression.AndAlso(predicate, equal);
            return Expression.Lambda<Func<T, bool>>(predicate, parameter).Compile();
        }

        private static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
        {
            var member = expression.Body as MethodCallExpression;

            if (member != null)
                return member.Method;

            throw new ArgumentException("Expression is not a method", "expression");
        }
        
    }
}
