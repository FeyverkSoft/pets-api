using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Core
{
    public class Specification<T> where T : class
    {
        private Expression<Func<T, Boolean>> Expression { get; }

        public Specification(Expression<Func<T, Boolean>> expression)
            => (Expression) 
                = (expression);

        public static Boolean operator false([NotNull] Specification<T> specification) 
            => false;
        public static Boolean operator true([NotNull] Specification<T> specification) 
            => true;

        public static Specification<T> operator &([NotNull] Specification<T> specification1, [NotNull] Specification<T> specification2)
            => specification1.Expression.And(specification2.Expression);

        public static Specification<T> operator !([NotNull] Specification<T> specification)
            => specification.Expression.Not();

        public static Specification<T> operator |([NotNull] Specification<T> specification1, [NotNull] Specification<T> specification2)
            => specification1.Expression.Or(specification2.Expression);

        public static implicit operator Expression<Func<T, Boolean>>([NotNull] Specification<T> specification)
            => specification.Expression;
        public static implicit operator Specification<T>([NotNull] Expression<Func<T, Boolean>> expression)
            => new(expression);
        
        public Boolean IsSatisfiedBy([NotNull]T obj) => Expression.Compile()(obj);
    }
}