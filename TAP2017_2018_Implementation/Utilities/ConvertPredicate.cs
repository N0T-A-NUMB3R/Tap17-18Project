using System;
using System.Linq;
using System.Linq.Expressions;
using TAP2017_2018_TravelCompanyInterface;


namespace TAP2017_2018_Implementation.Utilities
{
    public static class ConvertPredicate
    {
        public class MyExpressionVisitor : ExpressionVisitor
        {
            private System.Collections.ObjectModel.ReadOnlyCollection<ParameterExpression> _parameters;

            public static Func<LegEntity, bool> Convert<T>(Expression<T> root)
            {
                var visitor = new MyExpressionVisitor();
                var expression = (Expression<Func<LegEntity, bool>>) visitor.Visit(root);
                return expression.Compile();
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return (_parameters != null
                           ? _parameters.FirstOrDefault(p => p.Name == node.Name)
                           : (node.Type == typeof(ILegDTO) ? Expression.Parameter(typeof(LegEntity), node.Name) : node)) ?? throw new ArgumentException();
            }

            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                _parameters = VisitAndConvert(node.Parameters, "VisitLambda");
                return Expression.Lambda(Visit(node.Body), _parameters);
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Member.DeclaringType == typeof(ILegDTO))
                {
                    return Expression.MakeMemberAccess(Visit(node.Expression),
                        typeof(LegEntity).GetProperty(node.Member.Name) ?? throw new InvalidOperationException());
                }

                return base.VisitMember(node);
            }
        }
    }

}