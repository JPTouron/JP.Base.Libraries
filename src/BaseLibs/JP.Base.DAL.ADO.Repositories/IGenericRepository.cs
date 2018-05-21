﻿using JP.Base.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace JP.Base.DAL.ADO.Repositories
{
    public interface IGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get<TSource, TProperty>(Func<TEntity, string> filter = null, Expression<Func<TSource, TProperty>> orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int pageStart = 0, int pageEnd = 0);
    }
}