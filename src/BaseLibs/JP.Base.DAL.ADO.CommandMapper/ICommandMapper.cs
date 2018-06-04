using JP.Base.DAL.ADO.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace JP.Base.DAL.ADO.EntityMapper
{
    public interface ICommandMapper<TEntity> where TEntity : class
    {
        CommandData GetDeleteCommand(TEntity model);

        CommandData GetDeleteCommand(object id);

        CommandData GetInsertCommand(TEntity model);

        CommandData GetSelectCommand(string filter = null, string orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int page = 0, int pageSize = 0);

        CommandData GetSelectCommand(IEnumerable<ParameterData> filter = null, IEnumerable<ParameterData> orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int page = 0, int pageSize = 0);

        CommandData GetSelectCommand(object id);

        CommandData GetUpdateCommand(TEntity model);
    }
}