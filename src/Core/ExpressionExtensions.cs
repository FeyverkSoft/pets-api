using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, Boolean>> Or<T>(this Expression<Func<T, Boolean>> expression1, Expression<Func<T, Boolean>> expression2) 
            => expression1.Compose<Func<T, Boolean>>(expression2, Expression.OrElse);

        public static Expression<Func<T, Boolean>> And<T>(this Expression<Func<T, Boolean>> expression1, Expression<Func<T, Boolean>> expression2) 
            => expression1.Compose<Func<T, Boolean>>(expression2, Expression.AndAlso);

        public static Expression<Func<T, Boolean>> Not<T>(this Expression<Func<T, Boolean>> expression1) 
            => Expression.Lambda<Func<T, Boolean>>(Expression.Not(expression1.Body), expression1.Parameters);

        private static Expression<T> Compose<T>(this LambdaExpression first, LambdaExpression  second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }
}