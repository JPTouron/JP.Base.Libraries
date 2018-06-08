using JP.Base.DAL.Model;
using JP.Base.DAL.UnitOfWork;
using System;

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

        public Tresult Execute<Tresult>(Func<IUoWFactory<TIUnitOfWork>, Tresult> funcy)
        {
            return funcy.Invoke(factory);
        }

        public Tresult Execute<Tresult, TParam>(Func<IUoWFactory<TIUnitOfWork>, Tresult> funcy, TParam param)
        {
            return funcy.Invoke(factory);
        }

        public void Execute(Action<IUoWFactory<TIUnitOfWork>> action)
        {
            action.Invoke(factory);
        }

        public void Execute(Action<IUoWFactory<TIUnitOfWork>, BaseModel<int>> action, BaseModel<int> param)
        {
            action.Invoke(factory, param);
        }
    }
}