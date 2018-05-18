using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Factories;
using JP.Base.DAL.ADO.Repositories.Implementation;
using JP.Base.DAL.ADO.UnitOfWork.Contracts;
using JP.Base.DAL.ADO.UnitOfWork.Implementations;
using JP.Base.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JP.Base.DAL.ADO.EntityMapper;
using JP.Base.DAL.ADO.Commands;
using System.ComponentModel;

namespace Implementations.POC.Logic.ADO
{


    public class UoWFac : IUoWFactory<IBaseUnitOfWorkAdo>
    {
        public IBaseUnitOfWorkAdo CreateUoW()
        {
            var f = new DbConnFactory();

            var connstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\dbAccess.mdb;";

            var u = new UoW(f, "System.Data.OleDb", connstring);
            return u;
        }
    }


    public class clientMapper : IEntityDbMapper<Client>

    {
        public CommandData GetDeleteCommand(object id)
        {
            throw new NotImplementedException();
        }

        public CommandData GetDeleteCommand(Client model)
        {
            throw new NotImplementedException();
        }

        public CommandData GetInsertCommand(Client model)
        {
            throw new NotImplementedException();
        }

        public CommandData GetSelectCommand(object id)
        {
            throw new NotImplementedException();
        }

        public CommandData GetSelectCommand<TSource, TProperty>(Func<Client, string> filter = null, System.Linq.Expressions.Expression<Func<TSource, TProperty>> orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int pageStart = 0, int pageEnd = 0)
        {
            return new CommandData {
                 CommandText= "select * from clients",

            };
        }

        public CommandData GetUpdateCommand(Client model)
        {
            throw new NotImplementedException();
        }
    }


    public class PocRepo<TEntity> : GenericRepository<TEntity> where TEntity : class
    {
        public PocRepo(IEntityDbMapper<TEntity> mapper, IDbAdoConnection conn) : base(mapper, conn)
        {

        }

        protected override TEntity ToEntity(DataTable table)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<TEntity> ToEnumerable(DataTable table)
        {
            throw new NotImplementedException();
        }
    }

    public class UoW : BaseUnitOfWorkAdo
    {
        
        public UoW(IDbConnFactory factory, string dataProvider = "", string connectionString = "") : base(factory, dataProvider, connectionString)
        {
            var r = new PocRepo<Client>(new clientMapper(), currentConnection);
            base.repos.Add(typeof(Client),r);
        }
    }
}
