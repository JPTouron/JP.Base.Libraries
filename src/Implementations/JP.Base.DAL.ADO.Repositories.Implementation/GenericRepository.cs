using JP.Base.DAL.ADO.Commands;
using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.EntityMapper;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace JP.Base.DAL.ADO.Repositories.Implementation
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private IDbConnFactory factory;
        private ICommandMapper<TEntity> mapper;

        public GenericRepository(ICommandMapper<TEntity> mapper, IDbConnFactory factory)
        {
            this.mapper = mapper;
            this.factory = factory;
        }

        public void Delete(TEntity entityToDelete)
        {
            var cmd = mapper.GetDeleteCommand(entityToDelete);

            using (var conn = GetConnection())
            {
                conn.CreateCommand(cmd);
                conn.ExecuteNonQueryCommand();
            }
        }

        public void DeleteById(object id)
        {
            var cmd = mapper.GetDeleteCommand(id);

            using (var conn = GetConnection())
            {
                conn.CreateCommand(cmd);
                conn.ExecuteNonQueryCommand();
            }
        }

        public IEnumerable<TEntity> Get(string filter = null, string orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int page = 0, int pageSize = 0)
        {
            var cmd = mapper.GetSelectCommand(filter, orderBy, order, page, pageSize);

            return ExecuteSelectCommand(cmd);
        }

        public TEntity GetById(object id)
        {
            var cmd = mapper.GetSelectCommand(id);

            using (var conn = GetConnection())
            {
                conn.CreateCommand(cmd);
                return ToEntity(conn.ExecuteReaderCommand());
            }
        }

        public IEnumerable<TEntity> GetByParameters(IEnumerable<ParameterData> filter = null, string orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int page = 0, int pageSize = 0)
        {
            var cmd = mapper.GetSelectCommand(filter, orderBy, order, page, pageSize);

            return ExecuteSelectCommand(cmd);
        }

        public void Insert(TEntity entity)
        {
            var cmd = mapper.GetInsertCommand(entity);

            using (var conn = GetConnection())
            {
                conn.CreateCommand(cmd);
                conn.ExecuteNonQueryCommand();
            }
        }

        public void Update(TEntity entityToUpdate)
        {
            var cmd = mapper.GetUpdateCommand(entityToUpdate);

            using (var conn = GetConnection())
            {
                conn.CreateCommand(cmd);
                conn.ExecuteNonQueryCommand();
            }
        }

        protected abstract TEntity ToEntity(DataTable table);

        protected abstract IEnumerable<TEntity> ToEnumerable(DataTable table);

        private IEnumerable<TEntity> ExecuteSelectCommand(CommandData cmd)
        {
            using (var conn = GetConnection())
            {
                conn.CreateCommand(cmd);
                return ToEnumerable(conn.ExecuteReaderCommand());
            }
        }

        private IDbAdoConnection GetConnection()
        {
            var conn = factory.GetConnection();
            return conn;

            //return myconn;
        }
    }
}