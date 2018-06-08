using JP.Base.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JP.Base.DAL.UnitOfWork;

namespace Logic.POC.As.Composable
{
    public class myLogic : BaseLogic<Uow>

    {
        public myLogic(IUoWFactory<Uow> factory) : base(factory)
        {
        }

        public Tresult Execute<Tresult>(Func<IUoWFactory<IBaseUnitOfWork>, Tresult> funcy)
        {

            return funcy.Invoke(factory);
        }

        public Tresult Execute<Tresult, TParam>(Func<IUoWFactory<IBaseUnitOfWork>, Tresult> funcy, TParam param)
        {

            return funcy.Invoke(factory);
        }

        public void Execute(Action<IUoWFactory<IBaseUnitOfWork>> action)
        {

            action.Invoke(factory);
        }

        public void Execute(Action<IUoWFactory<IBaseUnitOfWork>, BaseModel<int>> action, BaseModel<int> param)
        {

            action.Invoke(factory, param);
        }

    }
}
