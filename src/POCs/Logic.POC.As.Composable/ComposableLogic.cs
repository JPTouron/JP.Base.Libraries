using JP.Base.DAL.Model;
using JP.Base.DAL.UnitOfWork;
using JP.Base.Logic;
using System;

namespace Logic.POC.As.Composable
{
    public class ComposableLogic<TModel> : BaseLogic<Uow>
        where TModel : BaseModel<int>
    {
        public ComposableLogic(IUoWFactory<Uow> factory) : base(factory)
        {
        }

        public Tresult Execute<Tresult>(Func<IUoWFactory<Uow>, Tresult> funcy)
        {
            return funcy.Invoke(factory);
        }

        public Tresult Execute<Tresult, TParam>(Func<IUoWFactory<Uow>, Tresult> funcy, TParam param)
        {
            return funcy.Invoke(factory);
        }

        public void Execute(Action<IUoWFactory<Uow>> action)
        {
            action.Invoke(factory);
        }

        public void Execute(Action<IUoWFactory<Uow>, TModel> action, TModel param)
        {
            action.Invoke(factory, param);
        }
    }

    public class ComposableOperatorLogic : ComposableLogic<Operator>

    {
        public ComposableOperatorLogic(IUoWFactory<Uow> factory) : base(factory)
        {
        }
    }
}