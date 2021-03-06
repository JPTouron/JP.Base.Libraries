﻿using JP.Base.DAL.UnitOfWork;

namespace JP.Base.Logic
{
    public abstract class BaseLogic<TIUnitOfWork>
        where TIUnitOfWork : IBaseUnitOfWork
    {
        protected IUoWFactory<TIUnitOfWork> factory;

        public BaseLogic(IUoWFactory<TIUnitOfWork> factory)
        {
            this.factory = factory;
        }
    }
}