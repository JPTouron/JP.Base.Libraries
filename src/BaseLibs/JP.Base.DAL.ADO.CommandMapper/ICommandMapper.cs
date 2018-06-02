using JP.Base.DAL.ADO.Commands;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace JP.Base.DAL.ADO.EntityMapper
{
    public interface ICommandMapper<TEntity> where TEntity : class
    {
        CommandData GetDeleteCommand(TEntity model);

        CommandData GetDeleteCommand(object id);

        CommandData GetInsertCommand(TEntity model);

        CommandData GetSelectCommand<TSource, TProperty>(Func<TEntity, TProperty> filter = null, Expression<Func<TSource, TProperty>> orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int pageStart = 0, int pageEnd = 0);

        CommandData GetSelectCommand(object id);

        CommandData GetUpdateCommand(TEntity model);
    }
}