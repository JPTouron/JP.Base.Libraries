using JP.Base.DAL.ADO.Commands;
using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.EntityMapper;
using JP.Base.DAL.ADO.Implementations.Factories;
using JP.Base.DAL.ADO.Repositories;
using JP.Base.DAL.ADO.Repositories.Implementation;
using JP.Base.DAL.ADO.UnitOfWork.Contracts;
using JP.Base.DAL.ADO.UnitOfWork.Implementations;
using JP.Base.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Implementations.POC.Logic.ADO
{
    public interface ICustomGenericRepo : IGenericRepository<OperatorWithClient>
    {
        IEnumerable<OperatorWithClient> GetJoinedData(int id);
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
            return new CommandData
            {
                CommandText = $"insert into clients (code,name) values ('{model.Code}','{model.Name}')"
            };
        }

        public CommandData GetSelectCommand(object id)
        {
            return new CommandData
            {
                CommandText = $"select * from clients where id ={id}"
            };
        }

        public CommandData GetSelectCommand<TSource, TProperty>(Func<Client, string> filter = null, System.Linq.Expressions.Expression<Func<TSource, TProperty>> orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int pageStart = 0, int pageEnd = 0)
        {
            return new CommandData
            {
                CommandText = "select * from clients",
            };
        }

        public CommandData GetUpdateCommand(Client model)
        {
            return new CommandData
            {
                CommandText = $"update clients set code ='{model.Code}', name ='{model.Name}' where id={model.Id}",
            };
        }
    }

    public class ClientRepo : GenericRepository<Client>
    {
        public ClientRepo(IEntityDbMapper<Client> mapper, IDbConnFactory factory) : base(mapper, factory)
        {
        }

        protected override Client ToEntity(DataTable table)
        {
            var row = table.Rows[0];
            return new Client
            {
                Code = row["Code"].ToString(),
                Id = Convert.ToInt32(row["Id"].ToString()),
                Name = row["Name"].ToString(),
            };
        }

        protected override IEnumerable<Client> ToEnumerable(DataTable table)
        {
            var rows = table.Rows.Cast<DataRow>().ToList();

            foreach (var row in rows)
            {
                yield return new Client
                {
                    Code = row["Code"].ToString(),
                    Id = Convert.ToInt32(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                };
            }
        }
    }

    public class Custrepo : GenericRepository<OperatorWithClient>, ICustomGenericRepo
    {
        public Custrepo(IEntityDbMapper<OperatorWithClient> mapper, IDbConnFactory factory) : base(mapper, factory)
        {
        }

        public IEnumerable<OperatorWithClient> GetJoinedData(int id)
        {
            throw new NotImplementedException();
        }

        protected override OperatorWithClient ToEntity(DataTable table)
        {
            var row = table.Rows[0];

            return new OperatorWithClient
            {
                Client = new Client
                {
                    Code = row["ClientCode"].ToString(),
                    Id = Convert.ToInt32(row["ClientId"].ToString()),
                    Name = row["ClientName"].ToString()
                },

                Document = row["Document"].ToString(),
                EmployeeNbr = Convert.ToInt32(row["EmployeeNbr"].ToString()),
                FirstName = row["FirstName"].ToString(),
                Id = Convert.ToInt32(row["Id"].ToString()),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                LastName = row["LastName"].ToString(),
            };
        }

        protected override IEnumerable<OperatorWithClient> ToEnumerable(DataTable table)
        {
            throw new NotImplementedException();
        }
    }

    public class UoW : BaseUnitOfWorkAdo
    {
        public UoW(IDbConnFactory factory) : base(factory)
        {
            var r = new ClientRepo(new clientMapper(), factory);
            var r2 = new Custrepo(new customMapper(), factory);
            base.repos.Add(typeof(Client), r);
            base.repos.Add(typeof(OperatorWithClient), r2);
        }
    }

    public class UoWFac : IUoWFactory<IBaseUnitOfWorkAdo>
    {
        public IBaseUnitOfWorkAdo CreateUoW()
        {
            var connstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\dbAccess.mdb;";
            var f = new DbConnFactory("System.Data.OleDb", connstring);

            var u = new UoW(f);
            return u;
        }
    }

    internal class customMapper : IEntityDbMapper<OperatorWithClient>
    {
        public CommandData GetDeleteCommand(object id)
        {
            throw new NotImplementedException();
        }

        public CommandData GetDeleteCommand(OperatorWithClient model)
        {
            throw new NotImplementedException();
        }

        public CommandData GetInsertCommand(OperatorWithClient model)
        {
            throw new NotImplementedException();
        }

        public CommandData GetSelectCommand(object id)
        {
            return new CommandData
            {
                CommandText = $@"SELECT Clients.Id as ClientId, Clients.Name as ClientName, Clients.Code as ClientCode, Operators.Id, Operators.Document, Operators.FirstName,
                                Operators.LastName, Operators.EmployeeNbr, Operators.IsActive
                                FROM Clients INNER JOIN Operators ON Clients.Id = Operators.Id where Clients.Id ={id}"
            };
        }

        public CommandData GetSelectCommand<TSource, TProperty>(Func<OperatorWithClient, string> filter = null, System.Linq.Expressions.Expression<Func<TSource, TProperty>> orderBy = null, ListSortDirection order = ListSortDirection.Ascending, int pageStart = 0, int pageEnd = 0)
        {
            throw new NotImplementedException();
        }

        public CommandData GetUpdateCommand(OperatorWithClient model)
        {
            throw new NotImplementedException();
        }
    }
}