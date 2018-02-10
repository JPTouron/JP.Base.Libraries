using System;
using System.Linq.Expressions;

namespace JP.Base.Common.Extensions.Linq
{
    public static class LinqExtensions
    {
        public static Expression<Func<EntityType, bool>> AndAlso<EntityType>(this Expression<Func<EntityType, bool>> expr1, Expression<Func<EntityType, bool>> expr2) where EntityType : class
        {
            //http://stackoverflow.com/questions/6736505/how-to-combine-two-lambdas/6736589#6736589

            var paramExpr = Expression.Parameter(typeof(EntityType));
            var exprBody = Expression.AndAlso(expr1.Body, expr2.Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<EntityType, bool>>(exprBody, paramExpr);
            return finalExpr;
        }
    }
}