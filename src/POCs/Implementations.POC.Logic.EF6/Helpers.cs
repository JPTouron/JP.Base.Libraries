﻿using JP.Base.DAL.EF6.Contracts;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Implementations.DAL.EF6.Repositories.Contracts;
using JP.Base.Implementations.DAL.EF6.Repositories.Implementation;
using JP.Base.Implementations.DAL.EF6.UnitOfWork;

namespace Implementations.POC.Logic.EF6
{
    public class CtxtProv : IContextProvider
    {
        private PocDbContext ctx;

        public IDbContext ProvideContext()
        {
            if (ctx == null || ctx.IsDisposed)
                ctx = new PocDbContext();

            return ctx;
        }
    }

    public class PocRepo<TEntity> : GenericRepository<TEntity> where TEntity : class
    {
        public PocRepo(IContextProvider factory) : base(factory)
        {
        }
    }

    public class UoW : BaseUnitOfWorkEf
    {
        public UoW(IContextProvider ctxtFactory) : base(ctxtFactory)
        {
            repos.Add(typeof(Employer), new PocRepo<Employer>(ctxtFactory));
            repos.Add(typeof(Employee), new PocRepo<Employee>(ctxtFactory));
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