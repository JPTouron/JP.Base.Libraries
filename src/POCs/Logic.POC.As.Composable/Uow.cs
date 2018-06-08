using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Factories;
using JP.Base.DAL.ADO.UnitOfWork.Implementations;
using JP.Base.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.POC.As.Composable
{



    public class Uow : BaseUnitOfWorkAdo
    {

//        DbConnFactory


        public Uow(IDbConnFactory factory) : base(factory)
        {

        }
    }
}
