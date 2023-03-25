namespace Core
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;

    public class Specification<T> where T : class
    {
        public Specification(Expression<Func<T, Boolean>> expression)
        {
            Expression
                = expression;
        }

        private Expression<Func<T, Boolean>> Expression { get; }

        public static Boolean operator false([NotNull] Specification<T> specification)
        {
            return false;
        }

        public static Boolean operator true([NotNull] Specification<T> specification)
        {
            return true;
        }

        public static Specification<T> operator &([NotNull] Specification<T> specification1, [NotNull] Specification<T> specification2)
        {
            return specification1.Expression.And(specification2.Expression);
        }

        public static Specification<T> operator !([NotNull] Specification<T> specification)
        {
            return specification.Expression.Not();
        }

        public static Specification<T> operator |([NotNull] Specification<T> specification1, [NotNull] Specification<T> specification2)
        {
            return specification1.Expression.Or(specification2.Expression);
        }

        public static implicit operator Expression<Func<T, Boolean>>([NotNull] Specification<T> specification)
        {
            return specification.Expression;
        }

        public static implicit operator Specification<T>([NotNull] Expression<Func<T, Boolean>> expression)
        {
            return new Specification<T>(expression);
        }

        public Boolean IsSatisfiedBy([NotNull] T obj)
        {
            return Expression.Compile()(obj);
        }
    }
}