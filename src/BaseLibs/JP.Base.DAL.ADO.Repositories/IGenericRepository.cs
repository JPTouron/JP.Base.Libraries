using JP.Base.DAL.Repositories;
using System.Collections.Generic;
using System.ComponentModel;

namespace JP.Base.DAL.ADO.Repositories
{
    public interface IGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(string filter = null, string orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int page = 0, int pageSize = 0);
    }
}