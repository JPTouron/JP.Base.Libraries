using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.EntityMapper;
using JP.Base.DAL.ADO.Repositories;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace JP.Base.Implementations.DAL.ADO.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private IDbConnectionExecutables conn;
        private ICommandMapper<TEntity> mapper;

        public GenericRepository(ICommandMapper<TEntity> mapper, IDbConnectionExecutables conn)
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

        public IEnumerable<TEntity> Get(string filter = null, string orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int page = 0, int pageSize = 0)
        {
            var cmd = mapper.GetSelectCommand(filter, orderBy, order, page, pageSize);
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