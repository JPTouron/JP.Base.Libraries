using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Factories;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Implementations.DAL.ADO.UnitOfWork;

namespace Logic.POC.As.Composable
{
    public class Uow : BaseUnitOfWorkAdo
    {
        public Uow(IDbConnFactory factory) : base(factory)
        {
            repos.Add(typeof(OperatorRepo), new OperatorRepo(factory.GetConnection()));
        }
    }

    public class UoWFac : IUoWFactory<Uow>
    {
        public Uow CreateUoW()
        {
            var connstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\..\..\resources\dbAccess.mdb;";
            var f = new DbConnFactory("System.Data.OleDb", connstring);

            var u = new Uow(f);
            return u;
        }
    }
}