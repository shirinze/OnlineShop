using System.Linq.Expressions;

namespace OnlineShop.Features;

public class BaseSpecification<TEntity>
{
    public Expression<Func<TEntity, bool>>? Criteria { get; set; }
    public Expression<Func<TEntity, object>>? OrderByExpression { get; set; }
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; set; }

    protected void AddCriteria(Expression<Func<TEntity, bool>> expression)
    {
        if (Criteria is null)
        {
            Criteria = expression;
        }
        else
        {
            var leftParameter = Criteria.Parameters[0];
            var rightParameter = expression.Parameters[0];
            var visitor = new ReplaceParameterVisitor(leftParameter, rightParameter);

            var oldExpression = Criteria.Body;
            var newExpression = visitor.Visit(expression.Body);
            var combined = Expression.AndAlso(oldExpression, newExpression);

            Criteria = Expression.Lambda<Func<TEntity, bool>>(combined, leftParameter);
        }
    }

    protected void AddOrderBy(Expression<Func<TEntity, object>> expression, OrderType orderType)
    {
        if (orderType == OrderType.Ascending)
        {
            OrderByExpression = expression;
        }
        else
        {
            OrderByDescendingExpression = expression;
        }
    }

    private class ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter) : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (ReferenceEquals(node, newParameter))
            {
                return oldParameter;
            }

            return base.VisitParameter(node);
        }
    }
}
