using JP.Base.DAL.EF6.Repositories.Contracts;
using JP.Base.DAL.EF6.UnitOfWork;
using JP.Base.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JP.Base.DAL.EF6.Contracts;

namespace Implementations.POC.Logic.EF6
{

    public class CtxtProv : IContextProvider
    {
        public IDbContext ProvideContext()
        {
            return new PocDbContext();
        }
    }

    public class UoW : BaseUnitOfWorkEf
    {
        public UoW(IContextProvider ctxtFactory) : base(ctxtFactory)
        {
        }
    }

    public class UoWFac : IUoWFactory<IBaseUnitOfWorkEf>
    {
        public IBaseUnitOfWorkEf CreateUoW()
        {
            return new UoW(new CtxtProv());
        }
    }

}
