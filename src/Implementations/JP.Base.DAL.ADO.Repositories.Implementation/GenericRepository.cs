using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.EntityMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace JP.Base.DAL.ADO.Repositories.Implementation
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private IDbAdoConnection conn;
        private IEntityDbMapper<TEntity> mapper;

        public GenericRepository(IEntityDbMapper<TEntity> mapper, IDbAdoConnection conn)
        {
            this.mapper = mapper;
            this.conn = conn;
        }

        public void Delete(TEntity entityToDelete)
        {
            var cmd = mapper.GetDeleteCommand(entityToDelete);

            conn.CreateCommand(cmd);
            conn.ExecuteNonQueryCommand();
        }

        public void DeleteById(object id)
        {
            var cmd = mapper.GetDeleteCommand(id);

            conn.CreateCommand(cmd);
            conn.ExecuteNonQueryCommand();
        }

        public IEnumerable<TEntity> Get<TSource, TProperty>(Func<TEntity, string> filter = null, System.Linq.Expressions.Expression<Func<TSource, TProperty>> orderBy = null, ListSortDirection order = 0, int pageStart = 0, int pageEnd = 0)
        {
            var cmd = mapper.GetSelectCommand(filter, orderBy, order, pageStart, pageEnd);

            conn.CreateCommand(cmd);
            return ToEnumerable(conn.ExecuteReaderCommand());
        }

        public TEntity GetById(object id)
        {
            var cmd = mapper.GetSelectCommand(id);

            conn.CreateCommand(cmd);
            return ToEntity(conn.ExecuteReaderCommand());
        }

        public void Insert(TEntity entity)
        {
            var cmd = mapper.GetInsertCommand(entity);

            conn.CreateCommand(cmd);
            conn.ExecuteNonQueryCommand();
        }

        public void Update(TEntity entityToUpdate)
        {
            var cmd = mapper.GetUpdateCommand(entityToUpdate);

            conn.CreateCommand(cmd);
            conn.ExecuteNonQueryCommand();
        }

        protected abstract TEntity ToEntity(DataTable table);

        protected abstract IEnumerable<TEntity> ToEnumerable(DataTable table);
    }
}